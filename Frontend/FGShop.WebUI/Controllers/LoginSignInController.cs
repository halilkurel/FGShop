using FGShop.WebUI.Models.AuthModels;
using FGShop.WebUI.Models.ErrorModels;
using FGShop.WebUI.Models.ValdiationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FGShop.WebUI.Controllers
{
    [AllowAnonymous]
	public class LoginSignInController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public LoginSignInController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<PartialViewResult> RegisterPartial()
		{
			return PartialView();
		}

        [HttpPost]
        public async Task<IActionResult> RegisterPartial(CreateRegisterModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7171/api/Auths/register", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                // Kayıt başarılı, index sayfasına yönlendir
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Default") });
            }
            else
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<List<CustomValidationError>>(responseContent); // Hataları 'CustomValidationError' modeline çeviriyoruz.

                // Hataları Dictionary formatına çeviriyoruz
                var errorDict = errors?.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return Json(new { success = false, errors = errorDict });
            }
        }




        [HttpGet]
		public async Task<PartialViewResult> SignInPartial()
		{
			return PartialView();
		}

        [HttpPost]
        public async Task<IActionResult> SignInPartial(LogInModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7171/api/Auths/login", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                // Giriş başarılı, JSON olarak 'success' mesajı dön
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Default") });
            }
            else
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<List<ValidationError>>(responseContent);

                var errorDict = errors?.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return Json(new { success = false, errors = errorDict });
            }
        }

    }
}
