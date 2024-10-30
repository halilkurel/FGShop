namespace FGShop.WebUI.Models.CategoryModels
{
    public class CreateCategoryModel
    {
        public string? CategoryName { get; set; }
        public string? CoverPhoto { get; set; }
        public IFormFile? CategoryImage { get; set; }
    }

}
