using FGShop.CommanLayer;
using FGShop.DtoLayer.ProducthasStockDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IProducthasStockService
    {
        Task<IResponse<List<ResultProducthasStockDto>>> GetAll();
        Task<IResponse<CreateProducthasStockDto>> Create(CreateProducthasStockDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateProducthasStockDto>> Update(UpdateProducthasStockDto dto);
    }
}
