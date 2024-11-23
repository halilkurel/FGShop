namespace FGShop.WebUI.Models.CartModels
{
    public class UpdateCartModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public int SizeId { get; set; }
        public int OrderQuantity { get; set; }

    }
}
