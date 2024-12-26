using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.EntityLayer.Entities
{
    public class Like: BaseEntity
    {
        public int? ProductId { get; set; }
        public int? UserId { get; set; }

        public Product? Product { get; set; }
        public ApplicationUser? User { get; set; }

    }
}
