using AutoMapper;
using EBookMasterWebApi.DTOs;
using EBookMasterWebApi.Models;

namespace EBookMasterWebApi.Mapping
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<RegisterRequestDTO, User>()
				.ForMember(x => x.Password, y => y.Ignore())
				.ForMember(x => x.Salt, y => y.Ignore())
				.ForMember(x => x.RefreshToken, y => y.Ignore())
				.ForMember(x => x.RefreshTokenExpiration, y => y.Ignore())
				.ForMember(x => x.Role, y => y.Ignore())
				.ForMember(x => x.Subscription, y => y.Ignore())
				.ForMember(x => x.LibraryCardNumber, y => y.Ignore())
				.ForMember(x => x.BookBorrowings, y => y.Ignore());
		}
	}
}
