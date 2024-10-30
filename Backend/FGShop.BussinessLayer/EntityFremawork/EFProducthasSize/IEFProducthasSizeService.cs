using FGShop.DtoLayer.ImageDtos;
using FGShop.DtoLayer.ProucthasSizeDtos;
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
        Task<List<ResultSizeDto>> GetByProductIdSize(int id);
        Task<List<ResultProducthasSizeDto>> GetByProductIdSProducthasSizeList(int id);
    }
}
