using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.ProducthasCategoryDtos;
using FGShop.DtoLayer.ProducthasColorAndProducthasSizeDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
	public class ProducthasColorAndSizeDtoService: IProducthasColorAndSizeService
	{
		private readonly IUow _uow;
		private readonly IMapper _mapper;
		private readonly IValidator<CreateProducthasColorAndSizeDto> _createValidator;
		private readonly IValidator<UpdateProducthasColorAndSizeDto> _updateValidator;
		private readonly FGShopContext _context;

		public ProducthasColorAndSizeDtoService(IUow uow, IMapper mapper, IValidator<CreateProducthasColorAndSizeDto> createValidator, IValidator<UpdateProducthasColorAndSizeDto> updateValidator, FGShopContext context)
		{
			_uow = uow;
			_mapper = mapper;
			_createValidator = createValidator;
			_updateValidator = updateValidator;
			_context = context;
		}

		public async Task<IResponse<CreateProducthasColorAndSizeDto>> Create(CreateProducthasColorAndSizeDto dto)
		{
			var ValidationResult = _createValidator.Validate(dto);
			if (ValidationResult.IsValid)
			{
				await _uow.GetRepository<ProducthasColorAndSize>().Create(_mapper.Map<ProducthasColorAndSize>(dto));
				await _uow.SaveChanges();

				return new Response<CreateProducthasColorAndSizeDto>(ResponseType.Success, dto);
			}

			else
			{

				return new Response<CreateProducthasColorAndSizeDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
			}
		}

		public async Task<IResponse<List<ResultProducthasColorAndSizeDto>>> GetAll()
		{
			var data = _mapper.Map<List<ResultProducthasColorAndSizeDto>>(await _uow.GetRepository<ProducthasColorAndSize>().GetAll());

			return new Response<List<ResultProducthasColorAndSizeDto>>(ResponseType.Success, data);
		}

		public async Task<IResponse<IDto>> GetById<IDto>(int id)
		{
			var data = _mapper.Map<IDto>(await _uow.GetRepository<ProducthasColorAndSize>().GetByFilter(x => x.Id == id));
			if (data == null)
			{
				return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
			}
			return new Response<IDto>(ResponseType.Success, data);
		}

		public async Task<IResponse> Remove(int id)
		{
			var deletedEntity = await _uow.GetRepository<ProducthasColorAndSize>().GetByFilter(x => x.Id == id);
			if (deletedEntity != null)
			{
				_uow.GetRepository<ProducthasColorAndSize>().Remove(deletedEntity);
				await _uow.SaveChanges();
				return new Response(ResponseType.Success);
			}
			else
			{
				return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
			}
		}

		public async Task<IResponse<UpdateProducthasColorAndSizeDto>> Update(UpdateProducthasColorAndSizeDto dto)
		{
			var result = _updateValidator.Validate(dto);
			if (result.IsValid)
			{
				var updatedEntity = await _uow.GetRepository<ProducthasColorAndSize>().Find(dto.Id);
				if (updatedEntity != null)
				{
					_uow.GetRepository<ProducthasColorAndSize>().Update(_mapper.Map<ProducthasColorAndSize>(dto), updatedEntity);
					await _uow.SaveChanges();

					return new Response<UpdateProducthasColorAndSizeDto>(ResponseType.Success, dto);
				}
				return new Response<UpdateProducthasColorAndSizeDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
			}
			else
			{
				return new Response<UpdateProducthasColorAndSizeDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
			}
		}
	}
}
