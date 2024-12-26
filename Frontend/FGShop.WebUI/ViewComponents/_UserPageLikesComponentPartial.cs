using FGShop.WebUI.Models.EFLikeModels;
using FGShop.WebUI.Models.EFOrderModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FGShop.WebUI.ViewComponents
{
    public class _UserPageLikesComponentPartial: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _UserPageLikesComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var userName = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            var userIdClaim = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = Convert.ToInt32(userIdClaim);

            var response = await client.GetAsync($"https://localhost:7171/api/EFLikes/GetByUserIdGetAllLikes/{userId}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<GetByUserIdGetAllLikes>>(jsonString);

            return View(model);
        }
    }
}
