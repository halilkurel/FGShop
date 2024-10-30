using FGShop.WebUI.Models.ImageModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FGShop.WebUI.ViewComponents
{
	public class _ProductDetailImagesComponentPartial: ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _ProductDetailImagesComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IViewComponentResult> InvokeAsync(int productid)
		{
			var client = _httpClientFactory.CreateClient();
			string url = $"https://localhost:7171/api/EFProducthasImages/{productid}";

			// Öncelikle ürünün detaylarını alarak fotoğraf yolunu
			var imageResponse = await client.GetAsync(url);
			if (imageResponse.IsSuccessStatusCode)
			{
				var imageJson = await imageResponse.Content.ReadAsStringAsync();
				var imageResultModel = JsonConvert.DeserializeObject<List<ImageResult>>(imageJson);
				return View(imageResultModel);
			}

			return View();
		}
	}
}
