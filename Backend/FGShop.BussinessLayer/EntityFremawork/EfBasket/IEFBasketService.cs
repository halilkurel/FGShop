using FGShop.CommanLayer;
using FGShop.DtoLayer.BasketDtos;
using FGShop.DtoLayer.EFOrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EfOrder
{
	public interface IEFBasketService
	{
		Task<List<ResultEFOrderDto>> GetProductDetailByUserId(int userId);
		Task<List<ResultBasketDto>> GetByUserId(int userId);
        Task UserIdBasketRemove(int userId);
    }
}
