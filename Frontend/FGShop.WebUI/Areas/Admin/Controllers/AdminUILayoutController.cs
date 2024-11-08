﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/AdminUILayout")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class AdminUILayoutController : Controller
	{
		[Route("Index")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
