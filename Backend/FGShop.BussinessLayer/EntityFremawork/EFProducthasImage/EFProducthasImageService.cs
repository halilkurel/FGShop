using AutoMapper;
using FGShop.DataAccessLayer.Context;
using FGShop.DtoLayer.ImageDtos;
using FGShop.DtoLayer.ProducthasImageDto;
using FGShop.EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProducthasImage
{
    public class EFProducthasImageService : IEFProducthasImageService
    {
        private readonly FGShopContext _context;
        private readonly IMapper _mapper;

        public EFProducthasImageService(FGShopContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ResultImageDto>> GetByProductIdImage(int id)
        {
            // 1. Adım: productId ile eşleşen ImageId'leri alıyoruz
            var producthasImageList = await _context.Set<ProducthasImage>()
                                                    .Where(x => x.ProductId == id)
                                                    .ToListAsync();

            var imageIds = producthasImageList.Select(x => x.ImageId).ToList();

            // 2. Adım: Image tablosundan bu ImageId'lere sahip resimleri çekiyoruz
            var images = await _context.Set<Image>()
                                        .Where(image => imageIds.Contains(image.Id))
                                        .ToListAsync();


            var list = _mapper.Map<List<ResultImageDto>>(images);
            return list;


        }

    }
}
