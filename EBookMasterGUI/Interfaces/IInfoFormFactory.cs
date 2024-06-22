using EBookMasterClassLibrary.DTOs;
using EBookMasterGUI.Forms;

namespace EBookMasterGUI.Interfaces
{
	public interface IInfoFormFactory
	{
		InfoForm Create(BorrowRequestDTO borrowRequest);
	}
}
