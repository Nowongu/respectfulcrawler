using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Crawler.Lib.Crawler
{
    public class Manager
    {
        private readonly SitemapFeeder _feeder;
        private readonly DocumentRepository _repository;
        private readonly SemaphoreSlim _semaphore;

        //Can update Manger to take multiple feeders
        public Manager(SitemapFeeder feeder, DocumentRepository repository)
        {
            _feeder = feeder;
            _repository = repository;
            _semaphore = new SemaphoreSlim(0, 10);
        }

        public event EventHandler<ResultArgs>? Result;

        private void RaiseResult(Document result) => Result?.Invoke(this, 
            new ResultArgs() 
            {
                Body = result.Body,
                ContentType = result.ContentType,
                StatusCode = result.Status,
                Url = result.Url
            });

        public async Task Start()
        {
            _feeder.UrlFound += Feeder_UrlFound;
            await _feeder.Start();
        }

        private async void Feeder_UrlFound(object? sender, UrlFoundArgs e)
        {
            _semaphore.Wait();

            try
            {
                var result = await Downloader.Instance.GetResult(e.Url);

                var document = new Document
                {
                    Url = result.Url,
                    Body = result.Content,
                    ContentType = result.ContentType,
                    Status = result.StatusCode.ToString(),
                    LastUpdated = DateTime.UtcNow
                };
                var sqlResult = await _repository.Save(document);

                if (sqlResult == 1) RaiseResult(document);
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
