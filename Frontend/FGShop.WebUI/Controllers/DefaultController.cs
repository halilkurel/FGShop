using FGShop.WebUI.Models.CartModels;
using FGShop.WebUI.Models.EFLikeModels;
using FGShop.WebUI.Models.ProducthasCategoryModels;
using FGShop.WebUI.Models.ProductModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;

namespace FGShop.WebUI.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();


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

            return View();
        }



    }
}
