﻿using AutoMapper;
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
				.ForMember(x => x.UserSubscription, y => y.Ignore())
				.ForMember(x => x.LibraryCardNumber, y => y.Ignore())
				.ForMember(x => x.BookBorrowings, y => y.Ignore());

			CreateMap<Book, BookDTO>()
				.ForMember(x => x.Borrowed, y => y.Ignore())
				.ForMember(x => x.NotAllowed, y => y.Ignore());

			CreateMap<PublishingHouse, PublishingHouseDTO>()
				.ForMember(x => x.Books, y => y.Ignore());

			CreateMap<Series, SeriesDTO>()
				.ForMember(x => x.Books, y => y.Ignore());

			CreateMap<Author, AuthorDTO>()
				.ForMember(x => x.Books, y => y.Ignore());

			CreateMap<Category, CategoryDTO>()
				.ForMember(x => x.Books, y => y.Ignore());

			CreateMap<Recommendation, RecommendationDTO>()
				.ForMember(x => x.RecommendedBooks, y => y.Ignore());

			CreateMap<BookBorrowing, BookBorrowingDTO>();
		}
	}
}
