using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.ViewComponents
{
	public class _ProductDetailRelatedProductsComponentPartial: ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}
