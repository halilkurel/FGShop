using AutoMapper;
using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.CategoryDtos;
using FGShop.DtoLayer.ProducthasCategoryDtos;
using FGShop.DtoLayer.ProducthasColorDtos;
using FGShop.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProducthasCategory
{
    public class EFProducthasCategoryService : IEFProducthasCategoryService
    {

        private readonly FGShopContext _context;
        private readonly IMapper _mapper;

        public EFProducthasCategoryService(FGShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ResultCategoryDto>> GetByProductIdCategory(int id)
        {

            var producthasCategoryList = await _context.Set<ProducthasCategory>().Where(x => x.ProductId==id).ToListAsync();

            var categoryIds = producthasCategoryList.Select(x => x.CategoryId).ToList();


            var categorys = await _context.Set<Category>()
                                        .Where(category => categoryIds.Contains(category.Id))
                                        .ToListAsync();


            var list = _mapper.Map<List<ResultCategoryDto>>(categorys);
            return list;
        }


        public async Task<List<ResultProducthasCategoryDto>> GetByProductIdProducthasCategoryList(int id)
        {
            var producthasCategoryList = await _context.Set<ProducthasCategory>()
                                                    .Where(x => x.ProductId == id)
                                                    .ToListAsync();
            var list = _mapper.Map<List<ResultProducthasCategoryDto>>(producthasCategoryList);
            return list;
        }
    }
}
