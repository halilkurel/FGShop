using FGShop.WebUI.Models.CartModels;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Security.Claims;

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
