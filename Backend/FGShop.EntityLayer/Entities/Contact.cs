using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.EntityLayer.Entities
{
    public class Contact: BaseEntity
    {
        public string Message { get; set; }
        public string Email { get; set; }

    }
}
