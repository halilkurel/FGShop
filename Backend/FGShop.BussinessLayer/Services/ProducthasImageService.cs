using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.ProducthasCategoryDtos;
using FGShop.DtoLayer.ProducthasImageDto;
using FGShop.EntityLayer.Entities;
using FluentValidation;


namespace FGShop.BussinessLayer.Services
{
    public class ProducthasImageService: IProducthasImageService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProducthasImageDto> _createValidator;
        private readonly IValidator<UpdateProducthasImageDto> _updateValidator;
        private readonly FGShopContext _context;

        public ProducthasImageService(IUow uow, IMapper mapper, IValidator<CreateProducthasImageDto> createValidator, IValidator<UpdateProducthasImageDto> updateValidator, FGShopContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        public async Task<IResponse<CreateProducthasImageDto>> Create(CreateProducthasImageDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<ProducthasImage>().Create(_mapper.Map<ProducthasImage>(dto));
                await _uow.SaveChanges();

                return new Response<CreateProducthasImageDto>(ResponseType.Success, dto);
            }

            else
            {

                return new Response<CreateProducthasImageDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        public async Task<IResponse<List<ResultProducthasImageDto>>> GetAll()
        {
            var data = _mapper.Map<List<ResultProducthasImageDto>>(await _uow.GetRepository<ProducthasImage>().GetAll());

            return new Response<List<ResultProducthasImageDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<ProducthasImage>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<ProducthasImage>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<ProducthasImage>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

        public async Task<IResponse<UpdateProducthasImageDto>> Update(UpdateProducthasImageDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<ProducthasImage>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<ProducthasImage>().Update(_mapper.Map<ProducthasImage>(dto), updatedEntity);
                    await _uow.SaveChanges();

                    return new Response<UpdateProducthasImageDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateProducthasImageDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
            }
            else
            {
                return new Response<UpdateProducthasImageDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }
    }
}
