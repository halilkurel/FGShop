using FGShop.WebUI.Models.SizeModels;
using Microsoft.AspNetCore.Mvc;
using static FGShop.WebUI.Models.StockModels.ResultStockModel;

namespace FGShop.WebUI.Areas.Admin.ViewComponents
{
    public class _ProductProductIStockComponentPartial: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductProductIStockComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id)
        {
            ViewBag.Id = id;
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7171/api/EFProducthasStocks/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultStock>>(jsonString);

                return View(list);
            }
            else
            {
                return View(new List<ResultStock>());
            }
        }
    }
}
