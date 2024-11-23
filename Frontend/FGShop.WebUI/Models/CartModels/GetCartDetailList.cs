namespace FGShop.WebUI.Models.CartModels
{
	public class GetCartDetailList
	{
        public int? Id { get; set; }
        public int? ProductId { get; set; }
		public string? ProductName { get; set; }
		public string? CoverPhoto { get; set; }
		public decimal? Price { get; set; }
		public int? OrderQuantity { get; set; }

		public int? SizeId { get; set; }
		public string? SizeName { get; set; }

		public int? ColorId { get; set; }
		public string? ColorName { get; set; }
	}
}
