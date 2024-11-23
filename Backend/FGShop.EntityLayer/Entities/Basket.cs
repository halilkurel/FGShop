using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.EntityLayer.Entities
{
	public class Basket: BaseEntity
	{
        public int? UserId { get; set; }
        public int? ProductId { get; set; }
        public int? ColorId { get; set; }
        public int? SizeId { get; set; }
        public int? OrderQuantity { get; set; }

        public ApplicationUser? User { get; set; }
        public Product? Product { get; set; }
        public Color? Color { get; set; }
        public Size? Size { get; set; }



    }
}
