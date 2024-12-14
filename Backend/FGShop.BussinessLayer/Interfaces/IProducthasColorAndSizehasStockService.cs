using FGShop.CommanLayer;
using FGShop.DtoLayer.ProducthasColorAndProducthasSizeDtos;
using FGShop.DtoLayer.ProducthasColorAndSizehasStockDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IProducthasColorAndSizehasStockService
    {
        Task<IResponse<List<ResultProducthasColorAndSizehasStockDto>>> GetAll();
        Task<IResponse<CreateProducthasColorAndSizehasStockDto>> Create(CreateProducthasColorAndSizehasStockDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateProducthasColorAndSizehasStockDto>> Update(UpdateProducthasColorAndSizehasStockDto dto);
    }
}
