﻿using FGShop.WebUI.Models.CategoryModels;
using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Models.ProductModels
{
    public class ResultProductModel
	{
		public List<ProductResult> Data { get; set; }
		public ValidationError ValidationErrors { get; set; }
		public string Message { get; set; }
		public int ResponseType { get; set; }
	}
	public class ProductResult
	{
        public int Id { get; set; }
		public string ProductName { get; set; }
		public string? CoverPhoto { get; set; }
		public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Description2 { get; set; }

    }
}
