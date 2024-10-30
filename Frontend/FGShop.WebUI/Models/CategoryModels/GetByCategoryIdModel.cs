using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Models.CategoryModels
{
    public class GetByCategoryIdModel
    {
        public GetByCategoryResult? Data { get; set; }
        public ValidationError? ValidationErrors { get; set; }
        public string? Message { get; set; }
        public int? ResponseType { get; set; }
    }

    public class GetByCategoryResult
    {
        public int? Id { get; set; }
        public string? ProductName { get; set; }
        public string? CoverPhoto { get; set; }
        public decimal? Price { get; set; }

    }
}
