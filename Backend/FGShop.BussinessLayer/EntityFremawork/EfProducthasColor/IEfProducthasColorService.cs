using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.ImageDtos;
using FGShop.DtoLayer.ProducthasColorDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EfProducthasColor
{
    public interface IEfProducthasColorService
    {
        Task<List<ResultColorDto>> GetByProductIdColor(int id);
        Task<List<ResultProducthasColorDto>> GetByProductIdProducthasColorList(int id);
    }
}
