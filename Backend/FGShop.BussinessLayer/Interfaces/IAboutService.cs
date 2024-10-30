using FGShop.CommanLayer;
using FGShop.DtoLayer.AboutDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
	public interface IAboutService
	{
		Task<IResponse<List<ResultAboutDto>>> GetAll();
		Task<IResponse<CreateAboutDto>> Create(CreateAboutDto dto);
		Task<IResponse<IDto>> GetById<IDto>(int id);
		Task<IResponse> Remove(int id);
		Task<IResponse<UpdateAboutDto>> Update(UpdateAboutDto dto);
	}
}
