﻿using FGShop.WebUI.Models.EFProductsModel;
using FGShop.WebUI.Models.ImageModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace FGShop.WebUI.Controllers
{
	public class ProductPartialController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ProductPartialController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<PartialViewResult> Modal()
		{
			var client = _httpClientFactory.CreateClient();
			string url = $"https://localhost:7171/api/EFProducthasImages/{96}";
			var response = await client.GetAsync(url);
			var jsonProduct = await response.Content.ReadAsStringAsync();
			var data = JsonConvert.DeserializeObject<List<ImageResult>>(jsonProduct);
			ViewBag.Images = data;
			return PartialView();
		}


	}
}
