using System;
using System.Windows.Forms;
using EBookMasterGUI.Forms;
using Microsoft.Extensions.DependencyInjection;

namespace EBookMasterGUI
{
	public partial class LoginForm : Form
	{
		private readonly ApiService _apiService;
		private readonly IServiceProvider _serviceProvider;

		public LoginForm(ApiService apiService, IServiceProvider serviceProvider)
		{
			InitializeComponent();
			_apiService = apiService;
			_serviceProvider = serviceProvider;
			this.StartPosition = FormStartPosition.CenterScreen;
		}

		private async void btnLogin_Click(object sender, EventArgs e)
		{
			var email = txtEmail.Text;
			var password = txtPassword.Text;

			if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
			{
				MessageBox.Show("Please enter both email and password.");
				return;
			}

			var result = await _apiService.LoginAsync(email, password);
			if (result)
			{
				this.Hide();
				_serviceProvider.GetRequiredService<MainForm>().Show();
			}
			else
			{
				MessageBox.Show("Login failed. Please check your email and password.");
			}
		}
	}
}
