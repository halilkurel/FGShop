using AutoMapper;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.ImageDtos;
using FGShop.DtoLayer.ProducthasColorDtos;
using FGShop.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EfProducthasColor
{
    public class EfProducthasColorService : IEfProducthasColorService
    {
        private readonly FGShopContext _context;
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public EfProducthasColorService(FGShopContext context, IMapper mapper, IUow uow)
        {
            _context = context;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<List<ResultColorDto>> GetByProductIdColor(int id)
        {

            var producthasColorList = await _context.Set<ProducthasColor>()
                                                    .Where(x => x.ProductId == id)
                                                    .ToListAsync();

            var colorIds = producthasColorList.Select(x => x.ColorId).ToList();


            var colors = await _context.Set<Color>()
                                        .Where(image => colorIds.Contains(image.Id))
                                        .ToListAsync();


            var list = _mapper.Map<List<ResultColorDto>>(colors);
            return list;
        }

        public async Task<List<ResultProducthasColorDto>> GetByProductIdProducthasColorList(int id)
        {
            var producthasColorList = await _context.Set<ProducthasColor>()
                                                    .Where(x => x.ProductId == id)
                                                    .ToListAsync();
            var list = _mapper.Map<List<ResultProducthasColorDto>>(producthasColorList);
            return list;
        }
    }
}
