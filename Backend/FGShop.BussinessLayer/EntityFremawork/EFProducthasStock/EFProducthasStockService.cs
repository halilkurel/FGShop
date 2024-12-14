using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.EFProducthasColorAndSizeAndStockDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProducthasStock
{
    public class EFProducthasStockService : IEFProducthasStockService
    {
        private readonly FGShopContext _context;

        public EFProducthasStockService(FGShopContext context)
        {
            _context = context;
        }

        public async Task<int> GetByProductId(int productId)
        {
            var totalStock = await _context.producthasColorAndSizehasStocks
                .Where(x => x.ProducthasColorAndSize.ProducthasColor.Product.Id == productId)
                .SumAsync(a => a.Stock);

            return totalStock;


        }

        //Admin panelinde stok güncelleme sayfası için 
        public async Task<List<ResultEFProducthasColorAndSizeDto>> GetByProductIdAndColorSizeStock(int productId)
        {
            var data = await _context.producthasColorAndSizehasStocks
                .Where(x => x.ProducthasColorAndSize.ProducthasColor.ProductId == productId)
                .Select(g => new ResultEFProducthasColorAndSizeDto
                {
                    Id = g.Id,
                    ProducthasColorAndSizeId = g.ProducthasColorAndSizeId,
                    ColorName= g.ProducthasColorAndSize.ProducthasColor.Color.ColorName,
                    Stock= g.Stock,
                    SizeName= g.ProducthasColorAndSize.Size.SizeName
                }).ToListAsync();
            return data;
        }
    }
}
