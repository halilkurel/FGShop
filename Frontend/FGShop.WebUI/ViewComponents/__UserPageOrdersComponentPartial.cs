using FGShop.WebUI.Models.EFOrderModels;
using FGShop.WebUI.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;

namespace FGShop.WebUI.ViewComponents
{
    public class __UserPageOrdersComponentPartial: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public __UserPageOrdersComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var userName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            var userIdClaim = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = Convert.ToInt32(userIdClaim);

            var response = await client.GetAsync($"https://localhost:7171/api/EFOrders/GetByUserNameOrders/{userName}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultEFOrderModel>>(jsonString);

            return View(model);
        }
    }
}
