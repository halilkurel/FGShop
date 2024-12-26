using FGShop.WebUI.Models.AboutModels;
using FGShop.WebUI.Models.CartModels;
using FGShop.WebUI.Models.CategoryModels;
using FGShop.WebUI.Models.ColorModels;
using FGShop.WebUI.Models.EFProductsModel;
using FGShop.WebUI.Models.SizeModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Security.Claims;
using static FGShop.WebUI.Models.StockModels.ResultStockModel;

namespace FGShop.WebUI.Controllers
{
	public class _WebUILayoutPartialController : Controller
	{

		private readonly IHttpClientFactory _httpClientFactory;


        public _WebUILayoutPartialController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public PartialViewResult _WebUILayoutHeaderModalSearchPartial()
		{
			return PartialView();
        }

		[HttpGet]
		public async Task<IActionResult> _WebUILayoutCardPartial()
		{
			return PartialView();
        }

		[HttpGet]
		public async Task<IActionResult> _WebUILayoutLikePartial()
		{
			return PartialView();
		}



		public PartialViewResult _WebUILayoutFooterPartial()
		{
			return PartialView();
		}
	}
}
