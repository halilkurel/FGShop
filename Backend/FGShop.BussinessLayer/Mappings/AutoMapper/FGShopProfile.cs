using AutoMapper;
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
using FGShop.DtoLayer.ProducthasColorAndProducthasSizeDtos;
using FGShop.DtoLayer.ProducthasColorAndSizehasStockDtos;
using FGShop.DtoLayer.ProducthasColorDtos;
using FGShop.DtoLayer.ProducthasImageDto;
using FGShop.DtoLayer.SizeDtos;
using FGShop.DtoLayer.SliderDtos;
using FGShop.DtoLayer.StatusDtos;
using FGShop.DtoLayer.UserAddressDtos;
using FGShop.EntityLayer.Entities;


namespace FGShop.BussinessLayer.Mappings.AutoMapper
{
    public class FGShopProfile: Profile
    {
        public FGShopProfile()
        {


            CreateMap<Product,ResultProductDto>().ReverseMap();
            CreateMap<Product,CreateProductDto>().ReverseMap();
            CreateMap<Product,UpdateProductDto>().ReverseMap();
            CreateMap<Product,GetByProductIdDto>().ReverseMap();
            CreateMap<UpdateProductDto, ResultProductDto>().ReverseMap();

            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, GetByCategoryIdDto>().ReverseMap();
            CreateMap<UpdateCategoryDto, ResultCategoryDto>().ReverseMap();

            CreateMap<ProducthasCategory, ResultProducthasCategoryDto>().ReverseMap();
            CreateMap<ProducthasCategory, CreateProducthasCategoryDto>().ReverseMap();
            CreateMap<ProducthasCategory, UpdateProducthasCategoryDto>().ReverseMap();
            CreateMap<ProducthasCategory, GetByProducthasCategoryIdDto>().ReverseMap();
            CreateMap<UpdateProducthasCategoryDto, ResultProducthasCategoryDto>().ReverseMap();


			CreateMap<Slider, ResultSliderDto>().ReverseMap();
			CreateMap<Slider, CreateSliderDto>().ReverseMap();
			CreateMap<Slider, UpdateSliderDto>().ReverseMap();
			CreateMap<Slider, GetBySliderIdDto>().ReverseMap();
			CreateMap<UpdateSliderDto, ResultSliderDto>().ReverseMap();

			CreateMap<About, ResultAboutDto>().ReverseMap();
			CreateMap<About, CreateAboutDto>().ReverseMap();
			CreateMap<About, UpdateAboutDto>().ReverseMap();
			CreateMap<About, GetByAboutIdDto>().ReverseMap();
			CreateMap<UpdateAboutDto, ResultAboutDto>().ReverseMap();


            CreateMap<Contact, ResultContactDto>().ReverseMap();
            CreateMap<Contact, CreateContactDto>().ReverseMap();
            CreateMap<Contact, UpdateContactDto>().ReverseMap();
            CreateMap<Contact, GetByContactIdDto>().ReverseMap();
            CreateMap<UpdateContactDto, ResultContactDto>().ReverseMap();

            CreateMap<ProducthasImage, ResultProducthasImageDto>().ReverseMap();
            CreateMap<ProducthasImage, CreateProducthasImageDto>().ReverseMap();
            CreateMap<ProducthasImage, UpdateProducthasImageDto>().ReverseMap();
            CreateMap<ProducthasImage, GetByProducthasImageIdDto>().ReverseMap();
            CreateMap<UpdateProducthasImageDto, ResultProducthasImageDto>().ReverseMap();

            CreateMap<Color, ResultColorDto>().ReverseMap();
            CreateMap<Color, CreateColorDto>().ReverseMap();
            CreateMap<Color, UpdateColorDto>().ReverseMap();
            CreateMap<Color, GetByColorIdDto>().ReverseMap();
            CreateMap<UpdateColorDto, ResultColorDto>().ReverseMap();

            CreateMap<Image, ResultImageDto>().ReverseMap();
            CreateMap<Image, CreateImageDto>().ReverseMap();
            CreateMap<Image, UpdateImageDto>().ReverseMap();
            CreateMap<Image, GetByImageIdDto>().ReverseMap();
            CreateMap<UpdateImageDto, ResultImageDto>().ReverseMap();


            CreateMap<ProducthasColor, ResultProducthasColorDto>().ReverseMap();
            CreateMap<ProducthasColor, CreateProducthasColorDto>().ReverseMap();
            CreateMap<ProducthasColor, UpdateProducthasColorDto>().ReverseMap();
            CreateMap<ProducthasColor, GetByProducthasColorIdDto>().ReverseMap();
            CreateMap<UpdateProducthasColorDto, ResultProducthasColorDto>().ReverseMap();


            CreateMap<Size, ResultSizeDto>().ReverseMap();
            CreateMap<Size, CreateSizeDto>().ReverseMap();
            CreateMap<Size, UpdateSizeDto>().ReverseMap();
            CreateMap<Size, GetBySizeIdDto>().ReverseMap();
            CreateMap<UpdateSizeDto, ResultSizeDto>().ReverseMap();



			CreateMap<ApplicationUser, UserRegisterDto>().ReverseMap();
			CreateMap<ApplicationUser, UserLoginDto>().ReverseMap();

            CreateMap<UserAddress, ResultUserAddressDto>().ReverseMap();
            CreateMap<UserAddress, CreateUserAddressDto>().ReverseMap();
            CreateMap<UserAddress, UpdateUserAddressDto>().ReverseMap();
            CreateMap<ResultUserAddressDto, UpdateUserAddressDto>().ReverseMap();
            CreateMap<CreateUserAddressDto, UpdateUserAddressDto>().ReverseMap();


            CreateMap<Status, ResultStatusDto>().ReverseMap();
            CreateMap<Status, CreateStatusDto>().ReverseMap();
            CreateMap<Status, UpdateStatusDto>().ReverseMap();
            CreateMap<ResultStatusDto, UpdateStatusDto>().ReverseMap();

			CreateMap<Basket, ResultBasketDto>().ReverseMap();
			CreateMap<Basket, CreateBasketDto>().ReverseMap();
			CreateMap<Basket, UpdateBasketDto>().ReverseMap();
			CreateMap<Basket, GetByBasketIdDto>().ReverseMap();
			CreateMap<Basket, List<ResultBasketDto>>().ReverseMap();


            CreateMap<Order, ResultOrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
            CreateMap<ResultOrderDto, UpdateOrderDto>().ReverseMap();


			CreateMap<ProducthasColorAndSize, CreateProducthasColorAndSizeDto>().ReverseMap();
			CreateMap<ProducthasColorAndSize, UpdateProducthasColorAndSizeDto>().ReverseMap();
			CreateMap<ProducthasColorAndSize, ResultProducthasColorAndSizeDto>().ReverseMap();
			CreateMap<ProducthasColorAndSize, GetByProducthasColorAndSizeIdDto>().ReverseMap();
			CreateMap<ResultProducthasColorAndSizeDto, UpdateProducthasColorAndSizeDto>().ReverseMap();

            CreateMap<ProducthasColorAndSizehasStock, CreateProducthasColorAndSizehasStockDto>().ReverseMap();
            CreateMap<ProducthasColorAndSizehasStock, UpdateProducthasColorAndSizehasStockDto>().ReverseMap();
            CreateMap<ProducthasColorAndSizehasStock, ResultProducthasColorAndSizehasStockDto>().ReverseMap();
            CreateMap<ProducthasColorAndSizehasStock, GetByProducthasColorAndSizehasStockIdDto>().ReverseMap();
		}
    }
}
