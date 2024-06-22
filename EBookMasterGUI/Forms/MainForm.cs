using EBookMasterGUI.DTOs;
using Microsoft.Extensions.DependencyInjection;
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
			try
			{
				var selectedRow = BookListGridView.SelectedRows[0];
				var title = selectedRow.Cells[nameof(BookDTO.Title)].Value.ToString();
				var authors = selectedRow.Cells[nameof(BookDTO.Authors)].Value.ToString();
				var result = MessageBox.Show($"Do you want to borrow a book '{title}' by {authors}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					_apiService.BorrowBookAsync(title, authors);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
			}
		}

		private void ReturnBtn_Click(object sender, System.EventArgs e)
		{
			try
			{
				var selectedRow = BookListGridView.SelectedRows[0];
				var title = selectedRow.Cells[nameof(BookDTO.Title)].Value.ToString();
				var authors = selectedRow.Cells[nameof(BookDTO.Authors)].Value.ToString();
				var result = MessageBox.Show($"Do you want to return a book '{title}' by {authors}?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

				if (result == DialogResult.Yes)
				{
					_apiService.ReturnBookAsync(title, authors);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
			}
		}

		private void InfoBtn_Click(object sender, EventArgs e)
		{
			try
			{
				var selectedRow = BookListGridView.SelectedRows[0];
				var title = selectedRow.Cells[nameof(BookDTO.Title)].Value.ToString();
				var authors = selectedRow.Cells[nameof(BookDTO.Authors)].Value.ToString();
				this.Hide();
				_infoFormFactory.Create(new BorrowRequestDTO() { Title = title, Authors = authors }).Show();

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
			}
		}
	}
}
