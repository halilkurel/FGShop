using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.StockDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
    public class StockService: IStockService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateStockDto> _createValidator;
        private readonly IValidator<UpdateStockDto> _updateValidator;
        private readonly FGShopContext _context;

        public StockService(IUow uow, IMapper mapper, IValidator<CreateStockDto> createValidator, IValidator<UpdateStockDto> updateValidator, FGShopContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        public async Task<IResponse<CreateStockDto>> Create(CreateStockDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<Stock>().Create(_mapper.Map<Stock>(dto));
                await _uow.SaveChanges();

                return new Response<CreateStockDto>(ResponseType.Success, dto);
            }

            else
            {

                return new Response<CreateStockDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        public async Task<IResponse<List<ResultStockDto>>> GetAll()
        {
            var data = _mapper.Map<List<ResultStockDto>>(await _uow.GetRepository<Stock>().GetAll());

            return new Response<List<ResultStockDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Stock>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<Stock>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<Stock>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

        public async Task<IResponse<UpdateStockDto>> Update(UpdateStockDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Stock>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Stock>().Update(_mapper.Map<Stock>(dto), updatedEntity);
                    await _uow.SaveChanges();

                    return new Response<UpdateStockDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateStockDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
            }
            else
            {
                return new Response<UpdateStockDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }
    }
}
