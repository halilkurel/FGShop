using FGShop.WebUI.Areas.Admin.Models.ProdcutModels;
using FGShop.WebUI.Models.ProductModels;
using FGShop.WebUI.Models.SizeModels;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.Areas.Admin.ViewComponents
{
    public class _ProductDescriptionComponentPartial: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductDescriptionComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.Id = id;
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7171/api/Products/{id}");

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
