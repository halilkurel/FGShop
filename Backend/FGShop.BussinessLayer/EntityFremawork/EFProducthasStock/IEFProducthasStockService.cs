using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.EFProducthasColorAndSizeAndStockDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProducthasStock
{
    public interface IEFProducthasStockService
    {
        Task<int> GetByProductId(int productId);
        Task<List<ResultEFProducthasColorAndSizeDto>> GetByProductIdAndColorSizeStock(int productId);
    }
}
