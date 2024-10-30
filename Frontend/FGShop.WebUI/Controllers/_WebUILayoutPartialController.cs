using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.Controllers
{
	public class _WebUILayoutPartialController : Controller
	{
		public PartialViewResult _WebUILayoutHeaderModalSearchPartial()
		{
			return PartialView();
		}

		public PartialViewResult _WebUILayoutCardPartial()
		{
			return PartialView();
		}

		public PartialViewResult _WebUILayoutFooterPartial()
		{
			return PartialView();
		}
	}
}
