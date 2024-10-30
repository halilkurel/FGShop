using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Models.SizeModels
{
    public class ResultSizeModel
    {
        public List<SizeResult>? Data { get; set; }
        public ValidationError? ValidationErrors { get; set; }
        public string? Message { get; set; }
        public int? ResponseType { get; set; }
    }
    public class SizeResult
    {
        public int? Id { get; set; }
        public string? SizeName { get; set; }
    }
}
