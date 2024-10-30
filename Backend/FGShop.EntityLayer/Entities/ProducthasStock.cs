using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.EntityLayer.Entities
{
    public class ProducthasStock: BaseEntity
    {
        public int ProductId { get; set; }
        public int StockId { get; set; }
        public Product Product { get; set; }
        public Stock Stock { get; set; }

    }
}
