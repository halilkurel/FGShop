namespace FGShop.WebUI.Models.ProducthasImageModels
{
    public class CreateProducthasImageParameterModel
    {
        public int ProductId { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
