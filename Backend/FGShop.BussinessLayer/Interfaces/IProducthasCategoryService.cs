using FGShop.CommanLayer;
using FGShop.DtoLayer.ProductDtos;
using FGShop.DtoLayer.ProducthasCategoryDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IProducthasCategoryService
    {
        Task<IResponse<List<ResultProducthasCategoryDto>>> GetAll();
		Task<List<ResultAllProducthasCategoryDto>> ResultAllProducthasCategory();
		Task<IResponse<CreateProducthasCategoryDto>> Create(CreateProducthasCategoryDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateProducthasCategoryDto>> Update(UpdateProducthasCategoryDto dto);
    }
}
