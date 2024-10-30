using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.CategoryDtos;
using FGShop.DtoLayer.ProductDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createValidator;
        private readonly IValidator<UpdateCategoryDto> _updateValidator;


        public CategoryService(IUow uow, IMapper mapper, IValidator<CreateCategoryDto> createValidator, IValidator<UpdateCategoryDto> updateValidator)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task Create(CreateCategoryDto dto)
        {
            var result = _createValidator.Validate(dto);
            if (result.IsValid)
            {
                await _uow.GetRepository<Category>().Create(_mapper.Map<Category>(dto));
                await _uow.SaveChanges();
            }
        }

        public async Task<List<ResultCategoryDto>> GetAll()
        {
            return _mapper.Map<List<ResultCategoryDto>>(await _uow.GetRepository<Category>().GetAll());
        }

        public async Task<IDto> GetById<IDto>(int id)
        {
            var value = await _uow.GetRepository<Category>().Find(id);
            return _mapper.Map<IDto>(value);
        }

        public async Task<List<ResultCategoryDto>> GetFirst3Category()
        {
            var data = _mapper.Map<List<ResultCategoryDto>>(await _uow.GetRepository<Category>().GetAll()); //Tüm verileri getiriyoruz
            var results = data.OrderBy(p => p.Id)    // "Id" alanını en düşükten en yükseğe sıralıyoruz.
                                .Take(3) // İlk 3 veriyi alıyoruz
                                .ToList();
            return results;
        }

        public async Task Remove(int id)
        {
            var removed = await _uow.GetRepository<Category>().GetByFilter(x => x.Id == id);
            if (removed != null)
            {
                _uow.GetRepository<Category>().Remove(removed);
                await _uow.SaveChanges();
            }

        }

        public async Task Update(UpdateCategoryDto dto)
        {
            var updatedEntity = await _uow.GetRepository<Category>().Find(dto.Id);
            if (updatedEntity != null)
            {
                _uow.GetRepository<Category>().Update(_mapper.Map<Category>(dto), updatedEntity);
                await _uow.SaveChanges();
            }


        }

        async Task<IResponse<CreateCategoryDto>> ICategoryService.Create(CreateCategoryDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<Category>().Create(_mapper.Map<Category>(dto));
                await _uow.SaveChanges();

                return new Response<CreateCategoryDto>(ResponseType.Success, dto);
            }



            else
            {

                return new Response<CreateCategoryDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        async Task<IResponse<List<ResultCategoryDto>>> ICategoryService.GetAll()
        {
            var data = _mapper.Map<List<ResultCategoryDto>>(await _uow.GetRepository<Category>().GetAll());

            return new Response<List<ResultCategoryDto>>(ResponseType.Success, data);
        }

        async Task<IResponse<IDto>> ICategoryService.GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Category>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        async Task<IResponse> ICategoryService.Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<Category>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<Category>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

        async Task<IResponse<UpdateCategoryDto>> ICategoryService.Update(UpdateCategoryDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Category>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Category>().Update(_mapper.Map<Category>(dto), updatedEntity);
                    _uow.SaveChanges();

                    return new Response<UpdateCategoryDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateCategoryDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");

            }
            else
            {
                return new Response<UpdateCategoryDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }
    }
}
