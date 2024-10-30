using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.BussinessLayer.ValidationRules.SliderValidationRules;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.ProducthasCategoryDtos;
using FGShop.DtoLayer.SliderDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
	public class SliderService : ISliderService
	{
		private readonly IUow _uow;
		private readonly IMapper _mapper;
		private readonly IValidator<CreateSliderDto> _createValidator;
		private readonly IValidator<UpdateSliderDto> _updateValidator;
		private readonly FGShopContext _context;

		public SliderService(IUow uow, IMapper mapper, IValidator<CreateSliderDto> createValidator, IValidator<UpdateSliderDto> updateValidator, FGShopContext context)
		{
			_uow = uow;
			_mapper = mapper;
			_createValidator = createValidator;
			_updateValidator = updateValidator;
			_context = context;
		}

		public async Task<IResponse<CreateSliderDto>> Create(CreateSliderDto dto)
		{
			var ValidationResult = _createValidator.Validate(dto);
			if (ValidationResult.IsValid)
			{
				await _uow.GetRepository<Slider>().Create(_mapper.Map<Slider>(dto));
				await _uow.SaveChanges();

				return new Response<CreateSliderDto>(ResponseType.Success, dto);
			}

			else
			{

				return new Response<CreateSliderDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
			}
		}

		public async Task<IResponse<List<ResultSliderDto>>> GetAll()
		{
			var data = _mapper.Map<List<ResultSliderDto>>(await _uow.GetRepository<Slider>().GetAll());

			return new Response<List<ResultSliderDto>>(ResponseType.Success, data);
		}

		public async Task<IResponse<IDto>> GetById<IDto>(int id)
		{
			var data = _mapper.Map<IDto>(await _uow.GetRepository<Slider>().GetByFilter(x => x.Id == id));
			if (data == null)
			{
				return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
			}
			return new Response<IDto>(ResponseType.Success, data);
		}

		public async Task<IResponse> Remove(int id)
		{
			var deletedEntity = await _uow.GetRepository<Slider>().GetByFilter(x => x.Id == id);
			if (deletedEntity != null)
			{
				_uow.GetRepository<Slider>().Remove(deletedEntity);
				await _uow.SaveChanges();
				return new Response(ResponseType.Success);
			}
			else
			{
				return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
			}
		}

		public async Task<IResponse<UpdateSliderDto>> Update(UpdateSliderDto dto)
		{
			var result = _updateValidator.Validate(dto);
			if (result.IsValid)
			{
				var updatedEntity = await _uow.GetRepository<Slider>().Find(dto.Id);
				if (updatedEntity != null)
				{
					_uow.GetRepository<Slider>().Update(_mapper.Map<Slider>(dto), updatedEntity);
					await _uow.SaveChanges();

					return new Response<UpdateSliderDto>(ResponseType.Success, dto);
				}
				return new Response<UpdateSliderDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
			}
			else
			{
				return new Response<UpdateSliderDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
			}
		}
	}
}
