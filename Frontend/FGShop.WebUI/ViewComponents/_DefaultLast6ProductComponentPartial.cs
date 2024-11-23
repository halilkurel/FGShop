using FGShop.WebUI.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace FGShop.WebUI.ViewComponents
{
	public class _DefaultLast6ProductComponentPartial: ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _DefaultLast6ProductComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7171/api/Products/GetLast6Product");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<ResultLast6ProductModel>>(jsonData);
				return View(values);
			}
			return View();
		}


	}

}
