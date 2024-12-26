using FGShop.WebUI.Models.CartModels;
using FGShop.WebUI.Models.CategoryModels;
using FGShop.WebUI.Models.EFLikeModels;
using FGShop.WebUI.Models.ProducthasCategoryModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;

namespace FGShop.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();


            //Cart Verileri Getirme

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = Convert.ToInt32(userIdClaim);

            var response = await client.GetAsync($"https://localhost:7171/api/EFBaskets/{userId}");

            var jsonString = await response.Content.ReadAsStringAsync();
            var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GetCartDetailList>>(jsonString);
            ViewBag.Cart = cart;


			var response1 = await client.GetAsync($"https://localhost:7171/api/EFLikes/GetByUserIdGetAllLikes/{userId}");
			var jsonString1 = await response1.Content.ReadAsStringAsync();
			var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GetByUserIdGetAllLikes>>(jsonString1);
			ViewBag.LikeCart = model;


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
