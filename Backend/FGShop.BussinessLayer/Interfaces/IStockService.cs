using FGShop.CommanLayer;
using FGShop.DtoLayer.StockDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IStockService
    {
        Task<IResponse<List<ResultStockDto>>> GetAll();
        Task<IResponse<CreateStockDto>> Create(CreateStockDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateStockDto>> Update(UpdateStockDto dto);
    }
}
