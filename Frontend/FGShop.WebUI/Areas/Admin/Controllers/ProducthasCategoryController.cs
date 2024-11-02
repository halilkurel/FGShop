using FGShop.WebUI.Models.CategoryModels;
using FGShop.WebUI.Models.ProducthasCategoryModels;
using FGShop.WebUI.Models.ProducthasSizeModels;
using FGShop.WebUI.Models.SizeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProducthasCategory")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ProducthasCategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProducthasCategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{id}")]
        [Route("CreateProducthasCategory")]
        public async Task<IActionResult> CreateProducthasCategory(int id)
        {
            ViewBag.ProductId = id;
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Categories");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var sliders = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultCategoryModel>(jsonString);
                ViewBag.Categories = sliders;
                return View();
            }
            else
            {
                return View();
            }
            
        }


        [HttpPost]
        [Route("CreateProducthasCategory")]
        public async Task<IActionResult> CreateProducthasCategory(CreateProducthasCategoryModel model)
        {
            
            if (ModelState.IsValid)
            {
                var httpClient = _httpClientFactory.CreateClient();

                foreach (var categoryId in model.CategoryId)
                {
                    var producthascategory = new CreateProducthasCategoryApiModel
                    {
                        CategoryId = categoryId,
                        ProductId= model.ProductId

                    };

                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(producthascategory), Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync("https://localhost:7171/api/ProducthasCategories", content);
                }

                return Json(new { success = true, redirectUrl = Url.Action("CreateProducthasSize", "ProducthasSize", new { area = "Admin", id = model.ProductId }) });

            }
            return View(); // Hatalı durumlarda view geri dön
        }


        [HttpGet("id")]
        [Route("UpdateProducthasCategory")]
        public async Task<IActionResult> UpdateProducthasCategory(int id)
        {
            ViewBag.ProductId = id;
            var httpClient = _httpClientFactory.CreateClient();
            var categories = await httpClient.GetAsync("https://localhost:7171/api/Categories");

            if (categories.IsSuccessStatusCode)
            {
                var jsonString = await categories.Content.ReadAsStringAsync();
                var categoryList = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultCategoryModel>(jsonString);
                ViewBag.Categories = categoryList;
            }


            var selectedCategory = await httpClient.GetAsync($"https://localhost:7171/api/EFProducthasCategories/{id}");
            if (selectedCategory.IsSuccessStatusCode)
            {
                var jsonString2 = await selectedCategory.Content.ReadAsStringAsync();
                var list = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CategoryResult>>(jsonString2);
                ViewBag.SelectedCategories = list;
            }


            return View();
        }


        [HttpPost]
        [Route("UpdateProducthasCategory")]
        public async Task<IActionResult> UpdateProducthasCategory(UpdateProducthasCategory model)
        {
            var ProductId = model.ProductId.Value;
            var httpClient = _httpClientFactory.CreateClient();
            var categories = await httpClient.GetAsync($"https://localhost:7171/api/EFProducthasCategories/GetByProductIdProducthasCategoryList/{ProductId}");
            if (categories.IsSuccessStatusCode)
            {
                var jsonString = await categories.Content.ReadAsStringAsync();
                var categoriyList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ListProducthasCategoryModel>>(jsonString);

                foreach (var item in categoriyList)
                {
                    var url = $"https://localhost:7171/api/ProducthasCategories/{item.Id}";
                    var response = await httpClient.DeleteAsync(url);
                }

                foreach (var categoryId in model.CategoryId)
                {
                    var producthassize = new CreateProducthasCategoryApiModel
                    {
                        CategoryId = categoryId,
                        ProductId = ProductId

                    };

                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(producthassize), Encoding.UTF8, "application/json");
                    await httpClient.PostAsync("https://localhost:7171/api/ProducthasCategories", content);


                }
                return Json(new { success = true, redirectUrl = Url.Action("Index", "ProductDetail", new { area = "Admin", id = model.ProductId }) });


            }
            return View();
        }
    }
}
