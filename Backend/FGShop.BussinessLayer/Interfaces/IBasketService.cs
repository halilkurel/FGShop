using FGShop.CommanLayer;
using FGShop.DtoLayer.BasketDtos;
using FGShop.DtoLayer.EFOrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
	public interface IBasketService
	{
		Task<IResponse<List<ResultBasketDto>>> GetAll();

		
		Task<IResponse<CreateBasketDto>> Create(CreateBasketDto dto);
		Task<IResponse<IDto>> GetById<IDto>(int id);
		Task<IResponse> Remove(int id);
		Task<IResponse<UpdateBasketDto>> Update(UpdateBasketDto dto);
	}
}
