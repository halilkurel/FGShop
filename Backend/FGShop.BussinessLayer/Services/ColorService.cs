using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.ColorDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;


namespace FGShop.BussinessLayer.Services
{
    public class ColorService : IColorService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateColorDto> _createValidator;
        private readonly IValidator<UpdateColorDto> _updateValidator;
        private readonly FGShopContext _context;

        public ColorService(IUow uow, IMapper mapper, IValidator<CreateColorDto> createValidator, IValidator<UpdateColorDto> updateValidator, FGShopContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        public async Task<IResponse<CreateColorDto>> Create(CreateColorDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<Color>().Create(_mapper.Map<Color>(dto));
                await _uow.SaveChanges();

                return new Response<CreateColorDto>(ResponseType.Success, dto);
            }

            else
            {

                return new Response<CreateColorDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        public async Task<IResponse<List<ResultColorDto>>> GetAll()
        {
            var data = _mapper.Map<List<ResultColorDto>>(await _uow.GetRepository<Color>().GetAll());

            return new Response<List<ResultColorDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Color>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<Color>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<Color>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

        public async Task<IResponse<UpdateColorDto>> Update(UpdateColorDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Color>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Color>().Update(_mapper.Map<Color>(dto), updatedEntity);
                    await _uow.SaveChanges();

                    return new Response<UpdateColorDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateColorDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
            }
            else
            {
                return new Response<UpdateColorDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }
    }
}
