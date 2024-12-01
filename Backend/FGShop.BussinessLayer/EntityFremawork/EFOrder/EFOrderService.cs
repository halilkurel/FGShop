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
            var data = await _context.Orders.Where(x => x.Id == orderId)
                .Include(x => x.User)
                .Include(x => x.UserAddress)
                .Include(x => x.Color)
                .Include(x => x.Size)
                .Include(x => x.Product)
                .Include(x => x.Stasus)
                .ToListAsync();

            var result = data.Select(x => new ResultJoinOrderDto
            {
                GetByOrderId = new GetByOrderIdDto
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    SizeId = x.SizeId,
                    OrderQuantity = x.OrderQuantity,
                    ColorId = x.ColorId,
                    StatusId = x.StatusId,
                    UserAddressId = x.UserAddressId,
                    UserId = x.UserId
                },
                GetByProductId = x.Product != null ? new GetByProductIdDto
                {
                    CoverPhoto = x.Product.CoverPhoto,
                    Description = x.Product.Description,
                    Description2 = x.Product.Description2,
                    Id = x.Product.Id,
                    Price = x.Product.Price,
                    ProductName = x.Product.ProductName
                } : null,
                UserList = x.User != null ? new UserListDto
                {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    Name = x.User.Name,
                    PhoneNumber = x.User.PhoneNumber,
                    Surname = x.User.Surname,
                    Username = x.User.UserName

                } : null,
                ResultUserAddress = x.UserAddress != null ? new ResultUserAddressDto
                {
                    Id = x.UserAddress.Id,
                    Email = x.UserAddress.Email,
                    PhoneNumber = x.UserAddress.PhoneNumber,
                    Address = x.UserAddress.Address,
                    City = x.UserAddress.City,
                    Country = x.UserAddress.Country,
                    District = x.UserAddress.District,
                    Neighbourhood = x.UserAddress.Neighbourhood,
                    UserId = x.UserAddress.UserId
                } : null,
                GetBySizeId = x.Size != null ? new GetBySizeIdDto
                {
                    Id = x.Size.Id,
                    SizeName = x.Size.SizeName
                } : null,
                GetByColorId = x.Color != null ? new GetByColorIdDto
                {
                    Id = x.Color.Id,
                    ColorName = x.Color.ColorName
                } : null,
                ResultStatus = new ResultStatusDto
                {
                    Id = x.StatusId,
                    StatusName = x.Stasus.StatusName
                }
            }).FirstOrDefault();

            return result;
        }

        public async Task<List<ResultJoinOrderDto>> ListApprovedOrders()
        {
            var data = await _context.Orders.Where(x => x.StatusId == 5)
                .Include(x => x.User)
                .Include(x => x.UserAddress)
                .Include(x => x.Color)
                .Include(x => x.Size)
                .Include(x => x.Product)
                .Include(x => x.Stasus)
                .ToListAsync();

            var result = data.Select(x => new ResultJoinOrderDto
            {
                GetByOrderId = new GetByOrderIdDto
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    SizeId = x.SizeId,
                    OrderQuantity = x.OrderQuantity,
                    ColorId = x.ColorId,
                    StatusId = x.StatusId,
                    UserAddressId = x.UserAddressId,
                    UserId = x.UserId
                },
                GetByProductId = x.Product != null ? new GetByProductIdDto
                {
                    CoverPhoto = x.Product.CoverPhoto,
                    Description = x.Product.Description,
                    Description2 = x.Product.Description2,
                    Id = x.Product.Id,
                    Price = x.Product.Price,
                    ProductName = x.Product.ProductName
                } : null,
                UserList = x.User != null ? new UserListDto
                {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    Name = x.User.Name,
                    PhoneNumber = x.User.PhoneNumber,
                    Surname = x.User.Surname,
                    Username = x.User.UserName

                } : null,
                ResultUserAddress = x.UserAddress != null ? new ResultUserAddressDto
                {
                    Id = x.UserAddress.Id,
                    Email = x.UserAddress.Email,
                    PhoneNumber = x.UserAddress.PhoneNumber,
                    Address = x.UserAddress.Address,
                    City = x.UserAddress.City,
                    Country = x.UserAddress.Country,
                    District = x.UserAddress.District,
                    Neighbourhood = x.UserAddress.Neighbourhood,
                    UserId = x.UserAddress.UserId
                } : null,
                GetBySizeId = x.Size != null ? new GetBySizeIdDto
                {
                    Id = x.Size.Id,
                    SizeName = x.Size.SizeName
                } : null,
                GetByColorId = x.Color != null ? new GetByColorIdDto
                {
                    Id = x.Color.Id,
                    ColorName = x.Color.ColorName
                } : null,
                ResultStatus = new ResultStatusDto
                {
                    Id = x.StatusId,
                    StatusName = x.Stasus.StatusName
                }
            }).ToList();

            return result;
        }

        public async Task<List<ResultJoinOrderDto>> ListCancelledOrders()
        {
            var data = await _context.Orders.Where(x => x.StatusId == 7)
                .Include(x => x.User)
                .Include(x => x.UserAddress)
                .Include(x => x.Color)
                .Include(x => x.Size)
                .Include(x => x.Product)
                .Include(x => x.Stasus)
                .ToListAsync();

            var result = data.Select(x => new ResultJoinOrderDto
            {
                GetByOrderId = new GetByOrderIdDto
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    SizeId = x.SizeId,
                    OrderQuantity = x.OrderQuantity,
                    ColorId = x.ColorId,
                    StatusId = x.StatusId,
                    UserAddressId = x.UserAddressId,
                    UserId = x.UserId
                },
                GetByProductId = x.Product != null ? new GetByProductIdDto
                {
                    CoverPhoto = x.Product.CoverPhoto,
                    Description = x.Product.Description,
                    Description2 = x.Product.Description2,
                    Id = x.Product.Id,
                    Price = x.Product.Price,
                    ProductName = x.Product.ProductName
                } : null,
                UserList = x.User != null ? new UserListDto
                {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    Name = x.User.Name,
                    PhoneNumber = x.User.PhoneNumber,
                    Surname = x.User.Surname,
                    Username = x.User.UserName

                } : null,
                ResultUserAddress = x.UserAddress != null ? new ResultUserAddressDto
                {
                    Id = x.UserAddress.Id,
                    Email = x.UserAddress.Email,
                    PhoneNumber = x.UserAddress.PhoneNumber,
                    Address = x.UserAddress.Address,
                    City = x.UserAddress.City,
                    Country = x.UserAddress.Country,
                    District = x.UserAddress.District,
                    Neighbourhood = x.UserAddress.Neighbourhood,
                    UserId = x.UserAddress.UserId
                } : null,
                GetBySizeId = x.Size != null ? new GetBySizeIdDto
                {
                    Id = x.Size.Id,
                    SizeName = x.Size.SizeName
                } : null,
                GetByColorId = x.Color != null ? new GetByColorIdDto
                {
                    Id = x.Color.Id,
                    ColorName = x.Color.ColorName
                } : null,
                ResultStatus = new ResultStatusDto
                {
                    Id = x.StatusId,
                    StatusName = x.Stasus.StatusName
                }
            }).ToList();

            return result;
        }

        public async Task<List<ResultJoinOrderDto>> ListOrderCompleted()
        {
            var data = await _context.Orders.Where(x => x.StatusId == 6)
                .Include(x => x.User)
                .Include(x => x.UserAddress)
                .Include(x => x.Color)
                .Include(x => x.Size)
                .Include(x => x.Product)
                .Include(x => x.Stasus)
                .ToListAsync();

            var result = data.Select(x => new ResultJoinOrderDto
            {
                GetByOrderId = new GetByOrderIdDto
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    SizeId = x.SizeId,
                    OrderQuantity = x.OrderQuantity,
                    ColorId = x.ColorId,
                    StatusId = x.StatusId,
                    UserAddressId = x.UserAddressId,
                    UserId = x.UserId
                },
                GetByProductId = x.Product != null ? new GetByProductIdDto
                {
                    CoverPhoto = x.Product.CoverPhoto,
                    Description = x.Product.Description,
                    Description2 = x.Product.Description2,
                    Id = x.Product.Id,
                    Price = x.Product.Price,
                    ProductName = x.Product.ProductName
                } : null,
                UserList = x.User != null ? new UserListDto
                {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    Name = x.User.Name,
                    PhoneNumber = x.User.PhoneNumber,
                    Surname = x.User.Surname,
                    Username = x.User.UserName

                } : null,
                ResultUserAddress = x.UserAddress != null ? new ResultUserAddressDto
                {
                    Id = x.UserAddress.Id,
                    Email = x.UserAddress.Email,
                    PhoneNumber = x.UserAddress.PhoneNumber,
                    Address = x.UserAddress.Address,
                    City = x.UserAddress.City,
                    Country = x.UserAddress.Country,
                    District = x.UserAddress.District,
                    Neighbourhood = x.UserAddress.Neighbourhood,
                    UserId = x.UserAddress.UserId
                } : null,
                GetBySizeId = x.Size != null ? new GetBySizeIdDto
                {
                    Id = x.Size.Id,
                    SizeName = x.Size.SizeName
                } : null,
                GetByColorId = x.Color != null ? new GetByColorIdDto
                {
                    Id = x.Color.Id,
                    ColorName = x.Color.ColorName
                } : null,
                ResultStatus = new ResultStatusDto
                {
                    Id = x.StatusId,
                    StatusName = x.Stasus.StatusName
                }
            }).ToList();

            return result;
        }

        
        public async Task<List<ResultJoinOrderDto>> ListUnapprovedOrders()
        {
            var data = await _context.Orders.Where(x => x.StatusId == 4)
                .Include(x => x.User)
                .Include(x => x.UserAddress)
                .Include(x => x.Color)
                .Include(x => x.Size)
                .Include(x => x.Product)
                .Include(x => x.Stasus)
                .ToListAsync();

            var result = data.Select(x => new ResultJoinOrderDto
            {
                GetByOrderId = new GetByOrderIdDto
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    SizeId = x.SizeId,
                    OrderQuantity = x.OrderQuantity,
                    ColorId = x.ColorId,
                    StatusId = x.StatusId,
                    UserAddressId = x.UserAddressId,
                    UserId = x.UserId
                },
                GetByProductId = x.Product != null ? new GetByProductIdDto
                {
                    CoverPhoto = x.Product.CoverPhoto,
                    Description = x.Product.Description,
                    Description2 = x.Product.Description2,
                    Id = x.Product.Id,
                    Price = x.Product.Price,
                    ProductName = x.Product.ProductName
                } : null,
                UserList = x.User != null ? new UserListDto
                {
                    Id = x.User.Id,
                    Email = x.User.Email,
                    Name = x.User.Name,
                    PhoneNumber = x.User.PhoneNumber,
                    Surname = x.User.Surname,
                    Username = x.User.UserName

                } : null,
                ResultUserAddress = x.UserAddress != null ? new ResultUserAddressDto
                {
                    Id = x.UserAddress.Id,
                    Email = x.UserAddress.Email,
                    PhoneNumber = x.UserAddress.PhoneNumber,
                    Address = x.UserAddress.Address,
                    City = x.UserAddress.City,
                    Country = x.UserAddress.Country,
                    District = x.UserAddress.District,
                    Neighbourhood = x.UserAddress.Neighbourhood,
                    UserId = x.UserAddress.UserId
                } : null,
                GetBySizeId = x.Size != null ? new GetBySizeIdDto
                {
                    Id = x.Size.Id,
                    SizeName = x.Size.SizeName
                } : null,
                GetByColorId = x.Color != null ? new GetByColorIdDto
                {
                    Id = x.Color.Id,
                    ColorName = x.Color.ColorName
                } : null,
                ResultStatus = new ResultStatusDto
                {
                    Id = x.StatusId,
                    StatusName = x.Stasus.StatusName
                }
            }).ToList();

            return result;
        }

        public async Task<List<ResultJoinOrderDto>> OrderList()
        {
            var data = await _context.Orders
                .Include(x => x.User)
                .Include(x => x.UserAddress)
                .Include(x => x.Color)
                .Include(x => x.Size)
                .Include(x => x.Product)
                .Include(x => x.Stasus)
                .ToListAsync();

            var result = data.Select(x => new ResultJoinOrderDto
            {
                GetByOrderId = new GetByOrderIdDto
                {
                    Id = x.Id,
                    OrderDate = x.OrderDate,
                    SizeId = x.SizeId,
                    OrderQuantity = x.OrderQuantity,
                    ColorId = x.ColorId,
                    StatusId = x.StatusId,
                    UserAddressId = x.UserAddressId,
                    UserId = x.UserId
                },
                GetByProductId = x.Product != null ? new GetByProductIdDto
                {
                    CoverPhoto = x.Product.CoverPhoto,
                    Description = x.Product.Description,
                    Description2 = x.Product.Description2,
                    Id = x.Product.Id,
                    Price = x.Product.Price,
                    ProductName = x.Product.ProductName
                } : null,
                UserList = x.User != null ? new UserListDto
                {
                    Id= x.User.Id,
                    Email = x.User.Email,
                    Name = x.User.Name,
                    PhoneNumber = x.User.PhoneNumber,
                    Surname = x.User.Surname,
                    Username = x.User.UserName

                } : null,
                ResultUserAddress = x.UserAddress != null ? new ResultUserAddressDto
                {
                    Id = x.UserAddress.Id,
                    Email = x.UserAddress.Email,
                    PhoneNumber= x.UserAddress.PhoneNumber,
                    Address = x.UserAddress.Address,
                    City = x.UserAddress.City,
                    Country = x.UserAddress.Country,
                    District = x.UserAddress.District,
                    Neighbourhood = x.UserAddress.Neighbourhood,
                    UserId = x.UserAddress.UserId
                } : null,
                GetBySizeId = x.Size != null ? new GetBySizeIdDto
                {
                    Id = x.Size.Id,
                    SizeName = x.Size.SizeName
                } : null,
                GetByColorId = x.Color != null ? new GetByColorIdDto
                {
                    Id = x.Color.Id,
                    ColorName = x.Color.ColorName
                } : null,
                ResultStatus = new ResultStatusDto
                {
                    Id = x.StatusId,
                    StatusName = x.Stasus.StatusName
                }
            }).ToList();

            return result;
        }

    }
}
