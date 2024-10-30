using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.EntityLayer.Entities
{
    public class Stock: BaseEntity
    {
        public int? StockQuantity { get; set; }
        public ICollection<ProducthasStock> producthasSizes { get; set; }

    }
}
