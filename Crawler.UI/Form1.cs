using Crawler.Lib;
using Crawler.Lib.Crawler;
using RobotsSharpParser;
using System.Configuration;

namespace Crawler.UI
{
    public partial class frm_main : Form
    {
        private readonly DocumentRepository _repository;

        public frm_main()
        {
            InitializeComponent();
            Settings.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _repository = new DocumentRepository();
        }

        private async void btn_start_Click(object sender, EventArgs e)
        {
            var feeder = new SitemapFeeder(new Robots(txt_seed.Text, "Respectful Crawler"));
            var manager = new Manager(feeder, _repository);
            manager.Result += Manager_Result;
            await manager.Start();
        }

        private void Manager_Result(object? sender, ResultArgs e)
        {
            txt_log.AppendText($"{DateTime.UtcNow:s} {e.StatusCode} {e.Url}");
        }

        private async void btn_deleteall_Click(object sender, EventArgs e)
        {
            await _repository.DeleteAll();
        }
    }
}