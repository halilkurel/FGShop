using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.AuthDtos;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.EFOrderDtos;
using FGShop.DtoLayer.OrderDtos;
using FGShop.DtoLayer.ProductDtos;
using FGShop.DtoLayer.SizeDtos;
using FGShop.DtoLayer.StatusDtos;
using FGShop.DtoLayer.UserAddressDtos;
using FGShop.EntityLayer.Entities;
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
            // İlgili tabloya yalnızca gerekli olan sorguyu gönderiyoruz
            var data = await _context.Orders
                .Where(order => order.Id == orderId)
                .Select(order => new ResultJoinOrderDto
                {
                    Id = order.Id,
                    Address = order.Address,
                    City = order.City,
                    ColorName = order.ColorName,
                    Country = order.Country,
                    District = order.District,
                    Email = order.Email,
                    Neighbourhood = order.Neighbourhood,
                    OrderDate = order.OrderDate,
                    OrderQuantity = order.OrderQuantity,
                    PhoneNumber = order.PhoneNumber,
                    ProductName = order.ProductName,
                    SizeName = order.SizeName,
                    UserName = order.UserName,
                    StatusId = order.StatusId,
                    ColorId= order.ColorId,
                    SizeId = order.SizeId,
                    StatusName = order.Status.StatusName,
                    FirstName = _context.Users
                        .Where(user => user.UserName == order.UserName)
                        .Select(user => user.Name)
                        .FirstOrDefault(),
                    SurName = _context.Users
                        .Where(user => user.UserName == order.UserName)
                        .Select(user => user.Surname)
                        .FirstOrDefault(),
                    Price = _context.Products
                        .Where(product => product.Id == order.ProductId)
                        .Select(product => product.Price)
                        .FirstOrDefault(),
                    CoverPhoto = _context.Products
                        .Where(product => product.Id == order.ProductId)
                        .Select(product => product.CoverPhoto)
                        .FirstOrDefault(),
                    ProductId = order.ProductId
                })
                .FirstOrDefaultAsync();

            return data;
        }

        public async Task<List<ResultJoinOrderDto>> GetByUserNameOrders(string userName)
        {

            var data = await _context.Orders
                .Where(order => order.UserName == userName)
                .Select(order => new ResultJoinOrderDto
                {
                    Id = order.Id,
                    Address = order.Address,
                    City = order.City,
                    ColorName = order.ColorName,
                    Country = order.Country,
                    District = order.District,
                    Email = order.Email,
                    Neighbourhood = order.Neighbourhood,
                    OrderDate = order.OrderDate,
                    OrderQuantity = order.OrderQuantity,
                    PhoneNumber = order.PhoneNumber,
                    ProductName = order.ProductName,
                    SizeName = order.SizeName,
                    UserName = order.UserName,
                    StatusId = order.StatusId,
                    ColorId = order.ColorId,
                    SizeId = order.SizeId,
                    StatusName = order.Status.StatusName,
                    FirstName = _context.Users
                        .Where(user => user.UserName == order.UserName)
                        .Select(user => user.Name)
                        .FirstOrDefault(),
                    SurName = _context.Users
                        .Where(user => user.UserName == order.UserName)
                        .Select(user => user.Surname)
                        .FirstOrDefault(),
                    Price = _context.Products
                        .Where(product => product.Id == order.ProductId)
                        .Select(product => product.Price)
                        .FirstOrDefault(),
                    CoverPhoto = _context.Products
                        .Where(product => product.Id == order.ProductId)
                        .Select(product => product.CoverPhoto)
                        .FirstOrDefault(),
                    ProductId = order.ProductId
                })
                .OrderBy(order => order.StatusId)
                .ToListAsync();

            return data;
        }

        public async Task<List<ResultJoinOrderDto>> ListApprovedOrders()
        {
            var user = await _context.Users.ToListAsync();
            var product = await _context.Products.ToListAsync();
            var status = await _context.Statuses.ToListAsync();


            var orders = await _context.Orders.ToListAsync();
            var data = orders
                .Where(y => y.StatusId == 5)
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
                    ColorId = g.ColorId,
                    SizeId = g.SizeId,
                    SizeName = g.SizeName,
                    UserName = g.UserName,
                    StatusId = g.StatusId,
                    StatusName = status.FirstOrDefault(r => r.Id == g.StatusId)?.StatusName,
                    Price = product.FirstOrDefault(x => x.Id == g.ProductId)?.Price,
                    CoverPhoto = product.FirstOrDefault(c => c.Id == g.ProductId)?.CoverPhoto,
                    ProductId = g.ProductId,
                    FirstName = user.FirstOrDefault(f => f.UserName == g.UserName)?.Name,
                    SurName = user.FirstOrDefault(f => f.UserName == g.UserName)?.Surname,
                }).ToList();

            return data;

        }

        public async Task<List<ResultJoinOrderDto>> ListCancelledOrders()
        {


            var user = await _context.Users.ToListAsync();
            var product = await _context.Products.ToListAsync();
            var status = await _context.Statuses.ToListAsync();


            var orders = await _context.Orders.ToListAsync();
            var data = orders
                .Where(y => y.StatusId == 7)
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
                ColorId = g.ColorId,
                SizeId = g.SizeId,
                SizeName = g.SizeName,
                UserName = g.UserName,
                StatusId = g.StatusId,
                StatusName = status.FirstOrDefault(r => r.Id == g.StatusId)?.StatusName,
                Price = product.FirstOrDefault(x => x.Id == g.ProductId)?.Price,
                CoverPhoto = product.FirstOrDefault(c => c.Id == g.ProductId)?.CoverPhoto,
                ProductId = g.ProductId,
                FirstName = user.FirstOrDefault(f => f.UserName == g.UserName)?.Name,
                SurName = user.FirstOrDefault(f => f.UserName == g.UserName)?.Surname,
            }).ToList();

            return data;


           
        }

        public async Task<List<ResultJoinOrderDto>> ListOrderCompleted()
        {
            var user = await _context.Users.ToListAsync();
            var product = await _context.Products.ToListAsync();
            var status = await _context.Statuses.ToListAsync();


            var orders = await _context.Orders.ToListAsync();
            var data = orders
                .Where(y => y.StatusId == 6)
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
                    ColorId = g.ColorId,
                    SizeId = g.SizeId,
                    SizeName = g.SizeName,
                    UserName = g.UserName,
                    StatusId = g.StatusId,
                    StatusName = status.FirstOrDefault(r => r.Id == g.StatusId)?.StatusName,
                    Price = product.FirstOrDefault(x => x.Id == g.ProductId)?.Price,
                    CoverPhoto = product.FirstOrDefault(c => c.Id == g.ProductId)?.CoverPhoto,
                    ProductId = g.ProductId,
                    FirstName = user.FirstOrDefault(f => f.UserName == g.UserName)?.Name,
                    SurName = user.FirstOrDefault(f => f.UserName == g.UserName)?.Surname,
                }).ToList();

            return data;

        }


        public async Task<List<ResultJoinOrderDto>> ListUnapprovedOrders()
        {
            var user = await _context.Users.ToListAsync();
            var product = await _context.Products.ToListAsync();
            var status = await _context.Statuses.ToListAsync();


            var orders = await _context.Orders.ToListAsync();
            var data = orders
                .Where(y => y.StatusId == 4)
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
                    ColorId = g.ColorId,
                    SizeId = g.SizeId,
                    SizeName = g.SizeName,
                    UserName = g.UserName,
                    StatusId = g.StatusId,
                    StatusName = status.FirstOrDefault(r => r.Id == g.StatusId)?.StatusName,
                    Price = product.FirstOrDefault(x => x.Id == g.ProductId)?.Price,
                    CoverPhoto = product.FirstOrDefault(c => c.Id == g.ProductId)?.CoverPhoto,
                    ProductId = g.ProductId,
                    FirstName = user.FirstOrDefault(f => f.UserName == g.UserName)?.Name,
                    SurName = user.FirstOrDefault(f => f.UserName == g.UserName)?.Surname,
                }).ToList();

            return data;

        }

        public async Task<List<ResultJoinOrderDto>> OrderList()
        {
			var user = await _context.Users.ToListAsync();
			var product = await _context.Products.ToListAsync();
            var status = await _context.Statuses.ToListAsync();


            var orders = await _context.Orders.ToListAsync();
            var data = orders.Select(g => new ResultJoinOrderDto
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
                ColorId = g.ColorId,
                SizeId = g.SizeId,
                SizeName = g.SizeName,
                UserName = g.UserName,
                StatusId = g.StatusId,
                StatusName = status.FirstOrDefault(r => r.Id == g.StatusId)?.StatusName,
                Price = product.FirstOrDefault(x => x.Id == g.ProductId)?.Price,
                CoverPhoto = product.FirstOrDefault(c => c.Id == g.ProductId)?.CoverPhoto,
                ProductId = g.ProductId,
                FirstName = user.FirstOrDefault(f => f.UserName == g.UserName)?.Name,
                SurName = user.FirstOrDefault(f => f.UserName == g.UserName)?.Surname,
            }).ToList();

            return data;

        }

        public async Task<List<ResultJoinOrderDto>> Last4OrderList()
        {
            var user = await _context.Users.ToListAsync();
            var product = await _context.Products.ToListAsync();
            var status = await _context.Statuses.ToListAsync();
            var orders = await _context.Orders.OrderByDescending(o => o.OrderDate)
                .Take(4)
                .ToListAsync(); // Sıralama işlemi burada yapılıyor

            var data = orders.Select(g => new ResultJoinOrderDto
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
                ColorId = g.ColorId,
                SizeId = g.SizeId,
                SizeName = g.SizeName,
                UserName = g.UserName,
                StatusId = g.StatusId,
                StatusName = status.FirstOrDefault(r => r.Id == g.StatusId)?.StatusName,
                Price = product.FirstOrDefault(x => x.Id == g.ProductId)?.Price,
                CoverPhoto = product.FirstOrDefault(c => c.Id == g.ProductId)?.CoverPhoto,
                ProductId = g.ProductId,
                FirstName = user.FirstOrDefault(f => f.UserName == g.UserName)?.Name,
                SurName = user.FirstOrDefault(f => f.UserName == g.UserName)?.Surname,
            }).ToList();

            return data;

        }
    }
}
