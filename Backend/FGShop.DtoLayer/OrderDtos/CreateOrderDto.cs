using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.OrderDtos
{
    public class CreateOrderDto
    {
		public string? UserName { get; set; }
		public string? ProductName { get; set; }
		public int? OrderQuantity { get; set; }
		public string? SizeName { get; set; }
		public string? ColorName { get; set; }
		public int? StatusId { get; set; }
		public DateTime? OrderDate { get; set; }
        public int ProductId { get; set; }


        public int? ColorId { get; set; }
        public int? SizeId { get; set; }


        //Adress bilgileri
        public string? Email { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Country { get; set; }
		public string? City { get; set; }
		public string? District { get; set; }
		public string? Neighbourhood { get; set; }
		public string? Address { get; set; }



		
	}
}
