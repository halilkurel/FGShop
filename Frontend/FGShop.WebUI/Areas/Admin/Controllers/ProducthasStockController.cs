using FGShop.WebUI.Models.ImageModels;
using FGShop.WebUI.Models.ProducthasStockModels;
using FGShop.WebUI.Models.StockModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using static FGShop.WebUI.Models.StockModels.UpdateStockModel;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProducthasStock")]
    public class ProducthasStockController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProducthasStockController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{id}")]
        [Route("CreateProducthasStock")]
        public async Task<IActionResult> CreateProducthasStock(int id)
        {
            ViewBag.ProductId = id;

            return View();

        }


        [HttpPost]
        [Route("CreateProducthasStock")]
        public async Task<IActionResult> CreateProducthasStock(CreateProducthasStockModel model)
        {

            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient();

                //Gelen Veriden Stock için Create Yapıyoruz

                var stockmodel = new CreateStockModel
                {
                    StockQuantity = model.StockQuantity,
                };

                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(stockmodel), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://localhost:7171/api/Stocks", content);


                //Son eklenen stock id yi alıyoruz

                var responseMessage2 = await httpClient.GetAsync("https://localhost:7171/api/Stocks");
                var stockJson = await responseMessage2.Content.ReadAsStringAsync();
                var stockResultModel = JsonConvert.DeserializeObject<ResultStockModel>(stockJson);

                var lastStockId = stockResultModel?.Data?.OrderByDescending(p => p.Id)?.FirstOrDefault()?.Id;


                //Yeni ProducthasStock modeli için veri ataması yapıyoruz
                var modelApi = new CreateProducthasStockModelApi()
                {
                    ProductId = model.ProductId,
                    StockId = lastStockId.Value,
                };


                // ProducthasStoct api sine create işlemi yapıyoruz
                var content2 = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(modelApi), Encoding.UTF8, "application/json");
                await httpClient.PostAsync("https://localhost:7171/api/ProducthasStocks", content2);

                return Json(new { success = true, redirectUrl = Url.Action("Index", "Product", new { area = "Admin", id = model.ProductId }) });

            }
            return View(); // Hatalı durumlarda view geri dön
        }

        [HttpGet("{id}")]
        [Route("UpdateProducthasStock")]
        public async Task<IActionResult> UpdateProducthasStock(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseProducthasStock = await client.GetAsync($"https://localhost:7171/api/EFProducthasStocks/GetByProductIdProducthasStockList/{id}");
            var jsonProducthasStock = await responseProducthasStock.Content.ReadAsStringAsync();
            var producthasStockModel= JsonConvert.DeserializeObject<ResultProducthasStockModel>(jsonProducthasStock);
            var StockId = producthasStockModel.StockId;


            var responseStock = await client.GetAsync($"https://localhost:7171/api/Stocks/{StockId}");
            var jsonStock = await responseStock.Content.ReadAsStringAsync();
            var stockModel = JsonConvert.DeserializeObject<GetByStockIdModel>(jsonStock);
            ViewBag.StockId = StockId;
            ViewBag.StockQuantity = stockModel?.Data?.StockQuantity;
            ViewBag.ProductId = id;
            return View();
        }


        [HttpPost]
        [Route("UpdateProducthasStock")]
        public async Task<IActionResult> UpdateProducthasStock(UpdateStock model, int productId)
        {
            var client = _httpClientFactory.CreateClient();

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            var response =await client.PutAsync($"https://localhost:7171/api/Stocks/", content);
            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "ProductDetail", new { area = "Admin", id = productId }) });
            }
            return View();
        }
    }
}
