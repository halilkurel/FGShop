using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.SizeDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
    public class SizeService : ISizeService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateSizeDto> _createValidator;
        private readonly IValidator<UpdateSizeDto> _updateValidator;
        private readonly FGShopContext _context;

        public SizeService(IUow uow, IMapper mapper, IValidator<CreateSizeDto> createValidator, IValidator<UpdateSizeDto> updateValidator, FGShopContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        public async Task<IResponse<CreateSizeDto>> Create(CreateSizeDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<Size>().Create(_mapper.Map<Size>(dto));
                await _uow.SaveChanges();

                return new Response<CreateSizeDto>(ResponseType.Success, dto);
            }

            else
            {

                return new Response<CreateSizeDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        public async Task<IResponse<List<ResultSizeDto>>> GetAll()
        {
            var data = _mapper.Map<List<ResultSizeDto>>(await _uow.GetRepository<Size>().GetAll());

            return new Response<List<ResultSizeDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Size>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<Size>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<Size>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

        public async Task<IResponse<UpdateSizeDto>> Update(UpdateSizeDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Size>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Size>().Update(_mapper.Map<Size>(dto), updatedEntity);
                    await _uow.SaveChanges();

                    return new Response<UpdateSizeDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateSizeDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
            }
            else
            {
                return new Response<UpdateSizeDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }
    }
}
