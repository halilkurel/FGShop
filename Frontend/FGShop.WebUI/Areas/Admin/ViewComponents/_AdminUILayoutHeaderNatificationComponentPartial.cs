using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.Areas.Admin.ViewComponents
{
    public class _AdminUILayoutHeaderNatificationComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
