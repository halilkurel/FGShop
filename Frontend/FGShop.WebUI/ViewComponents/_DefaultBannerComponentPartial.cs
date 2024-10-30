using FGShop.WebUI.Models.CategoryModels;
using FGShop.WebUI.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FGShop.WebUI.ViewComponents
{
	public class _DefaultBannerComponentPartial: ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _DefaultBannerComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7171/api/Categories/GetFirst3Category");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<Result3CategoryModel>>(jsonData);
				return View(values);
			}
			return View();
		}
	}
}
