using FGShop.CommanLayer;
using FGShop.DtoLayer.SliderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
	public interface ISliderService
	{
		Task<IResponse<List<ResultSliderDto>>> GetAll();
		Task<IResponse<CreateSliderDto>> Create(CreateSliderDto dto);
		Task<IResponse<IDto>> GetById<IDto>(int id);
		Task<IResponse> Remove(int id);
		Task<IResponse<UpdateSliderDto>> Update(UpdateSliderDto dto);
	}
}
