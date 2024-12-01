using FGShop.CommanLayer;
using FGShop.DtoLayer.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IOrderService
    {
        Task<IResponse<List<ResultOrderDto>>> GetAll();
        Task<IResponse<CreateOrderDto>> Create(CreateOrderDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateOrderDto>> Update(UpdateOrderDto dto);
        Task StatusConfirmed(int orderId);
        Task CancelOrder(int orderId);
        Task CompletetheOrder(int orderId);
    }
}
