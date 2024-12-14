using FGShop.WebUI.Models.EFProducthasColorAndSizeModels;
using FGShop.WebUI.Models.EFProducthasColorModels;
using FGShop.WebUI.Models.ProducthasColorAndSizehasStockModels;
using FGShop.WebUI.Models.ProducthasColorModels;
using FGShop.WebUI.Models.ProducthasSizeModels;
using FGShop.WebUI.Models.ProducthasStockModels;
using FGShop.WebUI.Models.StockModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using System.Text;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProducthasStock")]
    [Authorize(Policy = "RequireAdministratorRole")]
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
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7171/api/EFProducthasColorAndSizes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var colors = JsonConvert.DeserializeObject<List<ResultEFProducthasColorAndSizeModel>>(jsonString);
                ViewBag.Data = colors;
            }
            return View();
        }

        [HttpPost]
        [Route("CreateProducthasStock")]
        public async Task<IActionResult> CreateProducthasStock([FromBody] List<CreateProducthasColorAndSizehasStockModel> models)
        {
            var client = _httpClientFactory.CreateClient();

            if (models == null || !models.Any())
            {
                return Json(new { success = false, errors = "Gönderilen veri boş." });
            }

            foreach (var model in models)
            {


                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                await client.PostAsync("https://localhost:7171/api/ProducthasColorAndSizehasStocks", content);


                if (model.Stock <= 0)
                {
                    return Json(new { success = false, errors = "Stok miktarı sıfırdan büyük olmalıdır." });
                }

            }

            return Json(new { success = true, redirectUrl = "/Admin/Product/Index" });
        }


        [HttpGet("{id}")]
        [Route("UpdateProducthasStock")]
        public async Task<IActionResult> UpdateProducthasStock(int id)
        {
            ViewBag.ProductId=id;
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7171/api/EFProducthasStocks/GetByProductIdAndColorSizeStock/{id}");
            if( response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var list = JsonConvert.DeserializeObject<List<ResultEFProducthasColorAndSizeModel1>>(jsonString);
                ViewBag.List = list;
            }
            return View();
        }

        [HttpPost]
        [Route("UpdateProducthasStock")]
        public async Task<IActionResult> UpdateProducthasStock([FromBody] List<UpdateProducthasStockModel> models)
        {
            if (models == null || models.Count == 0)
                return Json(new { success = false, message = "Gönderilen veri boş olamaz." });

            var client = _httpClientFactory.CreateClient();
            var baseUrl = "https://localhost:7171/api/ProducthasColorAndSizehasStocks";

            foreach (var item in models)
            {
                if (item.Id == 0)
                {
                    var createApiModel = new CreateProducthasColorAndSizehasStockModel
                    {
                        ProducthasColorAndSizeId = item.ProducthasColorAndSizeId,
                        Stock = item.Stock ?? 0
                    };

                    var postResponse = await client.PostAsJsonAsync(baseUrl, createApiModel);
                    if (!postResponse.IsSuccessStatusCode)
                        return Json(new { success = false, message = "Yeni stok eklenemedi." });
                }
                else
                {
                    var putResponse = await client.PutAsJsonAsync($"{baseUrl}", item);
                    if (!putResponse.IsSuccessStatusCode)
                        return Json(new { success = false, message = "Stok güncelleme başarısız." });
                }
            }

            return Json(new { success = true });
        }



    }
}
