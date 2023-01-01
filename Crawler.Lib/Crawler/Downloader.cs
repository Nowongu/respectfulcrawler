using System.Diagnostics;
using System.Net;
using System.Runtime.Serialization;

namespace Crawler.Lib.Crawler
{
    public interface IDownloader
    {
        Task<DownloadResult> GetResult(string url, CancellationToken cancellationToken);
    }

    public class DownloadResult
    {
        public string? Url { get; set; }
        public int StatusCode { get; set; }
        public string? ContentType { get; set; }
        public string? Content { get; set; }
        public long DownloadTime_ms { get; set; }
    }

    public sealed class Downloader : IDownloader
    {
        private HttpClient _client;
        private int _retryCount = 3;
        private readonly TimeSpan _delay = TimeSpan.FromSeconds(5);

        static Downloader() { }

        //used for mocking
        public Downloader(IDownloader downloader)
            : this()
        {
            Instance = downloader;
        }

        private Downloader()
        {
            var handler = new SocketsHttpHandler
            {
                PooledConnectionLifetime = TimeSpan.FromMinutes(15), // Recreate every 15 minutes
                AllowAutoRedirect = true,
                MaxAutomaticRedirections = 15
            };
            _client = new HttpClient(handler);
        }

        public static IDownloader Instance { get; private set; } = new Downloader();

        public async Task<DownloadResult> GetResult(string url, CancellationToken cancellationToken)
        {
            var timer = new Stopwatch();
            timer.Start();
            var result = await OperationWithBasicRetryAsync(url, cancellationToken);
            timer.Stop();
            result.DownloadTime_ms = timer.ElapsedMilliseconds;
            return result;
        }

        //https://learn.microsoft.com/en-us/azure/architecture/patterns/retry
        public async Task<DownloadResult> OperationWithBasicRetryAsync(string url, CancellationToken cancellationToken)
        {
            int currentRetry = 0;

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    // Call external service.
                    var result = await TransientOperationAsync(url, cancellationToken);

                    return result;
                }
                catch (Exception ex)
                {
                    Trace.TraceError("Operation Exception");

                    currentRetry++;

                    // Check if the exception thrown was a transient exception
                    // based on the logic in the error detection strategy.
                    // Determine whether to retry the operation, as well as how
                    // long to wait, based on the retry strategy.
                    if (currentRetry > _retryCount || !IsTransient(ex))
                    {
                        // If this isn't a transient error or we shouldn't retry,
                        // rethrow the exception.

                        //ToDo: Add logging here
                        return new DownloadResult
                        {
                            StatusCode = -1,
                            Url = url
                        };
                    }
                }

                // Wait to retry the operation.
                // Consider calculating an exponential delay here and
                // using a strategy best suited for the operation and fault.
                await Task.Delay(_delay, cancellationToken);
            }

            return new DownloadResult
            {
                StatusCode = -1,
                Url = url
            };
        }

        // Async method that wraps a call to a remote service (details not shown).
        private async Task<DownloadResult> TransientOperationAsync(string url, CancellationToken cancellationToken)
        {
            var response = await _client.GetAsync(url, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new OperationTransientException();
            }

            return new DownloadResult
            {
                Content = await response.Content.ReadAsStringAsync(),
                ContentType = response.Content.Headers.ContentType?.MediaType?.ToString() ?? string.Empty,
                StatusCode = (int)response.StatusCode,
                Url = url
            };
        }

        private bool IsTransient(Exception ex)
        {
            // Determine if the exception is transient.
            // In some cases this is as simple as checking the exception type, in other
            // cases it might be necessary to inspect other properties of the exception.
            if (ex is OperationTransientException)
                return true;

            var webException = ex as WebException;
            if (webException != null)
            {
                // If the web exception contains one of the following status values
                // it might be transient.
                return new[] {
                    WebExceptionStatus.ConnectionClosed,
                      WebExceptionStatus.Timeout,
                      WebExceptionStatus.RequestCanceled }
                .Contains(webException.Status);
            }

            // Additional exception checking logic goes here.
            return false;
        }

        [Serializable]
        private class OperationTransientException : Exception
        {
            public OperationTransientException()
            {
            }

            public OperationTransientException(string? message) : base(message)
            {
            }

            public OperationTransientException(string? message, Exception? innerException) : base(message, innerException)
            {
            }

            protected OperationTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
            {
            }
        }
    }
}