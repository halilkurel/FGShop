using AutoMapper;
using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.ProducthasStockDtos;
using FGShop.DtoLayer.StockDtos;
using FGShop.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProducthasStock
{
    public class EFProducthasStockService : IEFProducthasStockService
    {
        private readonly FGShopContext _context;
        private readonly IMapper _mapper;

        public EFProducthasStockService(FGShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultProducthasStockDto> GetByProductIdProducthasStockList(int id)
        {
            var producthasStockList = await _context.Set<ProducthasStock>()
                                        .Where(x => x.ProductId == id).ToListAsync();

            var list = _mapper.Map<ResultProducthasStockDto>(producthasStockList.FirstOrDefault());
            return list;
        }

        public async Task<List<ResultStockDto>> GetByProductIdStock(int id)
        {

            var producthasStockList = await _context.Set<ProducthasStock>()
                                                    .Where(x => x.ProductId == id).ToListAsync();
                                                    

            var stockIds = producthasStockList.Select(x => x.StockId).ToList();


            var stocks = await _context.Set<Stock>()
                                        .Where(image => stockIds.Contains(image.Id))
                                        .ToListAsync();


            var list = _mapper.Map<List<ResultStockDto>>(stocks);
            return list;
        }
    }
}
