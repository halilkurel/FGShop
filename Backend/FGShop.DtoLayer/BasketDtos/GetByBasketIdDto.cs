﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.BasketDtos
{
	public class GetByBasketIdDto
	{
		public int Id { get; set; }
		public int? UserId { get; set; }
		public int? ProductId { get; set; }
		public int? ColorId { get; set; }
		public int? SizeId { get; set; }
		public int? OrderQuantity { get; set; }
	}
}
