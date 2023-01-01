using Crawler.Lib;
using Crawler.Lib.Crawler;
using RobotsParser;
using System.Data.SQLite;

namespace Crawler.UI
{
    public partial class frm_main : Form
    {
        private readonly DocumentRepository _repository;
        private CancellationTokenSource _tokenSource;
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
            _tokenSource = new CancellationTokenSource();

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
                    RobotsParser = new Robots("respectful_crawler", supressSitemapErrors: true)
                }
            );
            manager.Result += Manager_Result;
            manager.Progress += Manager_Progress;
            manager.Completed += Manager_Completed;
            await manager.Start(txt_seed.Text);
        }

        private void Manager_Completed(object? sender, CompletedArgs e)
        {
            _tokenSource.Cancel();
            _tokenSource.TryReset();
            cbx_follow_internal.Enabled = true;
            cbx_follow_sitemap.Enabled = true;
            num_max_internal_depth.Enabled = true;
            btn_stop.Visible = false;
            btn_start.Visible = true;

            if (string.IsNullOrEmpty(e.Error))
            {
                txt_log.AppendText($"{DateTime.UtcNow:s} Crawl completed successfully.{Environment.NewLine}");
            }
            else
            {
                txt_log.AppendText($"{DateTime.UtcNow:s} Crawl completed with error: {e.Error}.{Environment.NewLine}");
            }

        }

        private void btn_stop_Click(object sender, EventArgs e)
        {
            txt_log.AppendText($"{DateTime.UtcNow:s} Crawl stopped.{Environment.NewLine}");

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

        private async void frm_main_Shown(object sender, EventArgs e)
        {
            //create database if not found
            if (!File.Exists(Settings.Instance.DatabaseName))
            {
                SQLiteConnection.CreateFile(Settings.Instance.DatabaseName);
                using var con = new SQLiteConnection(Settings.Instance.ConnectionString);
                using var cmd = new SQLiteCommand("CREATE TABLE document (\r\n\tid INTEGER PRIMARY KEY,\r\n   \turl TEXT NOT NULL,\r\n    last_updated TEXT NULL, --as ISO8601 strings (\"YYYY-MM-DD HH:MM:SS.SSS\").\r\n\tstatus TEXT NULL,\r\n\tbody BLOB  NULL,\r\n\tcontent_type TEXT NULL,\r\n\tblob_download_ms INTEGER NULL\r\n);CREATE INDEX url_index ON document(url)", con);
                await con.OpenAsync();
                cmd.ExecuteNonQuery();
            }
        }
    }
}