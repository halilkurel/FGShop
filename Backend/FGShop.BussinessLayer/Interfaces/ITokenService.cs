using FGShop.DtoLayer.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface ITokenService
    {
        string TokenCreateAdmin(UserLoginDto dto);
        string TokenCreateUser(UserLoginDto dto);
    }
}
