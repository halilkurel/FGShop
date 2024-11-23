using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.EntityLayer.Entities
{
    public class Size:BaseEntity
    {
        public string? SizeName { get; set; }
        public ICollection<ProducthasSize>? producthasSizes { get; set; }
        public ICollection<Order>? Orders { get; set; }
    }
}
