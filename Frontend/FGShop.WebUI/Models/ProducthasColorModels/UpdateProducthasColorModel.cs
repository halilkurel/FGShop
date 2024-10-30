namespace FGShop.WebUI.Models.ProducthasColorModels
{
    public class UpdateProducthasColorModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public List<int> ColorId { get; set; }
    }
}
