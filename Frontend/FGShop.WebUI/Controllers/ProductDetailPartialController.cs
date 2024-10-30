using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.Controllers
{
	public class ProductDetailPartialController : Controller
	{
		public PartialViewResult AddReview()
		{
			return PartialView();
		}

	}
}
