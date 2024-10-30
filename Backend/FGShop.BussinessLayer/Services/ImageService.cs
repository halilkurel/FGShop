using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.ImageDtos;
using FGShop.DtoLayer.SliderDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
    public class ImageService : IImageService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateImageDto> _createValidator;
        private readonly IValidator<UpdateImageDto> _updateValidator;
        private readonly FGShopContext _context;

        public ImageService(IUow uow, IMapper mapper, IValidator<CreateImageDto> createValidator, IValidator<UpdateImageDto> updateValidator, FGShopContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        public async Task<IResponse<CreateImageDto>> Create(CreateImageDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<Image>().Create(_mapper.Map<Image>(dto));
                await _uow.SaveChanges();

                return new Response<CreateImageDto>(ResponseType.Success, dto);
            }

            else
            {

                return new Response<CreateImageDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        public async Task<IResponse<List<ResultImageDto>>> GetAll()
        {
            var data = _mapper.Map<List<ResultImageDto>>(await _uow.GetRepository<Image>().GetAll());

            return new Response<List<ResultImageDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Image>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<Image>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<Image>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

        public async Task<IResponse<UpdateImageDto>> Update(UpdateImageDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Image>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Image>().Update(_mapper.Map<Image>(dto), updatedEntity);
                    await _uow.SaveChanges();

                    return new Response<UpdateImageDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateImageDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
            }
            else
            {
                return new Response<UpdateImageDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }
    }
}
