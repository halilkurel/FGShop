using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Models.SliderModels
{
    public class GetBySliderIdModel
    {
        public GetBySliderResult? Data { get; set; }
        public ValidationError? ValidationErrors { get; set; }
        public string? Message { get; set; }
        public int? ResponseType { get; set; }
    }

    public class GetBySliderResult
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

    }

}
