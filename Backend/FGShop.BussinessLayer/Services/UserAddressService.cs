using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.SizeDtos;
using FGShop.DtoLayer.UserAddressDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
    public class UserAddressService: IUserAddressService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserAddressDto> _createValidator;
        private readonly IValidator<UpdateUserAddressDto> _updateValidator;
        private readonly FGShopContext _context;

        public UserAddressService(IUow uow, IMapper mapper, IValidator<CreateUserAddressDto> createValidator, IValidator<UpdateUserAddressDto> updateValidator, FGShopContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        public async Task<IResponse<CreateUserAddressDto>> Create(CreateUserAddressDto dto)
        {


            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<UserAddress>().Create(_mapper.Map<UserAddress>(dto));
                await _uow.SaveChanges();

                return new Response<CreateUserAddressDto>(ResponseType.Success, dto);
            }

            else
            {

                return new Response<CreateUserAddressDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        public async Task<IResponse<List<ResultUserAddressDto>>> GetAll()
        {
            var data = _mapper.Map<List<ResultUserAddressDto>>(await _uow.GetRepository<UserAddress>().GetAll());

            return new Response<List<ResultUserAddressDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<UserAddress>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }


        public async Task<IResponse> Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<UserAddress>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<UserAddress>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

        public async Task<IResponse<UpdateUserAddressDto>> Update(UpdateUserAddressDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<UserAddress>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<UserAddress>().Update(_mapper.Map<UserAddress>(dto), updatedEntity);
                    await _uow.SaveChanges();

                    return new Response<UpdateUserAddressDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateUserAddressDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
            }
            else
            {
                return new Response<UpdateUserAddressDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }


    }
}
