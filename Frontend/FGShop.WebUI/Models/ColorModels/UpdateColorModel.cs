using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Models.ColorModels
{
    public class UpdateColorModel
    {
        public UpdateColor? Data { get; set; }
        public ValidationError? ValidationErrors { get; set; }
        public string? Message { get; set; }
        public int? ResponseType { get; set; }
    }
    public class UpdateColor
    {
        public int? Id { get; set; }
        public string? ColorName { get; set; }
    }
}
