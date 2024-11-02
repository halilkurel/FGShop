using FGShop.WebUI.Models.ColorModels;
using FGShop.WebUI.Models.ValdiationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Color")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ColorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ColorController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Colors");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var sliders = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultColorModel>(jsonString);
                return View(sliders);
            }
            else
            {
                return View(new ResultColorModel());
            }
        }

        [HttpGet]
        [Route("CreateColor")]
        public IActionResult CreateColor()
        {
            return View();
        }


        [HttpPost]
        [Route("CreateColor")]
        public async Task<IActionResult> CreateColor(CreateColorModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7171/api/Colors", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Color", new { area = "Admin" }) });
            }
            else
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<List<ValidationError>>(responseContent);

                var errorDict = errors?.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return Json(new { success = false, errors = errorDict });
            }

        }



        [HttpDelete("{id}")]
        [Route("Admin/Color/DeleteColor/{id}")]
        public async Task<IActionResult> DeleteColor(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            string url = $"https://localhost:7171/api/Colors/{id}";

            // Öncelikle ürünün detaylarını alarak fotoğraf yolunu
            var colorResponse = await httpClient.GetAsync(url);

            //Dosyayı Images Klasöründen silme işlemi
            if (colorResponse.IsSuccessStatusCode)
            {
                var colorJson = await colorResponse.Content.ReadAsStringAsync();
                var colorResultModel = JsonConvert.DeserializeObject<GetByColorIdModel>(colorJson);
            }

            // Ürünü silme işlemi
            var response = await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Kategory silinemedi" });
            }
        }


        [HttpGet]
        [Route("UpdateColor")]
        public async Task<IActionResult> UpdateColor(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Colors/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var categories = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateColorModel>(jsonString);
                return View(categories);
            }
            return View();
        }


        [HttpPut]
        [Route("UpdateColor")]
        public async Task<IActionResult> UpdateColor(UpdateColorModel model)
        {
            var httpClient = _httpClientFactory.CreateClient();


            string url = $"https://localhost:7171/api/Colors/{model.Data.Id}";

            // Öncelikle ürünün detaylarını alarak fotoğraf yolunu
            var colorResponse = await httpClient.GetAsync(url);


            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model.Data);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"https://localhost:7171/api/Colors/", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Color", new { area = "Admin" }) });
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<List<ValidationError>>(responseContent);

                var errorDict = errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return Json(new { success = false, errors = errorDict });
            }

        }
    }
}
