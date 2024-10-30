using FGShop.WebUI.Models.ColorModels;
using FGShop.WebUI.Models.SizeModels;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.Areas.Admin.ViewComponents
{
    public class _ProductProductISizeComponentPartial: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductProductISizeComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.Id = id;
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7171/api/EFProducthasSizes/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SizeResult>>(jsonString);

                return View(list);
            }
            else
            {
                return View(new List<SizeResult>());
            }
        }
    }
}
