using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.ProducthasCategoryDtos
{
	public class ResultAllProducthasCategoryDto
	{
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
		public string? CoverPhoto { get; set; }
		public decimal? Price { get; set; }

		public List<string>? CategoryName { get; set; }

	}
}
