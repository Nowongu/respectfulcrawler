using Crawler.Lib;
using System.Configuration;

namespace Crawler.UI
{
    public partial class frm_main : Form
    {
        DocumentRepository _repository;

        public frm_main()
        {
            InitializeComponent();
            Settings.Instance.ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            _repository = new DocumentRepository();
        }

        private void btn_start_Click(object sender, EventArgs e)
        {

        }

        private async void btn_deleteall_Click(object sender, EventArgs e)
        {
            await _repository.DeleteAll();
        }
    }
}