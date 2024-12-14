using FGShop.BussinessLayer.Interfaces;
using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.AuthDtos;
using FGShop.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
	public class UserInformationService : IUserInformationService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly FGShopContext _context;

		public UserInformationService(UserManager<ApplicationUser> userManager, FGShopContext context)
		{
			_userManager = userManager;
			_context = context;
		}

		public async Task<string> GetByUserId(int id)
		{
			var data = await _context.Users
				.Where(x => x.Id == id)
				.Select(x => x.UserName)
				.FirstOrDefaultAsync();
			return data;
		}

		public async Task<UserListDto> GetByUserName(string userName)
		{
			var user = await _userManager.FindByNameAsync(userName);
			var userDto = new UserListDto
			{
				Username = userName,
				Name = user.Name,
				Email = user.Email,
				Surname = user.Surname,
				PhoneNumber = user.PhoneNumber
			};

			return userDto;

		}
	}
}
