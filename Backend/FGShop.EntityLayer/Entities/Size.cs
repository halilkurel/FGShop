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
        public ICollection<ProducthasColorAndSize>? producthasColorAndSize { get; set; }
        public ICollection<Basket>? baskets { get; set; }
    }
}
