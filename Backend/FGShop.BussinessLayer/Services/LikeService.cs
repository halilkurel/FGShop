using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.AboutDtos;
using FGShop.DtoLayer.LikeDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
    public class LikeService : ILikeService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateLikeDto> _createValidator;
        private readonly IValidator<UpdateLikeDto> _updateValidator;

        public LikeService(IUow uow, IMapper mapper, IValidator<CreateLikeDto> createValidator, IValidator<UpdateLikeDto> updateValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<IResponse<CreateLikeDto>> Create(CreateLikeDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<Like>().Create(_mapper.Map<Like>(dto));
                await _uow.SaveChanges();

                return new Response<CreateLikeDto>(ResponseType.Success, dto);
            }

            else
            {

                return new Response<CreateLikeDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        public async Task<List<ResultLikeDto>> GetAll()
        {
            var data = _mapper.Map<List<ResultLikeDto>>(await _uow.GetRepository<Like>().GetAll());

            return data;
        }

        public async Task<IDto> GetById<IDto>(int id)
        {
            // Veriyi filtreye göre getir
            var entity = await _uow.GetRepository<Like>().GetByFilter(x => x.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"{id} ait data bulunamadı.");
            }

            // Gelen entity'yi IDto'ya map et ve döndür
            return _mapper.Map<IDto>(entity);
        }



        public async Task<IResponse> Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<Like>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<Like>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

        public async Task<IResponse<UpdateLikeDto>> Update(UpdateLikeDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Like>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Like>().Update(_mapper.Map<Like>(dto), updatedEntity);
                    await _uow.SaveChanges();

                    return new Response<UpdateLikeDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateLikeDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
            }
            else
            {
                return new Response<UpdateLikeDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }
    }
}
