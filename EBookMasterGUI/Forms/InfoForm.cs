using System;
using System.Windows.Forms;
using EBookMasterClassLibrary.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace EBookMasterGUI.Forms
{
	public partial class InfoForm : Form
	{
		private readonly ApiService _apiService;
		private readonly IServiceProvider _serviceProvider;

		public string BookTitle { get; set; }
		public string BookAuthors { get; set; }

		public InfoForm(BorrowRequestDTO borrowRequestDto, ApiService apiService, IServiceProvider serviceProvider)
		{
			_apiService = apiService;
			_serviceProvider = serviceProvider;
			InitializeComponent();
			this.BookTitle = borrowRequestDto.Title;
			this.BookAuthors = borrowRequestDto.Authors;
			this.TitleText.Text = $"Borrow history of the book '{this.BookTitle}' by '{this.BookAuthors}'";
			LoadBorrowHistoryAsync();
			this.BookBorrowingGridView.Focus();
		}

		private async void LoadBorrowHistoryAsync()
		{
			BookBorrowingGridView.DataSource = await _apiService.GetBookBorrowHistoryAsync(this.BookTitle, this.BookAuthors);
			AdjustDataGridViewHeight();
		}

		private void AdjustDataGridViewHeight()
		{
			int totalHeight = BookBorrowingGridView.ColumnHeadersHeight;
			foreach (DataGridViewRow row in BookBorrowingGridView.Rows)
			{
				totalHeight += row.Height;
			}

			BookBorrowingGridView.Height = totalHeight;
		}

		private void BackBtn_Click(object sender, System.EventArgs e)
		{
			this.Hide();
			_serviceProvider.GetRequiredService<MainForm>().Show();
		}
	}
}
