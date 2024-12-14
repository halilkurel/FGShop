using FGShop.WebUI.Models.ColorModels;
using FGShop.WebUI.Models.EFProducthasColorModels;
using FGShop.WebUI.Models.ProducthasColorModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProducthasColor")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ProducthasColorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProducthasColorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{id}")]
        [Route("CreateProducthasColor")]
        public async Task<IActionResult> CreateProducthasColor(int id)
        {
            ViewBag.ProductId = id;
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Colors");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var colors = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultColorModel>(jsonString);
                ViewBag.Colors = colors;
                return View();
            }
            else
            {
                return View();
            }

        }

        [HttpPost]
        [Route("CreateProducthasColor")]
        public async Task<IActionResult> CreateProducthasColor(CreateProducthasColorModel model)
        {

            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient();

                foreach (var colorId in model.ColorId)
                {
                    var producthascolor = new CreateProducthasColorApiModel
                    {
                        ColorId = colorId,
                        ProductId = model.ProductId

                    };

                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(producthascolor), Encoding.UTF8, "application/json");
                     await httpClient.PostAsync("https://localhost:7171/api/ProducthasColors", content);
                }

                return Json(new { success = true, redirectUrl = Url.Action("CreateProducthasSize", "ProducthasSize", new { area = "Admin", id = model.ProductId }) });

            }
            return View(); // Hatalı durumlarda view geri dön
        }


        [HttpGet("{id}")]
        [Route("UpdateProducthasColor")]
        public async Task<IActionResult> UpdateProducthasColor(int id)
        {
            ViewBag.ProductId = id;
            var httpClient = _httpClientFactory.CreateClient();
            var colors = await httpClient.GetAsync("https://localhost:7171/api/Colors");

            if (colors.IsSuccessStatusCode)
            {
                var jsonString = await colors.Content.ReadAsStringAsync();
                var colorList = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultColorModel>(jsonString);
                ViewBag.Colors = colorList;
            }


            var selectedColors = await httpClient.GetAsync($"https://localhost:7171/api/EFProducthasColors/GetByProductIdProducthasColorList/{id}");
            if (selectedColors.IsSuccessStatusCode)
            {
                var jsonString2 = await selectedColors.Content.ReadAsStringAsync();
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultEFProducthasColorModel>>(jsonString2);
                ViewBag.SelectedColors = list;
            }

            return View();

        }


        [HttpPost]
        [Route("UpdateProducthasColor")]
        public async Task<IActionResult> UpdateProducthasColor([FromBody] UpdateProducthasColorRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var baseUrl = "https://localhost:7171/api/ProducthasColors"; // API base URL
            // Ekleme işlemleri
            foreach (var model in request.AddModels)
            {
                if (model.Id == 0)
                {
                    var postResponse = await client.PostAsJsonAsync(baseUrl, model);
                    if (!postResponse.IsSuccessStatusCode)
                    {
                        return Json(new { success = false, message = "Post işlemi başarısız oldu." });
                    }
                }
            }

            // Silme işlemleri
            foreach (var removeModel in request.RemoveModels)
            {
                var deleteUrl = $"{baseUrl}/{removeModel.Id}";
                var deleteResponse = await client.DeleteAsync(deleteUrl);
                if (!deleteResponse.IsSuccessStatusCode)
                {
                    return Json(new { success = false, message = "Delete işlemi başarısız oldu." });
                }
            }

            return Json(new { success = true, redirectUrl = $"/Admin/ProducthasSize/UpdateProducthasSize?id={request.productId}" });
        }




    }
}
