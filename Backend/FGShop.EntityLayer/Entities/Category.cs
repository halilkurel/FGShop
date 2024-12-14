using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.EntityLayer.Entities
{
    public class Category: BaseEntity
    {
        public string? CategoryName { get; set; }
        public string? CoverPhoto { get; set; }


        // Navigation property
        public ICollection<ProducthasCategory>? ProducthasCategories { get; set; }

    }
}
