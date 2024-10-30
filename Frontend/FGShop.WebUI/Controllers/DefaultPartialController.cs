using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.Controllers
{
	public class DefaultPartialController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
