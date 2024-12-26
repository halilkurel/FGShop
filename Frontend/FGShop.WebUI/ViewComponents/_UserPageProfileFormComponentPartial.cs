using FGShop.WebUI.Models.CartModels;
using FGShop.WebUI.Models.UserModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FGShop.WebUI.ViewComponents
{
    public class _UserPageProfileFormComponentPartial: ViewComponent
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public _UserPageProfileFormComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var client = _httpClientFactory.CreateClient();


            var userIdClaim = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = Convert.ToInt32(userIdClaim);

            var response = await client.GetAsync($"https://localhost:7171/api/UserInformations/GetByUserIdInformation/{userId}");
            var jsonString = await response.Content.ReadAsStringAsync();
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<UserInformationModel>(jsonString);

            return View(model);
        }
    }
}
