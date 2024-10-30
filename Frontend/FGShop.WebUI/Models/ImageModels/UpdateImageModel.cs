namespace FGShop.WebUI.Models.ImageModels
{
    public class UpdateImageModel
    {
        public int? Id { get; set; }
        public int? ProductId { get; set; }
        public string? ImageUrl { get; set; }
        public List<IFormFile>? ImageFile { get; set; }
    }
}
