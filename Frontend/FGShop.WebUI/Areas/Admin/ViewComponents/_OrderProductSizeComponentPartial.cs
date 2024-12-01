using FGShop.WebUI.Models.SizeModels;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.Areas.Admin.ViewComponents
{
    public class _OrderProductSizeComponentPartial: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _OrderProductSizeComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.Id = id;
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7171/api/Sizes/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<GetBySizeIdModel>(jsonString);

                return View(list);
            }
            else
            {
                return View(new GetBySizeIdModel());
            }
        }
    }
}
