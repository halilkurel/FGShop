namespace FGShop.WebUI.Areas.Admin.Models.ProdcutModels
{
    public class CreateProductModel
    {
        public string ProductName { get; set; }
        public string? CoverPhoto { get; set; }
        public IFormFile? ProductImage { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Description2 { get; set; }
    }
}
