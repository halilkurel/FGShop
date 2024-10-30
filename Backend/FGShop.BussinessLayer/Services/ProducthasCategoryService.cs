using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.ProducthasCategoryDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;


namespace FGShop.BussinessLayer.Services
{
	public class ProducthasCategoryService : IProducthasCategoryService
    {

        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProducthasCategoryDto> _createValidator;
        private readonly IValidator<UpdateProducthasCategoryDto> _updateValidator;
		private readonly FGShopContext _context;

		public ProducthasCategoryService(IUow uow, IMapper mapper, IValidator<CreateProducthasCategoryDto> createValidator, IValidator<UpdateProducthasCategoryDto> updateValidator, FGShopContext context)
		{
			_uow = uow;
			_mapper = mapper;
			_createValidator = createValidator;
			_updateValidator = updateValidator;
			_context = context;
		}

		public async Task<IResponse<CreateProducthasCategoryDto>> Create(CreateProducthasCategoryDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<ProducthasCategory>().Create(_mapper.Map<ProducthasCategory>(dto));
                await _uow.SaveChanges();

                return new Response<CreateProducthasCategoryDto>(ResponseType.Success, dto);
            }

            else
            {

                return new Response<CreateProducthasCategoryDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        public async Task<IResponse<List<ResultProducthasCategoryDto>>> GetAll()
        {
            var data = _mapper.Map<List<ResultProducthasCategoryDto>>(await _uow.GetRepository<ProducthasCategory>().GetAll());

            return new Response<List<ResultProducthasCategoryDto>>(ResponseType.Success, data);
        }


		public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<ProducthasCategory>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<ProducthasCategory>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<ProducthasCategory>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

		public async Task<List<ResultAllProducthasCategoryDto>> ResultAllProducthasCategory()
		{
			var result = await _context.Products
		.Select(p => new ResultAllProducthasCategoryDto
		{
            ProductId= p.Id,
			ProductName = p.ProductName,
			CoverPhoto = p.CoverPhoto,
			Price = p.Price,
			CategoryName = p.ProducthasCategories
				.Select(pc => pc.Category.CategoryName)
				.ToList()
		})
		.ToListAsync();

			return result;


		}

		public async Task<IResponse<UpdateProducthasCategoryDto>> Update(UpdateProducthasCategoryDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<ProducthasCategory>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<ProducthasCategory>().Update(_mapper.Map<ProducthasCategory>(dto), updatedEntity);
                    await _uow.SaveChanges();

                    return new Response<UpdateProducthasCategoryDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateProducthasCategoryDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
            }
            else
            {
                return new Response<UpdateProducthasCategoryDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }



    }
}
