namespace FGShop.WebUI.Models.ProducthasCategoryModels
{
	public class ResultProducthasCategoryModel
	{
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
		public string? CoverPhoto { get; set; }
		public decimal? Price { get; set; }
		public List<string>? CategoryName { get; set; } = new List<string>();
	}
}
