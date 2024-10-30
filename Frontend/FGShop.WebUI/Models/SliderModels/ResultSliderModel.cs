namespace FGShop.WebUI.Models.SliderModels
{
	public class ResultSliderModel
	{
		public List<SliderResult> Data { get; set; }
		public object ValidationErrors { get; set; }
		public string Message { get; set; }
		public int ResponseType { get; set; }
	}
	public class SliderResult
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string ImageUrl { get; set; }

	}
}
