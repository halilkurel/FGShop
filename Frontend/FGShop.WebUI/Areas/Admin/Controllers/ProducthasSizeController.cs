using FGShop.WebUI.Models.SizeModels;
using FGShop.WebUI.Models.ProducthasSizeModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using FGShop.WebUI.Models.ColorModels;
using FGShop.WebUI.Models.ProducthasColorModels;
using System.Drawing;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProducthasSize")]
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
            var response = await httpClient.GetAsync("https://localhost:7171/api/Sizes");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var sizes = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultSizeModel>(jsonString);
                ViewBag.Sizes = sizes;
                return View();
            }
            else
            {
                return View();
            }

        }


        [HttpPost]
        [Route("CreateProducthasSize")]
        public async Task<IActionResult> CreateProducthasSize(CreateProducthasSizeModel model)
        {

            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient();

                foreach (var sizeId in model.SizeId)
                {
                    var producthassize = new CreateProducthasSizeApiModel
                    {
                        SizeId = sizeId,
                        ProductId = model.ProductId

                    };

                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(producthassize), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync("https://localhost:7171/api/ProducthasSizes", content);
                }

                return Json(new { success = true, redirectUrl = Url.Action("CreateProducthasSize", "ProducthasSize", new { area = "Admin", id = model.ProductId }) });

            }
            return View(); // Hatalı durumlarda view geri dön
        }





        [HttpGet("{id}")]
        [Route("UpdateProducthasSize")]
        public async Task<IActionResult> UpdateProducthasSize(int id)
        {
            ViewBag.ProductId = id;
            var httpClient = _httpClientFactory.CreateClient();
            var sizes = await httpClient.GetAsync("https://localhost:7171/api/Sizes");

            if (sizes.IsSuccessStatusCode)
            {
                var jsonString = await sizes.Content.ReadAsStringAsync();
                var sizeList = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultSizeModel>(jsonString);
                ViewBag.Sizes = sizeList;
            }


            var selectedSizes = await httpClient.GetAsync($"https://localhost:7171/api/EFProducthasSizes/{id}");
            if (selectedSizes.IsSuccessStatusCode)
            {
                var jsonString2 = await selectedSizes.Content.ReadAsStringAsync();
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SizeResult>>(jsonString2);
                ViewBag.SelectedSizes = list;
            }


            return View();



        }


        [HttpPost]
        [Route("UpdateProducthasSize")]
        public async Task<IActionResult> UpdateProducthasSize(UpdateProducthasSizeModel model)
        {
            var ProductId = model.ProductId;
            var httpClient = _httpClientFactory.CreateClient();
            var sizes = await httpClient.GetAsync($"https://localhost:7171/api/EFProducthasSizes/GetByProductIdProducthasSizeList/{ProductId}");
            if (sizes.IsSuccessStatusCode)
            {
                var jsonString = await sizes.Content.ReadAsStringAsync();
                var sizeList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultProducthasSizeModel>>(jsonString);

                foreach (var item in sizeList)
                {
                    var url = $"https://localhost:7171/api/ProducthasSizes/{item.Id}";
                    var response = await httpClient.DeleteAsync(url);
                }

                foreach (var sizeId in model.SizeId)
                {
                    var producthassize = new CreateProducthasSizeApiModel
                    {
                        SizeId = sizeId,
                        ProductId = ProductId

                    };

                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(producthassize), Encoding.UTF8, "application/json");
                    await httpClient.PostAsync("https://localhost:7171/api/ProducthasSizes", content);


                }
                return Json(new { success = true, redirectUrl = Url.Action("Index", "ProductDetail", new { area = "Admin", id = model.ProductId }) });


            }
            return View();
        }




    }
}
