namespace FGShop.WebUI.Areas.Admin.Models.ProdcutModels
{
    public class ProductListModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CoverPhoto { get; set; }
        public decimal Price { get; set; }
        public List<string> CategoryName { get; set; }
        public string? Description { get; set; }
        public string? Description2 { get; set; }
    }
}
