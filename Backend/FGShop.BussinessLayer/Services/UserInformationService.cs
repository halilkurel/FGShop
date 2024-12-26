using FGShop.BussinessLayer.Interfaces;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.AuthDtos;
using FGShop.DtoLayer.UserDtos;
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
		private readonly IUow _uow;

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
				Id = user.Id,
				Username = userName,
				Name = user.Name,
				Email = user.Email,
				Surname = user.Surname,
				PhoneNumber = user.PhoneNumber
			};

			return userDto;

		}


        public async Task<GetByUserIdDto> GetByUserIdInformation(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var userDto = new GetByUserIdDto
            {
                Id = user.Id,
                Username = user.UserName,
                Name = user.Name,
                Email = user.Email,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber
            };

            return userDto;

        }

        public async Task<UpdateUserInformationDto> UpdateUserInformationDto(UpdateUserInformationDto dto)
        {
            // Kullanıcıyı bul
            var user = await _userManager.FindByIdAsync(dto.Id.ToString());
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            // Kullanıcı bilgilerini güncelle
            user.UserName = dto.Username;
            user.Name = dto.Name;
            user.Email = dto.Email;
            user.Surname = dto.Surname;
            user.PhoneNumber = dto.PhoneNumber;

            // Güncellemeyi veritabanına kaydet
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception("Kullanıcı güncellenirken bir hata oluştu.");
            }

            // Güncellenmiş bilgileri DTO olarak geri döndür
            var updatedDto = new UpdateUserInformationDto
            {
                Id = user.Id,
                Username = user.UserName,
                Name = user.Name,
                Email = user.Email,
                Surname = user.Surname,
                PhoneNumber = user.PhoneNumber
            };

            return updatedDto;
        }

    }
}
