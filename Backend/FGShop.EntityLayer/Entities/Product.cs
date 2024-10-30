using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.EntityLayer.Entities
{
    public class Product: BaseEntity
    {
        public string ProductName { get; set; }
        public string? CoverPhoto { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Description2 { get; set; }


        // Navigation property
        public ICollection<ProducthasCategory> ProducthasCategories { get; set; }
        public ICollection<ProducthasImage> producthasImages { get; set; }
        public ICollection<ProducthasSize> producthasSizes { get; set; }
        public ICollection<ProducthasColor> ProducthasColors { get; set; }
        public ICollection<ProducthasStock> ProducthasStocks { get; set; }



    }
}
