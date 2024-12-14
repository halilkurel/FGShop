using FGShop.CommanLayer;
using FGShop.DtoLayer.ProducthasCategoryDtos;
using FGShop.DtoLayer.ProducthasColorAndProducthasSizeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
	public interface IProducthasColorAndSizeService
	{
		Task<IResponse<List<ResultProducthasColorAndSizeDto>>> GetAll();
		Task<IResponse<CreateProducthasColorAndSizeDto>> Create(CreateProducthasColorAndSizeDto dto);
		Task<IResponse<IDto>> GetById<IDto>(int id);
		Task<IResponse> Remove(int id);
		Task<IResponse<UpdateProducthasColorAndSizeDto>> Update(UpdateProducthasColorAndSizeDto dto);
	}
}
