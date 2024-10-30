namespace FGShop.WebUI.Models.ProducthasCategoryModels
{
    public class UpdateProducthasCategory
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public List<int>? CategoryId { get; set; }
    }
}
