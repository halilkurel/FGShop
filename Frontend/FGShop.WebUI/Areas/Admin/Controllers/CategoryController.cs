using FGShop.WebUI.Areas.Admin.Models.ProdcutModels;
using FGShop.WebUI.Models.CategoryModels;
using FGShop.WebUI.Models.ProductModels;
using FGShop.WebUI.Models.ValdiationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Category")]

    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Categories");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var sliders = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultCategoryModel>(jsonString);
                return View(sliders);
            }
            else
            {
                return View(new ResultCategoryModel());
            }
        }

        [HttpGet]
        [Route("CreateCategory")]
        public IActionResult CreateCategory()
        {
            return View();
        }


        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> CreateCategory(CreateCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                // Dosya yükleme işlemi
                if (model.CategoryImage != null && model.CategoryImage.Length > 0)
                {
                    // Rastgele 8 karakterli bir string oluşturmak için
                    var randomFileName = Path.GetRandomFileName().Replace(".", "").Substring(0, 8);

                    // Dosya uzantısını al
                    var fileExtension = Path.GetExtension(model.CategoryImage.FileName);

                    // Rastgele dosya adı ve orijinal dosya uzantısını birleştir
                    var fileName = randomFileName + fileExtension;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.CategoryImage.CopyTo(stream);
                    }

                    // Dosya yolunu modele kaydetme
                    model.CoverPhoto = "/images/" + fileName;
                }

                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:7171/api/Categories", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return Json(new { success = true, redirectUrl = Url.Action("Index", "Category", new { area = "Admin" }) });
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
        [Route("Admin/Category/DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            string url = $"https://localhost:7171/api/Categories/{id}";

            // Öncelikle ürünün detaylarını alarak fotoğraf yolunu
            var categoryResponse = await httpClient.GetAsync(url);

            //Dosyayı Images Klasöründen silme işlemi
            if (categoryResponse.IsSuccessStatusCode)
            {
                var categoryJson = await categoryResponse.Content.ReadAsStringAsync();
                var categoryResultModel = JsonConvert.DeserializeObject<GetByCategoryIdModel>(categoryJson);

                // Ürünün veritabanında kayıtlı olan yolunu alalım
                var category = categoryResultModel.Data.CoverPhoto;

                if (category != null && !string.IsNullOrEmpty(category))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", category.TrimStart('/'));
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
                return Json(new { success = false, message = "Kategory silinemedi" });
            }
        }


        [HttpGet]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Categories/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var categories = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateCategoryModel>(jsonString);
                return View(categories);
            }
            return View();
        }


        [HttpPut]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryModel model)
        {
            var httpClient = _httpClientFactory.CreateClient();

            if (model.Data.CategoryImage != null)
            {


                string url = $"https://localhost:7171/api/Categories/{model.Data.Id}";

                // Öncelikle ürünün detaylarını alarak fotoğraf yolunu
                var categoryResponse = await httpClient.GetAsync(url);

                //Dosyayı Images Klasöründen silme işlemi
                if (categoryResponse.IsSuccessStatusCode)
                {
                    var categoryJson = await categoryResponse.Content.ReadAsStringAsync();
                    var categoryResultModel = JsonConvert.DeserializeObject<GetByCategoryIdModel>(categoryJson);

                    // Ürünün veritabanında kayıtlı olan yolunu alalım
                    var category = categoryResultModel?.Data?.CoverPhoto;

                    if (category != null && !string.IsNullOrEmpty(category))
                    {
                        var filePath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", category.TrimStart('/'));
                        if (System.IO.File.Exists(filePath1))
                        {
                            System.IO.File.Delete(filePath1);
                        }
                    }
                }

                // Rastgele 8 karakterli bir string oluşturmak için
                var randomFileName = Path.GetRandomFileName().Replace(".", "").Substring(0, 8);

                // Dosya uzantısını al
                var fileExtension = Path.GetExtension(model.Data.CategoryImage.FileName);

                // Rastgele dosya adı ve orijinal dosya uzantısını birleştir
                var fileName = randomFileName + fileExtension;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Data.CategoryImage.CopyTo(stream);
                }

                // Dosya yolunu modele kaydetme
                model.Data.CoverPhoto = "/images/" + fileName;

            }
            else
            {
                var data = await httpClient.GetAsync("https://localhost:7171/api/Categories/" + model.Data.Id);
                if (data.IsSuccessStatusCode)
                {
                    var jsonString = await data.Content.ReadAsStringAsync();
                    var categorysConvert = Newtonsoft.Json.JsonConvert.DeserializeObject<GetByCategoryIdModel>(jsonString);
                    model.Data.CoverPhoto = categorysConvert?.Data?.CoverPhoto;
                }

            }

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model.Data);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"https://localhost:7171/api/Categories/", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "Category", new { area = "Admin" }) });
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
