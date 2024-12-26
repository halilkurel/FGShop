using FGShop.WebUI.Models.LikeModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace FGShop.WebUI.Controllers
{
	public class LikeController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public LikeController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}


		
		[HttpPost]
		public async Task<IActionResult> CreateLike([FromBody] CreateLikeModel model)
		{

			var client = _httpClientFactory.CreateClient();

			//UserId yi çekiyoruz
			var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var userId = Convert.ToInt32(userIdClaim);

			var models = new CreateLikeModel
			{
				UserId = userId,
				ProductId = model.ProductId,
			};

			var jsonData = JsonConvert.SerializeObject(models);
			StringContent stringContent = new(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:7171/api/Likes", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return Json(new { success = true });
			}
			else
			{
				return Json(new { success = false });

				
			}

		}

		[HttpDelete]
		public async Task<IActionResult> DeleteLike([FromBody] CreateLikeModel model)
		{
			var client = _httpClientFactory.CreateClient();

			//UserId yi çekiyoruz
			var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var userId = Convert.ToInt32(userIdClaim);

			var response1 = await client.GetAsync($"https://localhost:7171/api/EFLikes/GetByProductIdandUserId/{model.ProductId}/{userId}");
			var result1 = await response1.Content.ReadAsStringAsync();
			int likeId = JsonConvert.DeserializeObject<int>(result1);

			var responseDelete = await client.DeleteAsync($"https://localhost:7171/api/Likes/{likeId}");


			if (responseDelete.IsSuccessStatusCode)
			{
				return Json(new { success = true });
			}
			else { return Json(new { success = false }); }
		}


		[HttpGet]
		public async Task<IActionResult> CheckLikeStatus(int productId)
		{
			var client = _httpClientFactory.CreateClient();
			//UserId yi çekiyoruz
			var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var userIdd = Convert.ToInt32(userIdClaim);

			var response = await client.GetAsync($"https://localhost:7171/api/EFLikes/CheckLikeStatusGetByProductIdandUserId/{productId}/{userIdd}");

			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadAsStringAsync();
				bool isLiked = JsonConvert.DeserializeObject<bool>(result);
				return Json(new { success = true, isLiked });
			}
			return Json(new { success = false });
		}


	}
}
