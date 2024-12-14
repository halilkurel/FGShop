using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.EFProducthasColorAndSizeDto;
using FGShop.DtoLayer.ProducthasColorAndProducthasSizeDtos;
using FGShop.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProducthasColorAndSize
{
    public class EFProducthasColorAndSizeService : IEFProducthasColorAndSizeService
    {
        private readonly FGShopContext _context;

        public EFProducthasColorAndSizeService(FGShopContext context)
        {
            _context = context;
        }

        public async Task<List<EFResultEFProducthasColorAndSizeDto>> GetByProductId(int productId)
        {
            var result = await _context.producthasColorAndSizes
                .Where( x=> x.ProducthasColor.ProductId == productId )
                .Select(y => new EFResultEFProducthasColorAndSizeDto
                {
                    ProductId = productId,
                    ColorName = y.ProducthasColor.Color.ColorName,
                    SizeNames= y.Size.SizeName,
                    ProducthasColorId= y.ProducthasColor.ColorId,
                    ProducthasColorAndSizeId = y.Id
                }).ToListAsync();


            return result;
        }

        public async Task<int> LastAddedDataId(int productId)
        {
            var lastAddedId = await _context.producthasColorAndSizes
                .Where(x => x.ProducthasColor.ProductId == productId) // İlgili ürün filtrelemesi
                .OrderByDescending(x => x.Id) // En büyük Id'ye göre sıralama
                .Select(x => x.Id) // Sadece Id'yi seç
                .FirstOrDefaultAsync(); // İlk Id'yi al (yoksa 0 döner)

            return lastAddedId;
        }

    }
}
