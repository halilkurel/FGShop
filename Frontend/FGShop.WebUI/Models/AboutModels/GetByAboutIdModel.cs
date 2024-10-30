using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Models.AboutModels
{
    public class GetByAboutIdModel
    {
        public GetByAboutResult? Data { get; set; }
        public ValidationError? ValidationErrors { get; set; }
        public string? Message { get; set; }
        public int? ResponseType { get; set; }
    }

    public class GetByAboutResult
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Description2 { get; set; }
        public string? ImageUrl { get; set; }
    }
}
