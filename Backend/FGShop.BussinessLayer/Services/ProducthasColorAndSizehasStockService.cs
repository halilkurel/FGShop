using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.ProducthasColorAndProducthasSizeDtos;
using FGShop.DtoLayer.ProducthasColorAndSizehasStockDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
    public class ProducthasColorAndSizehasStockService: IProducthasColorAndSizehasStockService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProducthasColorAndSizehasStockDto> _createValidator;
        private readonly IValidator<UpdateProducthasColorAndSizehasStockDto> _updateValidator;
        private readonly FGShopContext _context;

        public ProducthasColorAndSizehasStockService(IUow uow, IMapper mapper, IValidator<CreateProducthasColorAndSizehasStockDto> createValidator, IValidator<UpdateProducthasColorAndSizehasStockDto> updateValidator, FGShopContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        public async Task<IResponse<CreateProducthasColorAndSizehasStockDto>> Create(CreateProducthasColorAndSizehasStockDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<ProducthasColorAndSizehasStock>().Create(_mapper.Map<ProducthasColorAndSizehasStock>(dto));
                await _uow.SaveChanges();

                return new Response<CreateProducthasColorAndSizehasStockDto>(ResponseType.Success, dto);
            }

            else
            {

                return new Response<CreateProducthasColorAndSizehasStockDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        public async Task<IResponse<List<ResultProducthasColorAndSizehasStockDto>>> GetAll()
        {
            var data = _mapper.Map<List<ResultProducthasColorAndSizehasStockDto>>(await _uow.GetRepository<ProducthasColorAndSizehasStock>().GetAll());

            return new Response<List<ResultProducthasColorAndSizehasStockDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<ProducthasColorAndSizehasStock>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<ProducthasColorAndSizehasStock>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<ProducthasColorAndSizehasStock>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

        public async Task<IResponse<UpdateProducthasColorAndSizehasStockDto>> Update(UpdateProducthasColorAndSizehasStockDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<ProducthasColorAndSizehasStock>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<ProducthasColorAndSizehasStock>().Update(_mapper.Map<ProducthasColorAndSizehasStock>(dto), updatedEntity);
                    await _uow.SaveChanges();

                    return new Response<UpdateProducthasColorAndSizehasStockDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateProducthasColorAndSizehasStockDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
            }
            else
            {
                return new Response<UpdateProducthasColorAndSizehasStockDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }
    }
}
