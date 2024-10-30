using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.ViewComponents
{
	public class _ProductDetailBodyComponentPartial: ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}
