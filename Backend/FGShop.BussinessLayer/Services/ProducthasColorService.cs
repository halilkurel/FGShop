using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.ProducthasColorDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
    public class ProducthasColorService : IProducthasColorService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProducthasColorDto> _createValidator;
        private readonly IValidator<UpdateProducthasColorDto> _updateValidator;
        private readonly FGShopContext _context;

        public ProducthasColorService(IUow uow, IMapper mapper, IValidator<CreateProducthasColorDto> createValidator, IValidator<UpdateProducthasColorDto> updateValidator, FGShopContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        public async Task<IResponse<CreateProducthasColorDto>> Create(CreateProducthasColorDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<ProducthasColor>().Create(_mapper.Map<ProducthasColor>(dto));
                await _uow.SaveChanges();

                return new Response<CreateProducthasColorDto>(ResponseType.Success, dto);
            }

            else
            {

                return new Response<CreateProducthasColorDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        public async Task<IResponse<List<ResultProducthasColorDto>>> GetAll()
        {
            var data = _mapper.Map<List<ResultProducthasColorDto>>(await _uow.GetRepository<ProducthasColor>().GetAll());

            return new Response<List<ResultProducthasColorDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<ProducthasColor>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<ProducthasColor>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<ProducthasColor>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

        public async Task<IResponse<UpdateProducthasColorDto>> Update(UpdateProducthasColorDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<ProducthasColor>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<ProducthasColor>().Update(_mapper.Map<ProducthasColor>(dto), updatedEntity);
                    await _uow.SaveChanges();

                    return new Response<UpdateProducthasColorDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateProducthasColorDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
            }
            else
            {
                return new Response<UpdateProducthasColorDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }
    }
}
