using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.ProducthasColorAndProducthasSizeDtos
{
	public class UpdateProducthasColorAndSizeDto
	{
        public int Id { get; set; }
		public int ProducthasColorId { get; set; }
		public int SizeId { get; set; }
	}
}
