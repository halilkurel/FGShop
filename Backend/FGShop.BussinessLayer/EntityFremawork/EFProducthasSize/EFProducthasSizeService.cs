using AutoMapper;
using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.ProucthasSizeDtos;
using FGShop.DtoLayer.SizeDtos;
using FGShop.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProducthasSize
{
    public class EFProducthasSizeService: IEFProducthasSizeService
    {
        private readonly FGShopContext _context;
        private readonly IMapper _mapper;

        public EFProducthasSizeService(FGShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ResultSizeDto>> GetByProductIdSize(int id)
        {

            var producthasSizeList = await _context.Set<ProducthasSize>()
                                                    .Where(x => x.ProductId == id).ToListAsync();
                                                    

            var sizeIds = producthasSizeList.Select(x => x.SizeId).ToList();


            var sizes = await _context.Set<Size>()
                                        .Where(image => sizeIds.Contains(image.Id))
                                        .ToListAsync();


            var list = _mapper.Map<List<ResultSizeDto>>(sizes);
            return list;
        }

        public async Task<List<ResultProducthasSizeDto>> GetByProductIdSProducthasSizeList(int id)
        {
            var producthasSizeList = await _context.Set<ProducthasSize>()
                                        .Where(x => x.ProductId == id).ToListAsync();

            var list = _mapper.Map<List<ResultProducthasSizeDto>>(producthasSizeList);
            return list;
        }
    }
}
