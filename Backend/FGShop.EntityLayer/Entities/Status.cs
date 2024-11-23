using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.EntityLayer.Entities
{
    public class Status: BaseEntity
    {
        public string? StatusName { get; set; }
        public ICollection<Order>? Orders { get; set; }


    }
}
