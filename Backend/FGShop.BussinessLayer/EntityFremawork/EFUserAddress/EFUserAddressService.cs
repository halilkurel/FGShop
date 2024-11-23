using AutoMapper;
using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.UserAddressDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFUserAddress
{
    public class EFUserAddressService : IEFUserAddressService
    {
        private readonly FGShopContext _context;
        private readonly IMapper _mapper;

        public EFUserAddressService(FGShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultUserAddressDto> GetByUserId(int userId)
        {
            var data = await _context.UserAddresses.Where(x => x.UserId == userId).FirstOrDefaultAsync();
            var result = _mapper.Map<ResultUserAddressDto>(data);
            return result;
        }
    }
}
