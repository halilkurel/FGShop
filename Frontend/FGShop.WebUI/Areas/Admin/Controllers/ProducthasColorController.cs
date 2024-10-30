using FGShop.WebUI.Models.ColorModels;
using FGShop.WebUI.Models.ProducthasColorModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProducthasColor")]
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

                return Json(new { success = true, redirectUrl = Url.Action("CreateProducthasStock", "ProducthasStock", new { area = "Admin", id = model.ProductId }) });

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


            var selectedColors = await httpClient.GetAsync($"https://localhost:7171/api/EFProducthasColors/{id}");
            if (selectedColors.IsSuccessStatusCode)
            {
                var jsonString2 = await selectedColors.Content.ReadAsStringAsync();
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ColorResult>>(jsonString2);
                ViewBag.SelectedColors = list;
            }


            return View();



        }


        [HttpPost]
        [Route("UpdateProducthasColor")]
        public async Task<IActionResult> UpdateProducthasColor(UpdateProducthasColorModel model)
        {
            var ProductId = model.ProductId;
            var httpClient = _httpClientFactory.CreateClient();
            var colors = await httpClient.GetAsync($"https://localhost:7171/api/EFProducthasColors/GetByProductIdProducthasColorList/{ProductId}");
            if (colors.IsSuccessStatusCode)
            {
                var jsonString = await colors.Content.ReadAsStringAsync();
                var colorList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultProducthasColorModel>>(jsonString);

                foreach (var item in colorList)
                {
                    var url = $"https://localhost:7171/api/ProducthasColors/{item.Id}";
                    var response = await httpClient.DeleteAsync(url);
                }

                foreach (var colorId in model.ColorId)
                {
                    var producthascolor = new CreateProducthasColorApiModel
                    {
                        ColorId = colorId,
                        ProductId = ProductId

                    };

                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(producthascolor), Encoding.UTF8, "application/json");
                    await httpClient.PostAsync("https://localhost:7171/api/ProducthasColors", content);


                }
                return Json(new { success = true, redirectUrl = Url.Action("Index", "ProductDetail", new { area = "Admin", id = model.ProductId }) });


            }
            return View();
        }


    }
}
