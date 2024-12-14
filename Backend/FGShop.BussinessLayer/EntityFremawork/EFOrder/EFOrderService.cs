using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.AuthDtos;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.EFOrderDtos;
using FGShop.DtoLayer.OrderDtos;
using FGShop.DtoLayer.ProductDtos;
using FGShop.DtoLayer.SizeDtos;
using FGShop.DtoLayer.StatusDtos;
using FGShop.DtoLayer.UserAddressDtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFOrder
{
    public class EFOrderService : IEFOrderService
    {
        private readonly FGShopContext _context;

        public EFOrderService(FGShopContext context)
        {
            _context = context;
        }

        public async Task<ResultJoinOrderDto> GetByOrderId(int orderId)
        {
            var data = _context.Orders
                .Where(x => x.Id == orderId)
                .Select( g => new ResultJoinOrderDto
                {
                    Id = g.Id,
                    Address = g.Address,
                    City = g.City,
                    ColorName = g.ColorName,
                    Country = g.Country,
                    District = g.District,
                    Email = g.Email,
                    Neighbourhood = g.Neighbourhood,
                    OrderDate = g.OrderDate,
                    OrderQuantity = g.OrderQuantity,
                    PhoneNumber = g.PhoneNumber,
                    ProductName = g.ProductName,
                    SizeName = g.SizeName,
                    UserName = g.UserName,
                    StatusId = g.StatusId,
                    StatusName = g.Status.StatusName
                    
                })
                .FirstOrDefault();

            return data;
        }

        public async Task<List<ResultJoinOrderDto>> ListApprovedOrders()
        {
            var data = await _context.Orders
                .Where(x => x.StatusId == 5)
				.Select(g => new ResultJoinOrderDto
				{
					Id = g.Id,
					Address = g.Address,
					City = g.City,
					ColorName = g.ColorName,
					Country = g.Country,
					District = g.District,
					Email = g.Email,
					Neighbourhood = g.Neighbourhood,
					OrderDate = g.OrderDate,
					OrderQuantity = g.OrderQuantity,
					PhoneNumber = g.PhoneNumber,
					ProductName = g.ProductName,
					SizeName = g.SizeName,
					UserName = g.UserName,
					StatusId = g.StatusId,
					StatusName = g.Status.StatusName

				})
				.ToListAsync();


			return data;
        }

        public async Task<List<ResultJoinOrderDto>> ListCancelledOrders()
        {
			var data = await _context.Orders
				.Where(x => x.StatusId == 7)
				.Select(g => new ResultJoinOrderDto
				{
					Id = g.Id,
					Address = g.Address,
					City = g.City,
					ColorName = g.ColorName,
					Country = g.Country,
					District = g.District,
					Email = g.Email,
					Neighbourhood = g.Neighbourhood,
					OrderDate = g.OrderDate,
					OrderQuantity = g.OrderQuantity,
					PhoneNumber = g.PhoneNumber,
					ProductName = g.ProductName,
					SizeName = g.SizeName,
					UserName = g.UserName,
					StatusId = g.StatusId,
					StatusName = g.Status.StatusName

				})
				.ToListAsync();


			return data;
		}

        public async Task<List<ResultJoinOrderDto>> ListOrderCompleted()
        {
			var data = await _context.Orders
				.Where(x => x.StatusId == 6)
				.Select(g => new ResultJoinOrderDto
				{
					Id = g.Id,
					Address = g.Address,
					City = g.City,
					ColorName = g.ColorName,
					Country = g.Country,
					District = g.District,
					Email = g.Email,
					Neighbourhood = g.Neighbourhood,
					OrderDate = g.OrderDate,
					OrderQuantity = g.OrderQuantity,
					PhoneNumber = g.PhoneNumber,
					ProductName = g.ProductName,
					SizeName = g.SizeName,
					UserName = g.UserName,
					StatusId = g.StatusId,
					StatusName = g.Status.StatusName

				})
				.ToListAsync();


			return data;
		}

        
        public async Task<List<ResultJoinOrderDto>> ListUnapprovedOrders()
        {
			var data = await _context.Orders
				.Where(x => x.StatusId == 4)
				.Select(g => new ResultJoinOrderDto
				{
					Id = g.Id,
					Address = g.Address,
					City = g.City,
					ColorName = g.ColorName,
					Country = g.Country,
					District = g.District,
					Email = g.Email,
					Neighbourhood = g.Neighbourhood,
					OrderDate = g.OrderDate,
					OrderQuantity = g.OrderQuantity,
					PhoneNumber = g.PhoneNumber,
					ProductName = g.ProductName,
					SizeName = g.SizeName,
					UserName = g.UserName,
					StatusId = g.StatusId,
					StatusName = g.Status.StatusName

				})
				.ToListAsync();


			return data;
		}

        public async Task<List<ResultJoinOrderDto>> OrderList()
        {
			var data = await _context.Orders
				.Select(g => new ResultJoinOrderDto
				{
					Id = g.Id,
					Address = g.Address,
					City = g.City,
					ColorName = g.ColorName,
					Country = g.Country,
					District = g.District,
					Email = g.Email,
					Neighbourhood = g.Neighbourhood,
					OrderDate = g.OrderDate,
					OrderQuantity = g.OrderQuantity,
					PhoneNumber = g.PhoneNumber,
					ProductName = g.ProductName,
					SizeName = g.SizeName,
					UserName = g.UserName,
					StatusId = g.StatusId,
					StatusName = g.Status.StatusName

				})
				.ToListAsync();


			return data;
		}

    }
}
