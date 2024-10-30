﻿using FGShop.WebUI.Models.ProducthasCategoryModels;
using FGShop.WebUI.Models.SliderModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FGShop.WebUI.ViewComponents
{
	public class _DefaultSliderComponentPartial: ViewComponent
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public _DefaultSliderComponentPartial(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7171/api/Sliders");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<ResultSliderModel>(jsonData);
				return View(values);
			}
			return View();
		}
	}
}
