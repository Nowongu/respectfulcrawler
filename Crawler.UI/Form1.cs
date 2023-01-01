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
        private int pageCount = 0;
        private int queueLength = 0;

        public frm_main()
        {
            _tokenSource = new CancellationTokenSource();
            InitializeComponent();
            _repository = new DocumentRepository();
            txt_seed.Text = Settings.Instance.SeedUrl;
            cbx_follow_internal.Checked = Settings.Instance.FollowInternalLinks;
            num_max_internal_depth.Value = Settings.Instance.MaxInternalDepth;
            cbx_follow_sitemap.Checked = Settings.Instance.FollowSitemap;
        }

        private async void btn_start_Click(object sender, EventArgs e)
        {
            pageCount = 0;
            queueLength = 0;
            txt_log.AppendText($"{DateTime.UtcNow:s} Crawl started.{Environment.NewLine}");

            btn_start.Visible = false;
            btn_stop.Visible = true;
            cbx_follow_sitemap.Enabled = false;
            cbx_follow_internal.Enabled = false;
            num_max_internal_depth.Enabled = false;

            var manager = new Manager(
                new ManagerContext()
                {
                    FollowInternalLinks = cbx_follow_internal.Checked,
                    MaxInternalDepth = (int)num_max_internal_depth.Value,
                    FollowSitemap = cbx_follow_sitemap.Checked,
                    CancellationToken = _tokenSource.Token,
                    DocumentRepository = _repository,
                    RobotsParser = new Robots(txt_seed.Text, "respectful_crawler", supressSitemapErrors: true)
                }
            );
            manager.Result += Manager_Result;
            manager.Progress += Manager_Progress;
            await manager.Start(txt_seed.Text);
            txt_log.AppendText($"{DateTime.UtcNow:s} Crawl completed.{Environment.NewLine}");
        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            txt_log.AppendText($"{DateTime.UtcNow:s} Crawl stopped{Environment.NewLine}");

            _tokenSource.Cancel();
            _tokenSource.TryReset();
            cbx_follow_internal.Enabled = true;
            cbx_follow_sitemap.Enabled = true;
            num_max_internal_depth.Enabled = true;
            btn_stop.Visible = false;
            btn_start.Visible = true;
        }

        private void Manager_Progress(object? sender, ProgressArgs e)
        {
            queueLength += e.QueueCount;
            lblQueue.Text = $"Links in queue: {queueLength}";
        }

        private void Manager_Result(object? sender, ResultArgs e)
        {
            if (_tokenSource.IsCancellationRequested) return;

            _recentTimes[pageCount % _recentTimes.Length] = e.DownloadTime_ms;
            lblCount.Text = $"Pages crawled: {++pageCount}";
            lblResponseTime.Text = $"Response time: {_recentTimes.Average()} ms";
            txt_log.AppendText($"{DateTime.UtcNow:s} {e.StatusCode} {e.Url}{Environment.NewLine}");
        }

        private async void btn_deleteall_Click(object sender, EventArgs e)
        {
            var itemsDeleted = await _repository.DeleteAll();
            txt_log.AppendText($"Deleted {itemsDeleted} records.{Environment.NewLine}");
        }

        private void frm_main_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tokenSource?.Cancel();
        }

        private void txt_seed_TextChanged(object sender, EventArgs e)
        {
            Settings.Instance.SeedUrl = txt_seed.Text;
        }

        private void cbx_follow_sitemap_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.FollowSitemap = cbx_follow_sitemap.Checked;
        }

        private void cbx_follow_internal_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Instance.FollowInternalLinks = cbx_follow_internal.Checked;
            if (cbx_follow_internal.Checked)
            {
                lbl_max_depth.Visible = true;
                num_max_internal_depth.Visible = true;
            }
            else
            {
                lbl_max_depth.Visible = false;
                num_max_internal_depth.Visible = false;
            }
        }

        private void num_max_internal_depth_ValueChanged(object sender, EventArgs e)
        {
            Settings.Instance.MaxInternalDepth = (int)num_max_internal_depth.Value;
        }
    }
}