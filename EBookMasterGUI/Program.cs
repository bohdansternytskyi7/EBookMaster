using EBookMasterGUI.Factories;
using EBookMasterGUI.Forms;
using EBookMasterGUI.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;

namespace EBookMasterGUI
{
	public static class Program
	{
		public static IServiceProvider ServiceProvider { get; private set; }

		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			var services = new ServiceCollection();
			ConfigureServices(services);
			ServiceProvider = services.BuildServiceProvider();

			var loginForm = ServiceProvider.GetRequiredService<LoginForm>();
			Application.Run(loginForm);
		}

		private static void ConfigureServices(ServiceCollection services)
		{
			services.AddSingleton<ApiService>();
			services.AddTransient<LoginForm>();
			services.AddTransient<MainForm>();
			services.AddTransient<InfoForm>();
			services.AddSingleton<IInfoFormFactory, InfoFormFactory>();
		}
	}
}
