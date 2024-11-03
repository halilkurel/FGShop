using FGShop.BussinessLayer.Interfaces;
using FGShop.DtoLayer.AuthDtos;
using FGShop.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
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

		public UserInformationService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
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
