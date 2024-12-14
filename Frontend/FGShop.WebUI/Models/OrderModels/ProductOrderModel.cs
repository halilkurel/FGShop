namespace FGShop.WebUI.Models.OrderModels
{
    public class ProductOrderModel
    {
        public int BasketId { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }



		public string? ProductName { get; set; }
		public int? OrderQuantity { get; set; }
		public string? SizeName { get; set; }
		public string? ColorName { get; set; }
		public int? StatusId { get; set; }
		public DateTime? OrderDate { get; set; }





	}
}
