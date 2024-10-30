using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.Controllers
{
	public class ShoppingCardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
