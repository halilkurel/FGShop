using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.BasketDtos;
using FGShop.DtoLayer.BasketDtos;
using FGShop.DtoLayer.BasketDtos;
using FGShop.DtoLayer.BasketDtos;
using FGShop.DtoLayer.EFOrderDtos;
using FGShop.DtoLayer.Interfaces;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
	public class BasketService : IBasketService
	{
		private readonly IMapper _map;
		private readonly IUow _uow;
		private readonly IValidator<CreateBasketDto> _createValidator;
		private readonly IValidator<UpdateBasketDto> _updateValidator;

		public BasketService(IMapper map, IUow uow, IValidator<CreateBasketDto> createValidator, IValidator<UpdateBasketDto> updateValidator)
		{
			_map = map;
			_uow = uow;
			_createValidator = createValidator;
			_updateValidator = updateValidator;
		}

		public async Task<IResponse<CreateBasketDto>> Create(CreateBasketDto dto)
		{
			var ValidationResult = _createValidator.Validate(dto);
			if (ValidationResult.IsValid)
			{
				await _uow.GetRepository<Basket>().Create(_map.Map<Basket>(dto));
				await _uow.SaveChanges();

				return new Response<CreateBasketDto>(ResponseType.Success, dto);
			}

			else
			{

				return new Response<CreateBasketDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
			}
		}

		public async Task<IResponse<List<ResultBasketDto>>> GetAll()
		{
			var data = _map.Map<List<ResultBasketDto>>(await _uow.GetRepository<Basket>().GetAll());

			return new Response<List<ResultBasketDto>>(ResponseType.Success, data);
		}

		public async Task<IResponse<IDto>> GetById<IDto>(int id)
		{
			var data = _map.Map<IDto>(await _uow.GetRepository<Basket>().GetByFilter(x => x.Id == id));
			if (data == null)
			{
				return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
			}
			return new Response<IDto>(ResponseType.Success, data);
		}



		public async Task<IResponse> Remove(int id)
		{
			var deletedEntity = await _uow.GetRepository<Basket>().GetByFilter(x => x.Id == id);
			if (deletedEntity != null)
			{
				_uow.GetRepository<Basket>().Remove(deletedEntity);
				await _uow.SaveChanges();
				return new Response(ResponseType.Success);
			}
			else
			{
				return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
			}
		}

		public async Task<IResponse<UpdateBasketDto>> Update(UpdateBasketDto dto)
		{
			var result = _updateValidator.Validate(dto);
			if (result.IsValid)
			{
				var updatedEntity = await _uow.GetRepository<Basket>().Find(dto.Id);
				if (updatedEntity != null)
				{
					_uow.GetRepository<Basket>().Update(_map.Map<Basket>(dto), updatedEntity);
					await _uow.SaveChanges();

					return new Response<UpdateBasketDto>(ResponseType.Success, dto);
				}
				return new Response<UpdateBasketDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
			}
			else
			{
				return new Response<UpdateBasketDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
			}
		}
	}
}
