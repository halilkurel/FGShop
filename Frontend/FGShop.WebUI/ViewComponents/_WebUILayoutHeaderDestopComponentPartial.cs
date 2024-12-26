using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FGShop.WebUI.ViewComponents
{
	public class _WebUILayoutHeaderDestopComponentPartial: ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{

			var userIdClaim = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
			if (userIdClaim == null)
			{
				userIdClaim = "1";
			}
			var userId = Convert.ToInt32(userIdClaim);
			ViewBag.UserId = userId;

			return View();
		}
	}
}
