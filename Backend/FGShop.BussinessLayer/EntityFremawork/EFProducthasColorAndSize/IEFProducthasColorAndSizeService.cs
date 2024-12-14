using FGShop.DtoLayer.EFProducthasColorAndSizeDto;
using FGShop.DtoLayer.ProducthasColorAndProducthasSizeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProducthasColorAndSize
{
    public interface IEFProducthasColorAndSizeService
    {
        Task<List<EFResultEFProducthasColorAndSizeDto>> GetByProductId(int id);
        Task<int> LastAddedDataId(int productId);
    }
}
