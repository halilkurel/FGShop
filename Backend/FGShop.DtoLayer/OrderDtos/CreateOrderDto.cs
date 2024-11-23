using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.OrderDtos
{
    public class CreateOrderDto
    {
        public int UserId { get; set; }
        public int OrderQuantity { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int StatusId { get; set; }
        public int UserAddressId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
