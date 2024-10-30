using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Models.SliderModels
{
    public class UpdateSliderModel
    {
        public SliderUpdate? Data { get; set; }
        public ValidationError? ValidationErrors { get; set; }
        public string? Message { get; set; }
        public int ResponseType { get; set; }


        public class SliderUpdate
        {
            public int? Id { get; set; }
            public string? Title { get; set; }
            public string? Description { get; set; }
            public string? ImageUrl { get; set; }
            public IFormFile? SliderImage { get; set; }
        }
    }
}
