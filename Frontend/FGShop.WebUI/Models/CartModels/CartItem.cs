﻿namespace FGShop.WebUI.Models.CartModels
{
	public class CartItem
	{
		public string ProductName { get; set; }
		public decimal Price { get; set; }
		public int Quantity { get; set; }
		public string ImageUrl { get; set; }
	}
}