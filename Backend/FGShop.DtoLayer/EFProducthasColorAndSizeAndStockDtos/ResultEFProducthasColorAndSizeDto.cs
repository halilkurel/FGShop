using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.EFProducthasColorAndSizeAndStockDtos
{
    //Admin panelinde stok güncelleme sayfası için 
    public class ResultEFProducthasColorAndSizeDto
    {
        public int Id { get; set; }
        public int ProducthasColorAndSizeId { get; set; }
        public int? Stock { get; set; }
        public string? ColorName { get; set; }
        public string? SizeName { get; set; }
    }
}
