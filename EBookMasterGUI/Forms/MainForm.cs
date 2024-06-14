using EBookMasterGUI.DTOs;
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

		private async void BookListGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
			{
				var selectedRow = BookListGridView.Rows[e.RowIndex];
				string title = selectedRow.Cells[nameof(BookDTO.Title)].Value.ToString();
				string authors = selectedRow.Cells[nameof(BookDTO.Authors)].Value.ToString();
				var result = MessageBox.Show($"Do you want to borrow a book '{title}' by {authors}?",
									"Confirm",
									MessageBoxButtons.YesNo,
									MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					if (await _apiService.BorrowBookAsync(title, authors))
					{
						MessageBox.Show("Book borrowed successfully.", null, MessageBoxButtons.OK);
					}
					else
					{
						MessageBox.Show("There was a problem borrowing a book. Check your subscription.", null, MessageBoxButtons.OK);
					}
				}
			}
		}
	}
}
