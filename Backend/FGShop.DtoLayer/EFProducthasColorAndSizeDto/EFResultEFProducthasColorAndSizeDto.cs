using FGShop.DtoLayer.SizeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.EFProducthasColorAndSizeDto
{
    public class EFResultEFProducthasColorAndSizeDto
    {
        public int ProductId { get; set; }
        public int ProducthasColorId { get; set; } 
        public int ProducthasColorAndSizeId { get; set; } 
        public string? ColorName { get; set; }
        public string? SizeNames { get; set; }


    }
}
