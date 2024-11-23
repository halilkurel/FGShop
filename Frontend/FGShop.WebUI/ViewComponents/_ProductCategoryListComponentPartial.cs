using FGShop.WebUI.Models.CategoryModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FGShop.WebUI.ViewComponents
{
    public class _ProductCategoryListComponentPartial: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _ProductCategoryListComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7171/api/Categories");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<ResultCategoryModel>(jsonData);
                return View(values);
            }
            return View();
        }

    }
}
