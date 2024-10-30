using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.ProucthasSizeDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
    public class ProducthasSizeService : IProducthasSizeService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateProducthasSizeDto> _createValidator;
        private readonly IValidator<UpdateProducthasSizeDto> _updateValidator;
        private readonly FGShopContext _context;

        public ProducthasSizeService(IUow uow, IMapper mapper, IValidator<CreateProducthasSizeDto> createValidator, IValidator<UpdateProducthasSizeDto> updateValidator, FGShopContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        public async Task<IResponse<CreateProducthasSizeDto>> Create(CreateProducthasSizeDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<ProducthasSize>().Create(_mapper.Map<ProducthasSize>(dto));
                await _uow.SaveChanges();

                return new Response<CreateProducthasSizeDto>(ResponseType.Success, dto);
            }

            else
            {

                return new Response<CreateProducthasSizeDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        public async Task<IResponse<List<ResultProducthasSizeDto>>> GetAll()
        {
            var data = _mapper.Map<List<ResultProducthasSizeDto>>(await _uow.GetRepository<ProducthasSize>().GetAll());

            return new Response<List<ResultProducthasSizeDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<ProducthasSize>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<ProducthasSize>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<ProducthasSize>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

        public async Task<IResponse<UpdateProducthasSizeDto>> Update(UpdateProducthasSizeDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<ProducthasSize>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<ProducthasSize>().Update(_mapper.Map<ProducthasSize>(dto), updatedEntity);
                    await _uow.SaveChanges();

                    return new Response<UpdateProducthasSizeDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateProducthasSizeDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
            }
            else
            {
                return new Response<UpdateProducthasSizeDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }
    }
}
