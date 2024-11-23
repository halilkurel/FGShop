using FGShop.WebUI.Models.AboutModels;
using FGShop.WebUI.Models.CartModels;
using FGShop.WebUI.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Security.Claims;

namespace FGShop.WebUI.Controllers
{
	public class UserPageController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public UserPageController(IHttpClientFactory httpClientFactory)
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

            if (HttpContext.User.Identity.IsAuthenticated)
			{
				// Kullanıcının adını al
				var userName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

				// Kullanıcının rolünü al
				var role = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

				if(role == "User")
				{
					var responseMessage = await client.GetAsync($"https://localhost:7171/api/UserInformations/{userName}");
					if (responseMessage.IsSuccessStatusCode)
					{
						var jsonData = await responseMessage.Content.ReadAsStringAsync();
						var values = JsonConvert.DeserializeObject<UserInformationModel>(jsonData);
						return View(values);
					}
				}
				else
				{
					return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
				}
			}
			return RedirectToAction("Index","LoginSignIn");
		}
	}
}
