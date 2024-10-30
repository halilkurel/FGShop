using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Models.ImageModels
{
    public class ResultImageModel
    {
        public List<ImageResult> Data { get; set; }
        public ValidationError ValidationErrors { get; set; }
        public string Message { get; set; }
        public int ResponseType { get; set; }
    }
    public class ImageResult
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
    }
}
