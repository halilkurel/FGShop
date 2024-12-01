using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.Interfaces;
using FGShop.DtoLayer.OrderDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
    public class OrderService: IOrderService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderDto> _createValidator;
        private readonly IValidator<UpdateOrderDto> _updateValidator;
        private readonly FGShopContext _context;

        public OrderService(IUow uow, IMapper mapper, IValidator<CreateOrderDto> createValidator, IValidator<UpdateOrderDto> updateValidator, FGShopContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        public async Task<IResponse<CreateOrderDto>> Create(CreateOrderDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<Order>().Create(_mapper.Map<Order>(dto));
                await _uow.SaveChanges();

                return new Response<CreateOrderDto>(ResponseType.Success, dto);
            }

            else
            {

                return new Response<CreateOrderDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        public async Task<IResponse<List<ResultOrderDto>>> GetAll()
        {
            var data = _mapper.Map<List<ResultOrderDto>>(await _uow.GetRepository<Order>().GetAll());

            return new Response<List<ResultOrderDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Order>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<Order>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<Order>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }
        
        public async Task StatusConfirmed(int orderId)
        {
            var repository = _uow.GetRepository<Order>();

            // Güncellenmesi gereken kaydı bul
            var entity = await repository.GetByFilter(x => x.Id == orderId);

            if (entity == null)
            {
                throw new Exception("Order not found."); // Hata kontrolü
            }

            // Sadece StatusId'yi güncelle
            entity.StatusId = 5;
            repository.Update(entity);

            // Değişiklikleri kaydet
            await _uow.SaveChanges();
        }


        public async Task CancelOrder(int orderId)
        {
            var repository = _uow.GetRepository<Order>();

            // Güncellenmesi gereken kaydı bul
            var entity = await repository.GetByFilter(x => x.Id == orderId);

            if (entity == null)
            {
                throw new Exception("Order not found."); // Hata kontrolü
            }

            // Sadece StatusId'yi güncelle
            entity.StatusId = 7;
            repository.Update(entity);

            // Değişiklikleri kaydet
            await _uow.SaveChanges();
        }

        public async Task<IResponse<UpdateOrderDto>> Update(UpdateOrderDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Order>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Order>().Update(_mapper.Map<Order>(dto), updatedEntity);
                    await _uow.SaveChanges();

                    return new Response<UpdateOrderDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateOrderDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
            }
            else
            {
                return new Response<UpdateOrderDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }

        public async Task CompletetheOrder(int orderId)
        {
            var repository = _uow.GetRepository<Order>();

            // Güncellenmesi gereken kaydı bul
            var entity = await repository.GetByFilter(x => x.Id == orderId);

            if (entity == null)
            {
                throw new Exception("Order not found."); // Hata kontrolü
            }

            // Sadece StatusId'yi güncelle
            entity.StatusId = 6;
            repository.Update(entity);

            // Değişiklikleri kaydet
            await _uow.SaveChanges();
        }
    }
}
