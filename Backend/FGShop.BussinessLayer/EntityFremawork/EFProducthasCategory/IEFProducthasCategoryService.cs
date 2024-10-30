using FGShop.DtoLayer.CategoryDtos;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.ProducthasCategoryDtos;
using FGShop.DtoLayer.ProducthasColorDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProducthasCategory
{
    public interface IEFProducthasCategoryService
    {
        Task<List<ResultCategoryDto>> GetByProductIdCategory(int id);
        Task<List<ResultProducthasCategoryDto>> GetByProductIdProducthasCategoryList(int id);
    }
}
