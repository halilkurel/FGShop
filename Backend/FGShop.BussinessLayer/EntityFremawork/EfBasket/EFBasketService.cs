using AutoMapper;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.BasketDtos;
using FGShop.DtoLayer.EFOrderDtos;
using FGShop.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EfOrder
{
	public class EFBasketService : IEFBasketService
	{
		private readonly FGShopContext _context;
		private readonly IMapper _mapper;
		private readonly IBasketService _basketService;

        public EFBasketService(FGShopContext context, IMapper mapper, IBasketService basketService)
        {
            _context = context;
            _mapper = mapper;
            _basketService = basketService;
        }

        public async Task<List<ResultBasketDto>> GetByUserId(int userId)
		{
			var basketData = await _context.Baskets.Where(x => x.UserId == userId).ToListAsync();
			var map = _mapper.Map<List<ResultBasketDto>>(basketData);
			return map;
		}

		public async Task<List<ResultEFOrderDto>> GetProductDetailByUserId(int userId)
		{
			var basketData = await _context.Baskets
				.Where(x => x.UserId == userId)
				.Include(b => b.Product)
				.Include(b => b.Color)
				.Include(b => b.Size)
				.ToListAsync();

			var resultEFOrderDtos = basketData.Select(basket => new ResultEFOrderDto
			{
				Id= basket.Id,
				ProductId = basket.ProductId,
				OrderQuantity = basket.OrderQuantity,
				Price = basket.Product.Price, 
				ColorId = basket.ColorId,
				SizeId = basket.SizeId,
				ProductName = basket.Product.ProductName,
				ColorName = basket.Color.ColorName,
				SizeName = basket.Size.SizeName,
				CoverPhoto = basket.Product.CoverPhoto
			}).ToList();

			return resultEFOrderDtos;
		}

        public async Task UserIdBasketRemove(int userId)
        {
            var basket = await _context.Baskets.Where(x => x.UserId == userId).ToListAsync();
			foreach (var basketData in basket)
			{
                _context.Baskets.Remove(basketData);
				_context.SaveChanges();
            }
			
        }
    }
}
