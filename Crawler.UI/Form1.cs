using Crawler.Lib;
using Crawler.Lib.Crawler;
using RobotsParser;
using System.Configuration;

namespace Crawler.UI
{
    public partial class frm_main : Form
    {
        private readonly DocumentRepository _repository;
        private readonly CancellationTokenSource _tokenSource;
        private readonly long[] _recentTimes = new long[20];
        private int count = 0;

        public frm_main()
        {
            _tokenSource = new CancellationTokenSource();
            InitializeComponent();
            Settings.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _repository = new DocumentRepository();
        }

        private async void btn_start_Click(object sender, EventArgs e)
        {
            count = 0;
            txt_log.AppendText($"{DateTime.UtcNow:s} Crawl started{Environment.NewLine}");

            btn_start.Visible = false;
            btn_stop.Visible = true;
            var manager = new Manager(new Robots(txt_seed.Text, "respectful_crawler"), _repository, _tokenSource.Token);
            manager.Result += Manager_Result;
            await manager.Start();
        }
        private void btn_stop_Click(object sender, EventArgs e)
        {
            txt_log.AppendText($"{DateTime.UtcNow:s} Crawl stopped{Environment.NewLine}");

            _tokenSource.Cancel();
            _tokenSource.TryReset();
            btn_stop.Visible = false;
            btn_start.Visible = true;
        }

        private void Manager_Result(object? sender, ResultArgs e)
        {
            if (_tokenSource.IsCancellationRequested) return;

            _recentTimes[count % _recentTimes.Length] = e.DownloadTime_ms;
            lblQueue.Text = $"Links in queue: {++count}";
            lblResponseTime.Text = $"Response time: {_recentTimes.Average()} ms";
            txt_log.AppendText($"{DateTime.UtcNow:s} {e.StatusCode} {e.Url}{Environment.NewLine}");
        }

        private async void btn_deleteall_Click(object sender, EventArgs e)
        {
            var itemsDeleted = await _repository.DeleteAll();
            txt_log.AppendText($"Delete {itemsDeleted} records.{Environment.NewLine}");
        }

        private void frm_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tokenSource?.Cancel();
        }
    }
}