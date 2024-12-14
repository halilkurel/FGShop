using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.EFProducthasColorAndSizeDto;
using FGShop.DtoLayer.ProducthasColorAndProducthasSizeDtos;
using FGShop.DtoLayer.SizeDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProducthasSize
{
    public class EFProducthasSizeService : IEFProducthasSizeService
    {
        private readonly FGShopContext _context;

        public EFProducthasSizeService(FGShopContext context)
        {
            _context = context;
        }

        public async Task<List<ResultSizeDto>> GetByProductId(int productId)
        {
            var result = await _context.producthasColorAndSizes
                .Where(x => x.ProducthasColor.ProductId == productId)
                .GroupBy(y => new { y.SizeId, y.Size.SizeName }) // Benzersiz grupla
                .Select(g => new ResultSizeDto
                {
                    
                    Id = g.Key.SizeId,
                    SizeName = g.Key.SizeName
                })
                .ToListAsync();

            return result;
        }


    }
}
