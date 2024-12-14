using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.SizeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.EFProductDtos
{
	public class ResultEFProductDto
	{
		public int ProductId { get; set; }
		public string? ProductName { get; set; }
		public string? CoverPhoto { get; set; }
		public decimal? Price { get; set; }
		public string? Description { get; set; }
		public string? Description2 { get; set; }


        public List<ResultSizeDto>? Sizes { get; set; }
        public List<ResultColorDto>? Colors { get; set; }


    }
}
