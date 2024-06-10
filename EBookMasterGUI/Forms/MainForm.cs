using System.Windows.Forms;

namespace EBookMasterGUI.Forms
{
	public partial class MainForm : Form
	{
		private readonly ApiService _apiService;

		public MainForm(ApiService apiService)
		{
			_apiService = apiService;
			InitializeComponent();
			LoadBooksAsync();
		}

		private async void LoadBooksAsync()
		{
			BookListGridView.DataSource = await _apiService.GetBooksAsync();
		}
	}
}
