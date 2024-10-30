using FGShop.WebUI.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.ViewComponents
{
	public class _ProductDetailDescriptionComponentPartial: ViewComponent
	{
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductDetailDescriptionComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int productId)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7171/api/Products/{productId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<GetByProductIdModel>(jsonString);

                return View(list);
            }
            else
            {
                return View(new GetByProductIdModel());
            }
        }

	}
}
