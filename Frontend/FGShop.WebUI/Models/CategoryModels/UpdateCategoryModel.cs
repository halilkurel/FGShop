using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Models.CategoryModels
{
	public class UpdateCategoryModel
	{

		public CategoryUpdate Data { get; set; }
		public ValidationError? ValidationErrors { get; set; }
		public string? Message { get; set; }
		public int ResponseType { get; set; }


		public class CategoryUpdate
		{
			public int Id { get; set; }
			public string? CategoryName { get; set; }
			public string? CoverPhoto { get; set; }
            public IFormFile? CategoryImage { get; set; }
        }
	}
}
