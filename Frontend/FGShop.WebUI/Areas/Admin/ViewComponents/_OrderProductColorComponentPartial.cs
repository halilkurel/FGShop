using FGShop.WebUI.Models.ColorModels;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.Areas.Admin.ViewComponents
{
    public class _OrderProductColorComponentPartial: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _OrderProductColorComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7171/api/Colors/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<GetByColorIdModel>(jsonString);

                return View(list);
            }
            else
            {
                return View(new GetByColorIdModel());
            }
        }
    }
}
