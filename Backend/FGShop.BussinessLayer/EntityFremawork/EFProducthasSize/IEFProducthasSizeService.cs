using FGShop.DtoLayer.EFProducthasColorAndSizeDto;
using FGShop.DtoLayer.ProducthasColorAndProducthasSizeDtos;
using FGShop.DtoLayer.SizeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProducthasSize
{
    public interface IEFProducthasSizeService
    {
        Task<List<ResultSizeDto>> GetByProductId(int productId);
    }
}
