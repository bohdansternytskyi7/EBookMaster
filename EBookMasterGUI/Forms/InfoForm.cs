using System;
using System.Windows.Forms;
using EBookMasterGUI.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace EBookMasterGUI.Forms
{
	public partial class InfoForm : Form
	{
		private readonly ApiService _apiService;
		private readonly IServiceProvider _serviceProvider;

		public InfoForm(BookDTO bookDto, ApiService apiService, IServiceProvider serviceProvider)
		{
			_apiService = apiService;
			_serviceProvider = serviceProvider;
			InitializeComponent();
			this.TitleText.Text = $"Borrow history of the book '{bookDto.Title}' by '{bookDto.Authors}'";
			BookBorrowingGridView.DataSource = bookDto.BookBorrowings;
			AdjustDataGridViewHeight();
			this.BookBorrowingGridView.Focus();
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
