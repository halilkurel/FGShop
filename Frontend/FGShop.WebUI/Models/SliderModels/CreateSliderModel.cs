﻿namespace FGShop.WebUI.Models.SliderModels
{
    public class CreateSliderModel
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? SliderImage { get; set; }
    }
}
