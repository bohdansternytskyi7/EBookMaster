using EBookMasterGUI.DTOs;
using System;
using System.Windows.Forms;
using EBookMasterClassLibrary.DTOs;
using EBookMasterGUI.Interfaces;

namespace EBookMasterGUI.Forms
{
	public partial class MainForm : Form
	{
		private readonly ApiService _apiService;
		private readonly IServiceProvider _serviceProvider;
		private readonly IInfoFormFactory _infoFormFactory;

		public MainForm(ApiService apiService, IServiceProvider serviceProvider, IInfoFormFactory infoFormFactory)
		{
			_apiService = apiService;
			_serviceProvider = serviceProvider;
			_infoFormFactory = infoFormFactory;
			InitializeComponent();
			LoadBooksAsync();
		}

		private async void LoadBooksAsync()
		{
			BookListGridView.DataSource = await _apiService.GetBooksAsync();
			AdjustDataGridViewHeight();
		}

		private void AdjustDataGridViewHeight()
		{
			int totalHeight = BookListGridView.ColumnHeadersHeight;
			foreach (DataGridViewRow row in BookListGridView.Rows)
			{
				totalHeight += row.Height;
			}

			BookListGridView.Height = totalHeight;
		}

		private void BorrowBtn_Click(object sender, System.EventArgs e)
		{

			var book = GetSelectedBook();
			if (book != null)
			{
				var result = MessageBox.Show($"Do you want to borrow a book '{book.Title}' by {book.Authors}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					_apiService.BorrowBookAsync(book.Title, book.Authors);
				}
			}
		}

		private void ReturnBtn_Click(object sender, System.EventArgs e)
		{
			var book = GetSelectedBook();
			if (book != null)
			{
				var result = MessageBox.Show($"Do you want to return a book '{book.Title}' by {book.Authors}?", "",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					_apiService.ReturnBookAsync(book.Title, book.Authors);
				}
			}
		}

		private void InfoBtn_Click(object sender, EventArgs e)
		{
			this.Hide();
			var book = (BookDTO)BookListGridView.SelectedRows[0].DataBoundItem;
			if (book != null)
			{
				_infoFormFactory.Create(book).Show();
			}
		}

		private BorrowRequestDTO GetSelectedBook()
		{
			try
			{
				var selectedRow = BookListGridView.SelectedRows[0];
				var title = selectedRow.Cells[nameof(BookDTO.Title)].Value.ToString();
				var authors = selectedRow.Cells[nameof(BookDTO.Authors)].Value.ToString();
				return new BorrowRequestDTO() { Title = title, Authors = authors };
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
			}

			return null;
		}
	}
}
