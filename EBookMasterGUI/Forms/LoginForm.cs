using System;
using System.Windows.Forms;

namespace EBookMasterGUI
{
	public partial class LoginForm : Form
	{
		private readonly ApiService _apiService;

		public LoginForm()
		{
			InitializeComponent();
			_apiService = new ApiService();
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
			if (result != null)
			{
				MessageBox.Show("Login successful!");

			}
			else
			{
				MessageBox.Show("Login failed. Please check your email and password.");
			}
		}
	}
}
