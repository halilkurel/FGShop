using FGShop.CommanLayer;
using FGShop.DtoLayer.SliderDtos;
using FGShop.DtoLayer.UserAddressDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IUserAddressService
    {
        Task<IResponse<List<ResultUserAddressDto>>> GetAll();
        Task<IResponse<CreateUserAddressDto>> Create(CreateUserAddressDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateUserAddressDto>> Update(UpdateUserAddressDto dto);
    }
}
