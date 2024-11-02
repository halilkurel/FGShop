using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductDetail")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ProductDetailController : Controller
    {
        [Route("Index")]
        public IActionResult Index(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
