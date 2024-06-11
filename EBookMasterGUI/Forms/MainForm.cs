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
			BookListGridView.CellDoubleClick += BookListGridView_CellDoubleClick;
		}

		private void BookListGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
			{
				var selectedRow = BookListGridView.Rows[e.RowIndex];
				string column1Value = selectedRow.Cells["Title"].Value.ToString();
				string column2Value = selectedRow.Cells["Authors"].Value.ToString();
			}
		}
	}
}
