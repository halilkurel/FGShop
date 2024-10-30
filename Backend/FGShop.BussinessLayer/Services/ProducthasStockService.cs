using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.ProducthasStockDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
    public class ProducthasStockService: IProducthasStockService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProducthasStockDto> _createValidator;
        private readonly IValidator<UpdateProducthasStockDto> _updateValidator;
        private readonly FGShopContext _context;

        public ProducthasStockService(IUow uow, IMapper mapper, IValidator<CreateProducthasStockDto> createValidator, IValidator<UpdateProducthasStockDto> updateValidator, FGShopContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        public async Task<IResponse<CreateProducthasStockDto>> Create(CreateProducthasStockDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<ProducthasStock>().Create(_mapper.Map<ProducthasStock>(dto));
                await _uow.SaveChanges();

                return new Response<CreateProducthasStockDto>(ResponseType.Success, dto);
            }

            else
            {

                return new Response<CreateProducthasStockDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        public async Task<IResponse<List<ResultProducthasStockDto>>> GetAll()
        {
            var data = _mapper.Map<List<ResultProducthasStockDto>>(await _uow.GetRepository<ProducthasStock>().GetAll());

            return new Response<List<ResultProducthasStockDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<ProducthasStock>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<ProducthasStock>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<ProducthasStock>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

        public async Task<IResponse<UpdateProducthasStockDto>> Update(UpdateProducthasStockDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<ProducthasStock>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<ProducthasStock>().Update(_mapper.Map<ProducthasStock>(dto), updatedEntity);
                    await _uow.SaveChanges();

                    return new Response<UpdateProducthasStockDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateProducthasStockDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
            }
            else
            {
                return new Response<UpdateProducthasStockDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }
    }
}
