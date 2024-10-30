using FGShop.DtoLayer.ProducthasStockDtos;
using FGShop.DtoLayer.SizeDtos;
using FGShop.DtoLayer.StockDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProducthasStock
{
    public interface IEFProducthasStockService
    {
        Task<List<ResultStockDto>> GetByProductIdStock(int id);
        Task<ResultProducthasStockDto> GetByProductIdProducthasStockList(int id);
    }
}
