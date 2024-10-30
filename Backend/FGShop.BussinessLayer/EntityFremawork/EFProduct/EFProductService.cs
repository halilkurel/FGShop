using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.EFProductDtos;
using FGShop.DtoLayer.SizeDtos;
using FGShop.DtoLayer.StockDtos;
using FGShop.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProduct
{
	public class EFProductService : IEFProductService
	{
		private readonly FGShopContext _context;

		public EFProductService(FGShopContext context)
		{
			_context = context;
		}

		public async Task<ResultEFProductDto> GetByProductIdResultAll(int id)
		{
			var product = await _context.Products
				.Include(p => p.ProducthasColors) // Ara tabloyu dahil ediyoruz
					.ThenInclude(pc => pc.Color)  // Renk bilgilerini alıyoruz
				.Include(p => p.producthasSizes)  // Ara tabloyu dahil ediyoruz
					.ThenInclude(ps => ps.Size)   // Boyut bilgilerini alıyoruz
				.Include(p => p.ProducthasStocks) // Ara tabloyu dahil ediyoruz
					.ThenInclude(ps => ps.Stock)  // Stok bilgilerini alıyoruz
				.FirstOrDefaultAsync(p => p.Id == id);

			if (product == null)
			{
				return null; // Eğer ürün bulunamazsa null döner
			}

			return new ResultEFProductDto
			{
				ProductId = product.Id,
				ProductName = product.ProductName,
				CoverPhoto = product.CoverPhoto,
				Price = product.Price,
				Description = product.Description,
				Description2 = product.Description2,
				Colors = product.ProducthasColors.Select(pc => new ResultColorDto
				{
					Id = pc.Color.Id,   // Ara tablo üzerinden Color entity'sine ulaşıyoruz
					ColorName = pc.Color.ColorName
				}).ToList(),
				Sizes = product.producthasSizes.Select(ps => new ResultSizeDto
				{
					Id = ps.Size.Id,      // Ara tablo üzerinden Size entity'sine ulaşıyoruz
					SizeName = ps.Size.SizeName
				}).ToList(),
				Stocks = product.ProducthasStocks.Select(ps => new ResultStockDto
				{
					Id = ps.Stock.Id,     // Ara tablo üzerinden Stock entity'sine ulaşıyoruz
					StockQuantity = ps.Stock.StockQuantity
				}).FirstOrDefault() // Stock Çoka çok tabloda olsada tek bir veri olarak geleceği için FirstorDefault kullandık
			};
		}


	}
}
