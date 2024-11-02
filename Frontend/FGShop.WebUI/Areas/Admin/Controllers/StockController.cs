using FGShop.WebUI.Models.StockModels;
using FGShop.WebUI.Models.ValdiationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Stock")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class StockController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public StockController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Stocks");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var sliders = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultStockModel>(jsonString);
                return View(sliders);
            }
            else
            {
                return View(new ResultStockModel());
            }
        }

        [HttpGet]
        [Route("CreateStock")]
        public IActionResult CreateStock()
        {
            return View();
        }


        [HttpPost]
        [Route("CreateStock")]
        public async Task<IActionResult> CreateStock(CreateStockModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7171/api/Stocks", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Stock", new { area = "Admin" }) });
            }
            else
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<List<ValidationError>>(responseContent);

                var errorDict = errors?.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return Json(new { success = false, errors = errorDict });
            }

        }



        [HttpDelete("{id}")]
        [Route("Admin/Stock/DeleteStock/{id}")]
        public async Task<IActionResult> DeleteStock(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            string url = $"https://localhost:7171/api/Stocks/{id}";

            // Öncelikle ürünün detaylarını alarak fotoğraf yolunu
            var sizeResponse = await httpClient.GetAsync(url);

            //Dosyayı Images Klasöründen silme işlemi
            if (sizeResponse.IsSuccessStatusCode)
            {
                var sizeJson = await sizeResponse.Content.ReadAsStringAsync();
                var sizeResultModel = JsonConvert.DeserializeObject<GetByStockIdModel>(sizeJson);
            }

            // Ürünü silme işlemi
            var response = await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Kategory silinemedi" });
            }
        }


        [HttpGet]
        [Route("UpdateStock")]
        public async Task<IActionResult> UpdateStock(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Stocks/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var categories = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateStockModel>(jsonString);
                return View(categories);
            }
            return View();
        }


        [HttpPut]
        [Route("UpdateStock")]
        public async Task<IActionResult> UpdateStock(UpdateStockModel model)
        {
            var httpClient = _httpClientFactory.CreateClient();


            string url = $"https://localhost:7171/api/Stocks/{model.Data.Id}";

            // Öncelikle ürünün detaylarını alarak fotoğraf yolunu
            var sizeResponse = await httpClient.GetAsync(url);


            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model.Data);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"https://localhost:7171/api/Stocks/", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Stock", new { area = "Admin" }) });
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<List<ValidationError>>(responseContent);

                var errorDict = errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return Json(new { success = false, errors = errorDict });
            }

        }
    }
}
