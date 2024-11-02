using FGShop.WebUI.Models.SizeModels;
using FGShop.WebUI.Models.ValdiationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Size")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class SizeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SizeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Sizes");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var sliders = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultSizeModel>(jsonString);
                return View(sliders);
            }
            else
            {
                return View(new ResultSizeModel());
            }
        }

        [HttpGet]
        [Route("CreateSize")]
        public IActionResult CreateSize()
        {
            return View();
        }


        [HttpPost]
        [Route("CreateSize")]
        public async Task<IActionResult> CreateSize(CreateSizeModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7171/api/Sizes", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Size", new { area = "Admin" }) });
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
        [Route("Admin/Size/DeleteSize/{id}")]
        public async Task<IActionResult> DeleteSize(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            string url = $"https://localhost:7171/api/Sizes/{id}";

            // Öncelikle ürünün detaylarını alarak fotoğraf yolunu
            var sizeResponse = await httpClient.GetAsync(url);

            //Dosyayı Images Klasöründen silme işlemi
            if (sizeResponse.IsSuccessStatusCode)
            {
                var sizeJson = await sizeResponse.Content.ReadAsStringAsync();
                var sizeResultModel = JsonConvert.DeserializeObject<GetBySizeIdModel>(sizeJson);
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
        [Route("UpdateSize")]
        public async Task<IActionResult> UpdateSize(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Sizes/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var categories = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateSizeModel>(jsonString);
                return View(categories);
            }
            return View();
        }


        [HttpPut]
        [Route("UpdateSize")]
        public async Task<IActionResult> UpdateSize(UpdateSizeModel model)
        {
            var httpClient = _httpClientFactory.CreateClient();


            string url = $"https://localhost:7171/api/Sizes/{model.Data.Id}";

            // Öncelikle ürünün detaylarını alarak fotoğraf yolunu
            var sizeResponse = await httpClient.GetAsync(url);


            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model.Data);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"https://localhost:7171/api/Sizes/", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Size", new { area = "Admin" }) });
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
