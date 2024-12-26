using FGShop.WebUI.Models.AboutModels;
using FGShop.WebUI.Models.CartModels;
using FGShop.WebUI.Models.EFLikeModels;
using FGShop.WebUI.Models.SizeModels;
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


			var response1 = await client.GetAsync($"https://localhost:7171/api/EFLikes/GetByUserIdGetAllLikes/{userId}");
			var jsonString1 = await response1.Content.ReadAsStringAsync();
			var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GetByUserIdGetAllLikes>>(jsonString1);
			ViewBag.LikeCart = model;

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


		[HttpPost]
		public async Task<IActionResult> UpdateUserPageProfile(UserInformationModel model)
		{
			var client = _httpClientFactory.CreateClient();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            await client.PutAsync($"https://localhost:7171/api/UserInformations/", content);
			return RedirectToAction("Index","UserPage");
        }


	}
}
