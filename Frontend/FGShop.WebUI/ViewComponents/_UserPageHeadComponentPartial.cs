using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.ViewComponents
{
    public class _UserPageHeadComponentPartial: ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
