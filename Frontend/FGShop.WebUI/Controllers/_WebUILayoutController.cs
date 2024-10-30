using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.Controllers
{
    public class _WebUILayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
