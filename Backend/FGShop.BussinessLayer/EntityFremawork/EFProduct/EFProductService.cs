using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.EFProductDtos;
using FGShop.DtoLayer.SizeDtos;
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

		//Bu metotta ürünün rengine ait olan bedenleri listeleyeceğiz
		public async Task<List<ResultSizeDto>> GetByProductandColorIdResultAll(int productId, int colorId)
		{
			var data = await _context.producthasColorAndSizes
				.Where(x => x.ProducthasColor.ProductId == productId && x.ProducthasColor.ColorId == colorId)
				.Select( g => new ResultSizeDto
				{
					Id = g.SizeId,
					SizeName= g.Size.SizeName
				})
				.ToListAsync();
			return data;
				
		}

		public async Task<ResultEFProductDto> GetByProductIdResultAll(int id)
		{
			var product = await _context.Products
				.Include(p => p.ProducthasColors) // Ara tabloyu dahil ediyoruz
					.ThenInclude(pc => pc.Color)  // Renk bilgilerini alıyoruz   // Boyut bilgilerini alıyoruz 
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
					ColorName = pc.Color.ColorName,

				}).ToList(),
			};
		}


	}
}
