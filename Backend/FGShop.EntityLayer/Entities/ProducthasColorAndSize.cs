using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.EntityLayer.Entities
{
	public class ProducthasColorAndSize: BaseEntity
	{
        public int ProducthasColorId { get; set; }
        public int SizeId { get; set; }


        public ProducthasColor? ProducthasColor { get; set; }
        public Size? Size { get; set; }
        public ICollection<ProducthasColorAndSizehasStock>? producthasColorAndSizehasStocks { get; set; }



    }
}
