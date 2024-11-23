using AutoMapper;
using FGShop.BussinessLayer.EntityFremawork.EfOrder;
using FGShop.BussinessLayer.EntityFremawork.EFProduct;
using FGShop.BussinessLayer.EntityFremawork.EFProducthasCategory;
using FGShop.BussinessLayer.EntityFremawork.EfProducthasColor;
using FGShop.BussinessLayer.EntityFremawork.EFProducthasImage;
using FGShop.BussinessLayer.EntityFremawork.EFProducthasSize;
using FGShop.BussinessLayer.EntityFremawork.EFProducthasStock;
using FGShop.BussinessLayer.EntityFremawork.EFUserAddress;
using FGShop.BussinessLayer.Interfaces;
using FGShop.BussinessLayer.Mappings.AutoMapper;
using FGShop.BussinessLayer.Services;
using FGShop.BussinessLayer.ValidationRules.AboutValidationRules;
using FGShop.BussinessLayer.ValidationRules.AuthValidationRules;
using FGShop.BussinessLayer.ValidationRules.BasketValidationRules;
using FGShop.BussinessLayer.ValidationRules.CategoryValidationRules;
using FGShop.BussinessLayer.ValidationRules.ColorValidationRules;
using FGShop.BussinessLayer.ValidationRules.ContactValidationRules;
using FGShop.BussinessLayer.ValidationRules.ImageValidationRules;
using FGShop.BussinessLayer.ValidationRules.OrderValidationRules;
using FGShop.BussinessLayer.ValidationRules.ProducthasCategoryValidationRules;
using FGShop.BussinessLayer.ValidationRules.ProducthasColorValidationRules;
using FGShop.BussinessLayer.ValidationRules.ProducthasImageValidationRules;
using FGShop.BussinessLayer.ValidationRules.ProducthasSizeValidationRules;
using FGShop.BussinessLayer.ValidationRules.ProducthasStockValidationRules;
using FGShop.BussinessLayer.ValidationRules.ProductValidationRules;
using FGShop.BussinessLayer.ValidationRules.SizeValidationRules;
using FGShop.BussinessLayer.ValidationRules.SliderValidationRules;
using FGShop.BussinessLayer.ValidationRules.StockValidationRules;
using FGShop.BussinessLayer.ValidationRules.UserAddressValidationRules;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.AboutDtos;
using FGShop.DtoLayer.AuthDtos;
using FGShop.DtoLayer.BasketDtos;
using FGShop.DtoLayer.CategoryDtos;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.ContactDtos;
using FGShop.DtoLayer.ImageDtos;
using FGShop.DtoLayer.OrderDtos;
using FGShop.DtoLayer.ProductDtos;
using FGShop.DtoLayer.ProducthasCategoryDtos;
using FGShop.DtoLayer.ProducthasColorDtos;
using FGShop.DtoLayer.ProducthasImageDto;
using FGShop.DtoLayer.ProducthasStockDtos;
using FGShop.DtoLayer.ProucthasSizeDtos;
using FGShop.DtoLayer.SizeDtos;
using FGShop.DtoLayer.SliderDtos;
using FGShop.DtoLayer.StockDtos;
using FGShop.DtoLayer.UserAddressDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services) 
        {
            services.AddDbContext<FGShopContext>(opt =>
            {
                opt.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=FGShopProject;Trusted_Connection=True;TrustServerCertificate=True");
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<FGShopContext>()
                .AddDefaultTokenProviders();


            var configurations = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new FGShopProfile());
            });
            var mapper = configurations.CreateMapper();
            services.AddSingleton(mapper);

            services.AddScoped<IUow, Uow>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IIdentityRoleService, IdentityRoleService>();
            services.AddScoped<ITokenService, TokenService>();

            //Validation Kuraları
            services.AddTransient<IValidator<CreateProductDto>, CreateProductDtoValidator>();
            services.AddTransient<IValidator<UpdateProductDto>, UpdateProductDtoValidator>();

            services.AddTransient<IValidator<CreateStockDto>, CreateStockDtoValidator>();
            services.AddTransient<IValidator<UpdateStockDto>, UpdateStockDtoValidator>();

            services.AddTransient<IValidator<CreateProducthasStockDto>, CreateProducthasStockDtoValidator>();
            services.AddTransient<IValidator<UpdateProducthasStockDto>, UpdateProducthasStockDtoValidator>();

            services.AddTransient<IValidator<CreateCategoryDto>, CreateCategoryDtoValidator>();
            services.AddTransient<IValidator<UpdateCategoryDto>, UpdateCategoryDtoValidator>();

            services.AddTransient<IValidator<CreateProducthasCategoryDto>, CreateProducthasCategoryDtoValidator>();
            services.AddTransient<IValidator<UpdateProducthasCategoryDto>, UpdateProducthasCategoryDtoValidator>();

			services.AddTransient<IValidator<CreateSliderDto>, CreateSliderDtoValidator>();
			services.AddTransient<IValidator<UpdateSliderDto>, UpdateSliderDtoValidator>();

			services.AddTransient<IValidator<CreateAboutDto>, CreateAboutDtoValidator>();
			services.AddTransient<IValidator<UpdateAboutDto>, UpdateAboutDtoValidator>();

            services.AddTransient<IValidator<CreateContactDto>, CreateContactDtoValidator>();
            services.AddTransient<IValidator<UpdateContactDto>, UpdateContactDtoValidator>();

            services.AddTransient<IValidator<CreateImageDto>, CreateImageDtoValidor>();
            services.AddTransient<IValidator<UpdateImageDto>, UpdateImageDtoValidator>();

            services.AddTransient<IValidator<CreateProducthasImageDto>, CreateProducthasImageDtoValidator>();
            services.AddTransient<IValidator<UpdateProducthasImageDto>, UpdateProducthasImageDtoValidator>();


            services.AddTransient<IValidator<CreateColorDto>, CreateColorDtoValidator>();
            services.AddTransient<IValidator<UpdateColorDto>, UpdateColorDtoValidator>();


            services.AddTransient<IValidator<CreateProducthasColorDto>, CreateProducthasColorValidationRules>();
            services.AddTransient<IValidator<UpdateProducthasColorDto>, UpdateProducthasColorValidationRules>();

            services.AddTransient<IValidator<CreateSizeDto>, CreateSizeDtoValidator>();
            services.AddTransient<IValidator<UpdateSizeDto>, UpdateSizeDtoValidator>();


            services.AddTransient<IValidator<CreateProducthasSizeDto>, CreateProducthasSizeDtoValidator>();
            services.AddTransient<IValidator<UpdateProducthasSizeDto>, UpdateProducthasSizeDtoValidator>();

            services.AddTransient<IValidator<UserRegisterDto>, CreateRegisterDtoValidator>();
            services.AddTransient<IValidator<UserLoginDto>, CreateLoginValidationRules>();

            services.AddTransient<IValidator<CreateUserAddressDto>, CreateUserAddressValidator>();
            services.AddTransient<IValidator<UpdateUserAddressDto>, UpdateUserAddressValidator>();

			services.AddTransient<IValidator<CreateBasketDto>, CreateBasketDtoValidator>();
			services.AddTransient<IValidator<UpdateBasketDto>, UpdateBasketDtoValidator>();

            services.AddTransient<IValidator<CreateOrderDto>, CreateOrderDtoValidator>();
            services.AddTransient<IValidator<UpdateOrderDto>, UpdateOrderDtoValidator>();



            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IContactService, ContactService>();
			services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<IProducthasImageService, ProducthasImageService>();
            services.AddScoped<IProducthasCategoryService, ProducthasCategoryService>();
            services.AddScoped<IProducthasColorService, ProducthasColorService>();
            services.AddScoped<IProducthasSizeService, ProducthasSizeService>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IProducthasStockService, ProducthasStockService>();
            services.AddScoped<IUserInformationService, UserInformationService>();
            services.AddScoped<IUserAddressService, UserAddressService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IOrderService, OrderService>();


            


            //EF
            services.AddScoped<IEFProducthasImageService, EFProducthasImageService>();
            services.AddScoped<IEfProducthasColorService, EfProducthasColorService>();
            services.AddScoped<IEFProducthasCategoryService, EFProducthasCategoryService>();
            services.AddScoped<IEFProducthasSizeService, EFProducthasSizeService>();
            services.AddScoped<IEFProducthasStockService, EFProducthasStockService>();
            services.AddScoped<IEFProductService, EFProductService>();
            services.AddScoped<IEFBasketService, EFBasketService>();
            services.AddScoped<IEFUserAddressService, EFUserAddressService>();








        }
    }
}
