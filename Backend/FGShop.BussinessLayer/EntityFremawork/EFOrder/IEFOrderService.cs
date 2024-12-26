using FGShop.DtoLayer.EFOrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFOrder
{
    public interface IEFOrderService
    {
        Task<List<ResultJoinOrderDto>> OrderList();
        Task<List<ResultJoinOrderDto>> ListCancelledOrders();
        Task<List<ResultJoinOrderDto>> ListUnapprovedOrders();
        Task<List<ResultJoinOrderDto>> ListApprovedOrders();
        Task<List<ResultJoinOrderDto>> ListOrderCompleted();
        Task<ResultJoinOrderDto> GetByOrderId(int orderId);
        Task<List<ResultJoinOrderDto>> GetByUserNameOrders(string userName);
        Task<List<ResultJoinOrderDto>> Last4OrderList();


    }
}
