using FGShop.WebUI.Models.CategoryModels;
using FGShop.WebUI.Models.SliderModels;
using FGShop.WebUI.Models.ValdiationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Slider")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class SliderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SliderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Sliders");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var sliders = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultSliderModel>(jsonString);
                return View(sliders);
            }
            else
            {
                return View(new ResultCategoryModel());
            }
        }

        [HttpGet]
        [Route("CreateSlider")]
        public IActionResult CreateSlider()
        {
            return View();
        }


        [HttpPost]
        [Route("CreateSlider")]
        public async Task<IActionResult> CreateSlider(CreateSliderModel model)
        {
            if (ModelState.IsValid)
            {
                // Dosya yükleme işlemi
                if (model.SliderImage != null && model.SliderImage.Length > 0)
                {
                    // Rastgele 8 karakterli bir string oluşturmak için
                    var randomFileName = Path.GetRandomFileName().Replace(".", "").Substring(0, 8);

                    // Dosya uzantısını al
                    var fileExtension = Path.GetExtension(model.SliderImage.FileName);

                    // Rastgele dosya adı ve orijinal dosya uzantısını birleştir
                    var fileName = randomFileName + fileExtension;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.SliderImage.CopyTo(stream);
                    }

                    // Dosya yolunu modele kaydetme
                    model.ImageUrl = "/images/" + fileName;
                }

                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:7171/api/Sliders", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Slider", new { area = "Admin" }) });
                }
                else
                {
                    var responseContent = await responseMessage.Content.ReadAsStringAsync();
                    var errors = JsonConvert.DeserializeObject<List<ValidationError>>(responseContent);

                    var errorDict = errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                    return Json(new { success = false, errors = errorDict });
                }


            }

            return View(model);
        }

        [HttpDelete("{id}")]
        [Route("Admin/Slider/DeleteSlider/{id}")]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            string url = $"https://localhost:7171/api/Sliders/{id}";

            // Öncelikle ürünün detaylarını alarak fotoğraf yolunu
            var sliderResponse = await httpClient.GetAsync(url);

            //Dosyayı Images Klasöründen silme işlemi
            if (sliderResponse.IsSuccessStatusCode)
            {
                var sliderJson = await sliderResponse.Content.ReadAsStringAsync();
                var sliderResultModel = JsonConvert.DeserializeObject<GetBySliderIdModel>(sliderJson);

                // Ürünün veritabanında kayıtlı olan yolunu alalım
                var slider = sliderResultModel.Data.ImageUrl;

                if (slider != null && !string.IsNullOrEmpty(slider))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", slider.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }
            }

            // Ürünü silme işlemi
            var response = await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Slider silinemedi" });
            }
        }


        [HttpGet]
        [Route("UpdateSlider")]
        public async Task<IActionResult> UpdateSlider(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Sliders/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var sliders = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateSliderModel>(jsonString);
                return View(sliders);
            }
            return View();
        }


        [HttpPut]
        [Route("UpdateSlider")]
        public async Task<IActionResult> UpdateSlider(UpdateSliderModel model)
        {
            var httpClient = _httpClientFactory.CreateClient();

            if (model.Data.SliderImage != null)
            {


                string url = $"https://localhost:7171/api/Sliders/{model.Data.Id}";

                // Öncelikle ürünün detaylarını alarak fotoğraf yolunu
                var sliderResponse = await httpClient.GetAsync(url);

                //Dosyayı Images Klasöründen silme işlemi
                if (sliderResponse.IsSuccessStatusCode)
                {
                    var sliderJson = await sliderResponse.Content.ReadAsStringAsync();
                    var sliderResultModel = JsonConvert.DeserializeObject<GetByCategoryIdModel>(sliderJson);

                    // Ürünün veritabanında kayıtlı olan yolunu alalım
                    var slider = sliderResultModel?.Data?.CoverPhoto;

                    if (slider != null && !string.IsNullOrEmpty(slider))
                    {
                        var filePath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", slider.TrimStart('/'));
                        if (System.IO.File.Exists(filePath1))
                        {
                            System.IO.File.Delete(filePath1);
                        }
                    }
                }

                // Rastgele 8 karakterli bir string oluşturmak için
                var randomFileName = Path.GetRandomFileName().Replace(".", "").Substring(0, 8);

                // Dosya uzantısını al
                var fileExtension = Path.GetExtension(model.Data.SliderImage.FileName);

                // Rastgele dosya adı ve orijinal dosya uzantısını birleştir
                var fileName = randomFileName + fileExtension;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Data.SliderImage.CopyTo(stream);
                }

                // Dosya yolunu modele kaydetme
                model.Data.ImageUrl = "/images/" + fileName;

            }
            else
            {
                var data = await httpClient.GetAsync("https://localhost:7171/api/Sliders/" + model.Data.Id);
                if (data.IsSuccessStatusCode)
                {
                    var jsonString = await data.Content.ReadAsStringAsync();
                    var slidersConvert = Newtonsoft.Json.JsonConvert.DeserializeObject<GetBySliderIdModel>(jsonString);
                    model.Data.ImageUrl = slidersConvert?.Data?.ImageUrl;
                }

            }

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model.Data);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"https://localhost:7171/api/Sliders/", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Slider", new { area = "Admin" }) });
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
