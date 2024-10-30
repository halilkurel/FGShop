using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.AboutDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;


namespace FGShop.BussinessLayer.Services
{
	public class AboutService: IAboutService
	{
		private readonly IUow _uow;
		private readonly IMapper _mapper;
		private readonly IValidator<CreateAboutDto> _createValidator;
		private readonly IValidator<UpdateAboutDto> _updateValidator;
		private readonly FGShopContext _context;

		public AboutService(IUow uow, IMapper mapper, IValidator<CreateAboutDto> createValidator, IValidator<UpdateAboutDto> updateValidator, FGShopContext context)
		{
			_uow = uow;
			_mapper = mapper;
			_createValidator = createValidator;
			_updateValidator = updateValidator;
			_context = context;
		}

		public async Task<IResponse<CreateAboutDto>> Create(CreateAboutDto dto)
		{
			var ValidationResult = _createValidator.Validate(dto);
			if (ValidationResult.IsValid)
			{
				await _uow.GetRepository<About>().Create(_mapper.Map<About>(dto));
				await _uow.SaveChanges();

				return new Response<CreateAboutDto>(ResponseType.Success, dto);
			}

			else
			{

				return new Response<CreateAboutDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
			}
		}

		public async Task<IResponse<List<ResultAboutDto>>> GetAll()
		{
			var data = _mapper.Map<List<ResultAboutDto>>(await _uow.GetRepository<About>().GetAll());

			return new Response<List<ResultAboutDto>>(ResponseType.Success, data);
		}

		public async Task<IResponse<IDto>> GetById<IDto>(int id)
		{
			var data = _mapper.Map<IDto>(await _uow.GetRepository<About>().GetByFilter(x => x.Id == id));
			if (data == null)
			{
				return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
			}
			return new Response<IDto>(ResponseType.Success, data);
		}

		public async Task<IResponse> Remove(int id)
		{
			var deletedEntity = await _uow.GetRepository<About>().GetByFilter(x => x.Id == id);
			if (deletedEntity != null)
			{
				_uow.GetRepository<About>().Remove(deletedEntity);
				await _uow.SaveChanges();
				return new Response(ResponseType.Success);
			}
			else
			{
				return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
			}
		}

		public async Task<IResponse<UpdateAboutDto>> Update(UpdateAboutDto dto)
		{
			var result = _updateValidator.Validate(dto);
			if (result.IsValid)
			{
				var updatedEntity = await _uow.GetRepository<About>().Find(dto.Id);
				if (updatedEntity != null)
				{
					_uow.GetRepository<About>().Update(_mapper.Map<About>(dto), updatedEntity);
					await _uow.SaveChanges();

					return new Response<UpdateAboutDto>(ResponseType.Success, dto);
				}
				return new Response<UpdateAboutDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
			}
			else
			{
				return new Response<UpdateAboutDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
			}
		}
	}
}
