namespace FGShop.WebUI.Models.ProductModels
{
	public class ResultLast6ProductModel
	{
		public int Id { get; set; }
		public string ProductName { get; set; }
		public string? CoverPhoto { get; set; }
		public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Description2 { get; set; }
    }
}
