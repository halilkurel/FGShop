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
        string TokenCreate(string userName, string role);
    }
}
