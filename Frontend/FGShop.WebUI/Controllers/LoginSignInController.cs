using FGShop.WebUI.Models.AuthModels;
using FGShop.WebUI.Models.ErrorModels;
using FGShop.WebUI.Models.ValdiationModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
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

            // API'ye login isteği gönder
            var responseMessage = await client.PostAsync("https://localhost:7171/api/Auths/login", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                // Yanıt başarılıysa, dönen içeriği al ve deserialize et
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                var tokenValue = loginResponse.Token.ToString();
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(tokenValue);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, jwtToken.Claims.First(c => c.Type == "sub").Value), // "sub" alanını kullan
                    new Claim(ClaimTypes.Role, jwtToken.Claims.First(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value), // Rolü almak için 
                };

                // Claims'i ekle
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Token'ın sona erme süresini al
                var expirationUnix = long.Parse(jwtToken.Claims.First(c => c.Type == "exp").Value);
                var expirationDate = DateTimeOffset.FromUnixTimeSeconds(expirationUnix);

                // Kullanıcıyı oturum açtır ve token süresi kadar geçerli yap
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = expirationDate,
                    IsPersistent = true // Oturumu tarayıcı kapansa bile devam ettirmek için
                    
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return Json(new { success = true, redirectUrl = Url.Action("Index", "Default") });
            }

            // Giriş başarısızsa, hata mesajlarını işle ve dön
            var errorContent = await responseMessage.Content.ReadAsStringAsync();
            var errors = JsonConvert.DeserializeObject<List<ValidationError>>(errorContent);

            // Hataları dictionary formatına çevir
            var errorDict = errors?.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

            return Json(new { success = false, errors = errorDict });
        }



    }
}
