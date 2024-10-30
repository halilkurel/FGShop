using FGShop.CommanLayer;
using FGShop.DtoLayer.ImageDtos;
using FGShop.DtoLayer.ProducthasImageDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProducthasImage
{
    public interface IEFProducthasImageService
    {
        Task<List<ResultImageDto>> GetByProductIdImage(int id);
    }
}
