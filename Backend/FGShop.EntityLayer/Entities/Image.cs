using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.EntityLayer.Entities
{
    public class Image: BaseEntity
    {
        public string ImageUrl { get; set; }
        // Navigation property
        public ICollection<ProducthasImage> producthasImages { get; set; }
    }
}
