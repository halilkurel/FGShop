using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/AdminUILayoutPartial")]

    public class AdminUILayoutPartialController : Controller
    {
        [Route("Header")]
        public PartialViewResult Header()
        {
            return PartialView();
        }
    }
}
