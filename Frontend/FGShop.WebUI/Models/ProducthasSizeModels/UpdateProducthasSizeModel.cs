namespace FGShop.WebUI.Models.ProducthasSizeModels
{
    public class UpdateProducthasSizeModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public List<int> SizeId { get; set; }
    }
}
