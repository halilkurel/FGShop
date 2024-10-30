using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.ProductDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;


namespace FGShop.BussinessLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProductDto> _createValidator;
        private readonly IValidator<UpdateProductDto> _updateValidator;


        public ProductService(IUow uow, IMapper mapper, IValidator<CreateProductDto> createValidator, IValidator<UpdateProductDto> updateValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

		public async Task<List<ResultProductDto>> GetLastSixProduct()
		{
			var data = _mapper.Map<List<ResultProductDto>>(await _uow.GetRepository<Product>().GetAll()); //Tüm verileri getiriyoruz
            var results = data.OrderByDescending(p => p.Id)    // "Id" alanını en yüksekten en düşüğe sıralıyoruz
								.Take(8) // Son 8 veriyi alıyoruz
                                .ToList();
            return results;
		}

		async Task<IResponse<CreateProductDto>> IProductService.Create(CreateProductDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<Product>().Create(_mapper.Map<Product>(dto));
                await _uow.SaveChanges();

                return new Response<CreateProductDto>(ResponseType.Success, dto);
            }



            else
            {

                return new Response<CreateProductDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        async Task<IResponse<List<ResultProductDto>>> IProductService.GetAll()
        {
            var data = _mapper.Map<List<ResultProductDto>>(await _uow.GetRepository<Product>().GetAll());

            return new Response<List<ResultProductDto>>(ResponseType.Success, data);
        }

        async Task<IResponse<IDto>> IProductService.GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Product>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        async Task<IResponse> IProductService.Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<Product>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<Product>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

        async Task<IResponse<UpdateProductDto>> IProductService.Update(UpdateProductDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Product>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Product>().Update(_mapper.Map<Product>(dto), updatedEntity);
                    _uow.SaveChanges();

                    return new Response<UpdateProductDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateProductDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");

            }
            else
            {
                return new Response<UpdateProductDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }
    }
}
