using EBookMasterGUI.DTOs;
using EBookMasterGUI.Forms;

namespace EBookMasterGUI.Interfaces
{
	public interface IInfoFormFactory
	{
		InfoForm Create(BookDTO bookDto);
	}
}
