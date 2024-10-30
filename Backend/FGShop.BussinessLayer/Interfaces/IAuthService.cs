using FGShop.CommanLayer;
using FGShop.DtoLayer.AuthDtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IAuthService
    {
        Task<Response<UserRegisterDto>> Register(UserRegisterDto dto);
        Task<Response<UserLoginDto>> Login(UserLoginDto dto);
        Task<List<UserListDto>> ListUsers();
        Task<UserListDto> GetUserList(int id);

    }
}
