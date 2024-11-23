namespace FGShop.WebUI.Models.BasketModels
{
	public class CreateBasketModel
	{
		public int? ProductId { get; set; }
		public int? UserId { get; set; }
		public int? ColorId { get; set; }
		public int? SizeId { get; set; }
		public int? OrderQuantity { get; set; }
	}
}
