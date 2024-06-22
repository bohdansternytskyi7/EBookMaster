using EBookMasterGUI.Forms;
using EBookMasterGUI.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using EBookMasterGUI.DTOs;

namespace EBookMasterGUI.Factories
{
	public class InfoFormFactory : IInfoFormFactory
	{
		private readonly IServiceProvider _serviceProvider;

		public InfoFormFactory(IServiceProvider serviceProvider)
		{
			_serviceProvider = serviceProvider;
		}

		public InfoForm Create(BookDTO bookDto)
		{
			return ActivatorUtilities.CreateInstance<InfoForm>(_serviceProvider, bookDto);
		}
	}
}
