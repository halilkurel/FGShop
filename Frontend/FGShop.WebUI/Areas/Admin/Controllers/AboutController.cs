using FGShop.WebUI.Models.AboutModels;
using FGShop.WebUI.Models.CategoryModels;
using FGShop.WebUI.Models.ValdiationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/About")]
    public class AboutController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AboutController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Abouts");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var sliders = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultAboutModel>(jsonString);
                return View(sliders);
            }
            else
            {
                return View(new ResultAboutModel());
            }
        }


        [HttpGet]
        [Route("UpdateAbout")]
        public async Task<IActionResult> UpdateAbout(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Abouts/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var abouts = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateAboutModel>(jsonString);
                return View(abouts);
            }
            return View();
        }


        [HttpPut]
        [Route("UpdateAbout")]
        public async Task<IActionResult> UpdateAbout(UpdateAboutModel model)
        {
            var httpClient = _httpClientFactory.CreateClient();

            if (model.Data.AboutImage != null)
            {


                string url = $"https://localhost:7171/api/Abouts/{model.Data.Id}";

                // Öncelikle ürünün detaylarını alarak fotoğraf yolunu
                var aboutResponse = await httpClient.GetAsync(url);

                //Dosyayı Images Klasöründen silme işlemi
                if (aboutResponse.IsSuccessStatusCode)
                {
                    var aboutJson = await aboutResponse.Content.ReadAsStringAsync();
                    var aboutResultModel = JsonConvert.DeserializeObject<GetByAboutIdModel>(aboutJson);

                    // Ürünün veritabanında kayıtlı olan yolunu alalım
                    var about = aboutResultModel?.Data?.ImageUrl;

                    if (about != null && !string.IsNullOrEmpty(about))
                    {
                        var filePath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", about.TrimStart('/'));
                        if (System.IO.File.Exists(filePath1))
                        {
                            System.IO.File.Delete(filePath1);
                        }
                    }
                }

                // Rastgele 8 karakterli bir string oluşturmak için
                var randomFileName = Path.GetRandomFileName().Replace(".", "").Substring(0, 8);

                // Dosya uzantısını al
                var fileExtension = Path.GetExtension(model.Data.AboutImage.FileName);

                // Rastgele dosya adı ve orijinal dosya uzantısını birleştir
                var fileName = randomFileName + fileExtension;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Data.AboutImage.CopyTo(stream);
                }

                // Dosya yolunu modele kaydetme
                model.Data.ImageUrl = "/images/" + fileName;

            }
            else
            {
                var data = await httpClient.GetAsync("https://localhost:7171/api/Abouts/" + model.Data.Id);
                if (data.IsSuccessStatusCode)
                {
                    var jsonString = await data.Content.ReadAsStringAsync();
                    var aboutsConvert = Newtonsoft.Json.JsonConvert.DeserializeObject<GetByAboutIdModel>(jsonString);
                    model.Data.ImageUrl = aboutsConvert?.Data?.ImageUrl;
                }

            }

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model.Data);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"https://localhost:7171/api/Abouts/", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "About", new { area = "Admin" }) });
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
