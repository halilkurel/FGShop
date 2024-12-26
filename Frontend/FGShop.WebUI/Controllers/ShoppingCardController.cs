using FGShop.WebUI.Models.CartModels;
using FGShop.WebUI.Models.EFLikeModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FGShop.WebUI.Controllers
{
	public class ShoppingCardController : Controller
	{
        private readonly IHttpClientFactory _httpClientFactory;

        public ShoppingCardController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
		{
            //Cart Verileri Getirme
            var client = _httpClientFactory.CreateClient();
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = Convert.ToInt32(userIdClaim);

            var response = await client.GetAsync($"https://localhost:7171/api/EFBaskets/{userId}");

            var jsonString = await response.Content.ReadAsStringAsync();
            var cart = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GetCartDetailList>>(jsonString);
            ViewBag.Cart = cart;
            ViewBag.UserId = userId;


			var response1 = await client.GetAsync($"https://localhost:7171/api/EFLikes/GetByUserIdGetAllLikes/{userId}");
			var jsonString1 = await response1.Content.ReadAsStringAsync();
			var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GetByUserIdGetAllLikes>>(jsonString1);
			ViewBag.LikeCart = model;

			return View(cart);
		}
	}
}
