using FGShop.DtoLayer.UserAddressDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFUserAddress
{
    public interface IEFUserAddressService
    {
        Task<ResultUserAddressDto> GetByUserId(int userId);
    }
}
