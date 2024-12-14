using FGShop.WebUI.Models.SizeModels;
using FGShop.WebUI.Models.ProducthasSizeModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using FGShop.WebUI.Models.ColorModels;
using FGShop.WebUI.Models.ProducthasColorModels;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using FGShop.WebUI.Models.EFProducthasColorModels;
using System.Reflection;
using FGShop.WebUI.Models.EFProducthasColorAndSizeModels;
using FGShop.WebUI.Models.ProducthasStockModels;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProducthasSize")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ProducthasSizeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProducthasSizeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{id}")]
        [Route("CreateProducthasSize")]
        public async Task<IActionResult> CreateProducthasSize(int id)
        {
            ViewBag.ProductId = id;
            var httpClient = _httpClientFactory.CreateClient();

            //Önceki ekleme sayfasında product'ın Color biligileri eklendi onları getiriyoruz
            var colorResponse = await httpClient.GetAsync($"https://localhost:7171/api/EFProducthasColors/GetByProductIdProducthasColorList/{id}");
            if (colorResponse.IsSuccessStatusCode)
            {
                var colorJsonString = await colorResponse.Content.ReadAsStringAsync();
                var colors = JsonConvert.DeserializeObject<List<ResultEFProducthasColorModel>> (colorJsonString);
                ViewBag.Colors = colors;
            }


            // Burada Daha önce eklenmiş Size bilgilerini alıyoruz 
            var response = await httpClient.GetAsync("https://localhost:7171/api/Sizes");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var sizes = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultSizeModel>(jsonString).Data;
                ViewBag.Sizes = sizes;
                return View();
            }

            return View();

        }

        [HttpPost]
        [Route("CreateProducthasSize")]
        public async Task<IActionResult> CreateProducthasSize([FromBody]List<CreateProducthasSizeModel> models)
        {
            if (models == null || !models.Any())
            {
                return Json(new { success = false, errors = "Geçersiz veri gönderildi." });
            }

            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient();

                foreach (var model in models)
                {
                    foreach (var sizeId in model.SizeId)
                    {
                        var producthassize = new CreateProducthasSizeApiModel
                        {
                            SizeId = sizeId,
                            ProducthasColorId = model.ProducthasColorId
                        };

                        var content = new StringContent(
                            Newtonsoft.Json.JsonConvert.SerializeObject(producthassize),
                            Encoding.UTF8,
                            "application/json"
                        );

                        var response = await httpClient.PostAsync(
                            "https://localhost:7171/api/ProducthasColorAndSize",
                            content
                        );

                        if (!response.IsSuccessStatusCode)
                        {
                            return Json(new { success = false, errors = "API isteği başarısız oldu." });
                        }
                    }
                }

                return Json(new
                {
                    success = true,
                    redirectUrl = Url.Action("CreateProducthasStock", "ProducthasStock", new { area = "Admin", id = models.FirstOrDefault()?.ProductId })
                });
            }

            return Json(new { success = false, errors = "Model doğrulama hatası." });
        }






        [HttpGet("{id}")]
        [Route("UpdateProducthasSize")]
        public async Task<IActionResult> UpdateProducthasSize(int id)
        {
            ViewBag.ProductId = id;
            var httpClient = _httpClientFactory.CreateClient();

            //Size bilgileri
            var sizes = await httpClient.GetAsync("https://localhost:7171/api/Sizes");

            if (sizes.IsSuccessStatusCode)
            {
                var jsonString = await sizes.Content.ReadAsStringAsync();
                var sizeList = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultSizeModel>(jsonString).Data;
                ViewBag.Sizes = sizeList;
            }
            //Önceki ekleme sayfasında product'ın Color biligileri eklendi onları getiriyoruz
            var colorResponse = await httpClient.GetAsync($"https://localhost:7171/api/EFProducthasColors/GetByProductIdProducthasColorList/{id}");
            if (colorResponse.IsSuccessStatusCode)
            {
                var colorJsonString = await colorResponse.Content.ReadAsStringAsync();
                var colors = JsonConvert.DeserializeObject<List<ResultEFProducthasColorModel>>(colorJsonString);
                ViewBag.Colors = colors;
            }

            var selectedSizes = await httpClient.GetAsync($"https://localhost:7171/api/EFProducthasColorAndSizes/{id}");
            if (selectedSizes.IsSuccessStatusCode)
            {
                var jsonString2 = await selectedSizes.Content.ReadAsStringAsync();
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultEFProducthasColorAndSizeModel>>(jsonString2); 
                ViewBag.SelectedSizes = list;
            }
            return View();
        }


        [HttpPost]
        [Route("UpdateProducthasSize")]
        public async Task<IActionResult> UpdateProducthasSize([FromBody] UpdateProducthasSizeModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var baseUrl = "https://localhost:7171/api/ProducthasColorAndSize"; // API base URL
            // Ekleme işlemleri
            foreach (var item in model.SelectedSizes)
            {
                if (item.ProducthasColorAndSizeId == 0)
                {
                    var createApiModel = new CreateProducthasSizeApiModel
                    {
                        ProducthasColorId = item.ProducthasColorId,
                        SizeId = item.SizeId
                    };
                    //Ekleme için createproducthasSize modlei oluşurulacak.
                    var postResponse = await client.PostAsJsonAsync(baseUrl, createApiModel);


                    //Son eklenen ProducthasColorAndSizeId bilgisini alalım
                    var responseId = await client.GetAsync($"https://localhost:7171/api/EFProducthasColorAndSizes/LastAddedDataId/{model.ProductId}");
                    var responseIdforString = await responseId.Content.ReadAsStringAsync();
                    var responseIdResult = JsonConvert.DeserializeObject<int>(responseIdforString);

                    var createProducthasSizeApi = new CreateProducthasStockModel
                    {
                        ProducthasColorAndSizeId = responseIdResult,
                        Stock = 0
                    };

                    //ProducthasStock için ekleme işlemi yapacağız
                    var createProducthasStockUrl = "https://localhost:7171/api/ProducthasColorAndSizehasStocks";
                    var responseProducthasStock = await client.PostAsJsonAsync(createProducthasStockUrl,createProducthasSizeApi);

                    
                    if (!postResponse.IsSuccessStatusCode && responseProducthasStock.IsSuccessStatusCode)
                    {
                        return Json(new { success = false, message = "Post işlemi başarısız oldu." });
                    }
                }
            }

            // Silme işlemleri
            foreach (var removeModel in model.DeselectedSizes)
            {
                var deleteUrl = $"{baseUrl}/{removeModel.ProducthasColorAndSizeId}";
                var deleteResponse = await client.DeleteAsync(deleteUrl);
                if (!deleteResponse.IsSuccessStatusCode)
                {
                    return Json(new { success = false, message = "Delete işlemi başarısız oldu." });
                }
            }

            return Json(new { success = true });
        }

    }




}

