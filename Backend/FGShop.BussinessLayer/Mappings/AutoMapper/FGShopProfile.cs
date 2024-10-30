using AutoMapper;
using FGShop.DtoLayer.AboutDtos;
using FGShop.DtoLayer.AuthDtos;
using FGShop.DtoLayer.CategoryDtos;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.ContactDtos;
using FGShop.DtoLayer.ImageDtos;
using FGShop.DtoLayer.ProductDtos;
using FGShop.DtoLayer.ProducthasCategoryDtos;
using FGShop.DtoLayer.ProducthasColorDtos;
using FGShop.DtoLayer.ProducthasImageDto;
using FGShop.DtoLayer.ProducthasStockDtos;
using FGShop.DtoLayer.ProucthasSizeDtos;
using FGShop.DtoLayer.SizeDtos;
using FGShop.DtoLayer.SliderDtos;
using FGShop.DtoLayer.StockDtos;
using FGShop.EntityLayer.Entities;


namespace FGShop.BussinessLayer.Mappings.AutoMapper
{
    public class FGShopProfile: Profile
    {
        public FGShopProfile()
        {
            CreateMap<Stock, ResultStockDto>().ReverseMap();
            CreateMap<Stock, CreateStockDto>().ReverseMap();
            CreateMap<Stock, UpdateStockDto>().ReverseMap();
            CreateMap<Stock, GetByStockIdDto>().ReverseMap();
            CreateMap<UpdateStockDto, ResultStockDto>().ReverseMap();

            CreateMap<ProducthasStock, ResultProducthasStockDto>().ReverseMap();
            CreateMap<ProducthasStock, CreateProducthasStockDto>().ReverseMap();
            CreateMap<ProducthasStock, UpdateProducthasStockDto>().ReverseMap();
            CreateMap<ProducthasStock, GetByProducthasStockIdDto>().ReverseMap();
            CreateMap<UpdateProducthasStockDto, ResultProducthasStockDto>().ReverseMap();

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


            CreateMap<ProducthasSize, ResultProducthasSizeDto>().ReverseMap();
            CreateMap<ProducthasSize, CreateProducthasSizeDto>().ReverseMap();
            CreateMap<ProducthasSize, UpdateProducthasSizeDto>().ReverseMap();
            CreateMap<ProducthasSize, GetByProducthasSizeIdDto>().ReverseMap();
            CreateMap<UpdateProducthasSizeDto, ResultProducthasSizeDto>().ReverseMap();



			CreateMap<ApplicationUser, UserRegisterDto>().ReverseMap();
			CreateMap<ApplicationUser, UserLoginDto>().ReverseMap();
		}
    }
}
