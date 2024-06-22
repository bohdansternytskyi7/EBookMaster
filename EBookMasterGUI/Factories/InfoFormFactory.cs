using EBookMasterClassLibrary.DTOs;
using EBookMasterGUI.Forms;
using EBookMasterGUI.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EBookMasterGUI.Factories
{
	public class InfoFormFactory : IInfoFormFactory
	{
		private readonly IServiceProvider _serviceProvider;

		public InfoFormFactory(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public InfoForm Create(BorrowRequestDTO borrowRequest)
		{
			return ActivatorUtilities.CreateInstance<InfoForm>(_serviceProvider, borrowRequest);
		}
	}
}
