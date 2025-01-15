using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AutoMapper;
using EBookMasterWebApi.Context;
using EBookMasterWebApi.DTOs;
using EBookMasterWebApi.Enums;
using EBookMasterWebApi.Models;
using EBookMasterWebApi.Services.Interfaces;

namespace EBookMasterWebApi.Controllers
{
    public class BaseController : Controller
	{

		protected readonly EBookMasterDbContext _context;
		protected readonly IConfiguration _configuration;
		protected readonly IMapper _mapper;
		protected readonly IAccountsService _accountsService;

		protected BaseController(EBookMasterDbContext context, IConfiguration configuration, IMapper mapper, IAccountsService accountsService)
		{
			_context = context;
			_configuration = configuration;
			_mapper = mapper;
			_accountsService = accountsService;
		}

		protected int GetCurrentUserId()
		{
			var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (!int.TryParse(userIdString, out var userId))
				throw new InvalidOperationException("User Id claim is not a valid integer.");
			return userId;
		}

		protected async Task<User> GetCurrentUserAsync()
		{
			var userId = GetCurrentUserId();
			var user = await _context.Users.Include(x => x.Subscription).FirstOrDefaultAsync(x => x.Id == userId);
			if (user == null)
				throw new InvalidOperationException("User not found.");
			return user;
		}

		protected async Task<User> RegisterUserAsync(RegisterRequestDTO registerRequest, Role role)
		{
			var existingUser = await _context.Users.Where(x => x.Email == registerRequest.Email).FirstOrDefaultAsync();
			if (existingUser != null)
				throw new InvalidOperationException("User with the same email already exists.");
			if (registerRequest.SubscriptionId == 0)
				throw new InvalidOperationException("No subscription chosen.");

			var user = _mapper.Map<User>(registerRequest);
			user.Role = Role.Reader;
			user.LibraryCardNumber = await GetNexLibraryCardNumberAsync();
			user.Subscription = await _context.Subscriptions.FirstAsync(x => x.Id == registerRequest.SubscriptionId);
			user.Password = _accountsService.HashPassword(registerRequest.Password, user.Salt = _accountsService.GenerateSalt());
			user.RefreshToken = _accountsService.GenerateSalt();
			user.RefreshTokenExpiration = DateTime.Now.AddHours(1);
			await _context.Users.AddAsync(user);
			await _context.SaveChangesAsync();
			return user;
		}

		private async Task<int> GetNexLibraryCardNumberAsync()
		{
			var libraryCardNumbers = await _context.Users.Select(x => x.LibraryCardNumber).ToListAsync();
			return (libraryCardNumbers.Any() ? libraryCardNumbers.Max() : 0) + 1;
		}
	}
}
