﻿namespace FGShop.WebUI.Models.CartModels
{
	public class CartItemModel
	{
		public int ProductId { get; set; }
		public int Quantity { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }

    }
}
