using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.EntityLayer.Entities
{
    public class ProducthasColorAndSizehasStock: BaseEntity
    {
        public int ProducthasColorAndSizeId { get; set; }
        public int Stock { get; set; }

        public ProducthasColorAndSize ProducthasColorAndSize { get; set; }

    }
}
