using FGShop.WebUI.Models.EFProductsModel;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FGShop.WebUI.ViewComponents
{
	public class _ProductDetailComponentPartial: ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _ProductDetailComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IViewComponentResult> InvokeAsync(int id)
		{
			ViewBag.ProductId = id;
			var client = _httpClientFactory.CreateClient();
			string url = $"https://localhost:7171/api/EFProducts/GetByProductIdProductAllResult/{id}";
			var response = await client.GetAsync(url);
			var jsonProduct = await response.Content.ReadAsStringAsync();
			var data = JsonConvert.DeserializeObject<ResultEFProductModel>(jsonProduct);


			return View(data);
		}
	}
}
