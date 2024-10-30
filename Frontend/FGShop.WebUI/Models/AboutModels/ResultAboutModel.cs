using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Models.AboutModels
{
	public class ResultAboutModel
	{
		public List<AboutResult> Data { get; set; }
		public ValidationError ValidationErrors { get; set; }
		public string? Message { get; set; }
		public int? ResponseType { get; set; }
	}
	public class AboutResult
	{
		public int Id { get; set; }
		public string? Title { get; set; }
		public string? Description { get; set; }
		public string? Description2 { get; set; }
		public string? ImageUrl { get; set; }

	}
}
