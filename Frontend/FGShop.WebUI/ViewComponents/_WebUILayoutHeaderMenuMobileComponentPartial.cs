using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.ViewComponents
{
	public class _WebUILayoutHeaderMenuMobileComponentPartial: ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}
