using FGShop.WebUI.Areas.Admin.Models.ProdcutModels;
using FGShop.WebUI.Models.ImageModels;
using FGShop.WebUI.Models.ProductModels;
using FGShop.WebUI.Models.ValdiationModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Product")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/ProducthasCategories/GetAllProducthasCategory");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProductListModel>>(jsonString);
                return View(products);
            }
            else
            {
                return View(new List<ProductListModel>());
            }
        }


        [HttpDelete("{id}")]
        [Route("Admin/Product/DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            string url = $"https://localhost:7171/api/Products/{id}";

            // Öncelikle ürünün detaylarını alarak fotoğraf yolunu
            var productResponse = await httpClient.GetAsync(url);

            //Dosyayı Images Klasöründen silme işlemi
            if (productResponse.IsSuccessStatusCode)
            {
                var productJson = await productResponse.Content.ReadAsStringAsync();
                var productResultModel = JsonConvert.DeserializeObject<GetByProductIdModel>(productJson);

                // Ürünün veritabanında kayıtlı olan yolunu alalım
                var product = productResultModel.Data.CoverPhoto;
                

                if (product != null && !string.IsNullOrEmpty(product))
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.TrimStart('/'));
                    if (System.IO.File.Exists(filePath))
                    {
                        System.IO.File.Delete(filePath);
                    }
                }


                var imageResponse= await httpClient.GetAsync($"https://localhost:7171/api/EFProducthasImages/{id}");
                if (imageResponse.IsSuccessStatusCode)
                {
                    var imageJson = await imageResponse.Content.ReadAsStringAsync();
                    var imageResultModel = JsonConvert.DeserializeObject<List<ImageResult>>(imageJson);
                    foreach(var imageurl in imageResultModel)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageurl.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(filePath))
                        {
                            System.IO.File.Delete(filePath);
                        }
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
                return Json(new { success = false, message = "Ürün silinemedi" });
            }
        }



        [HttpGet]
        [Route("CreateProduct")]
        public IActionResult CreateProduct()
        {
            return View();
        }



        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct(CreateProductModel model)
        {
            if (ModelState.IsValid)
            {
                // Dosya yükleme işlemi
                if (model.ProductImage != null && model.ProductImage.Length > 0)
                {
                    // Rastgele 8 karakterli bir string oluşturmak için
                    var randomFileName = Path.GetRandomFileName().Replace(".", "").Substring(0, 8);

                    // Dosya uzantısını al
                    var fileExtension = Path.GetExtension(model.ProductImage.FileName);

                    // Rastgele dosya adı ve orijinal dosya uzantısını birleştir
                    var fileName = randomFileName + fileExtension;

                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.ProductImage.CopyTo(stream);
                    }

                    // Dosya yolunu modele kaydetme
                    model.CoverPhoto = "/images/" + fileName;
                }

                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(model);
                StringContent stringContent = new(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:7171/api/Products", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseMessage2 = await client.GetAsync("https://localhost:7171/api/Products");
                    var productJson = await responseMessage2.Content.ReadAsStringAsync();
                    var productResultModel = JsonConvert.DeserializeObject<ResultProductModel>(productJson);

                    // Yeni oluşturulan ürünün ID'sini al
                    var productId = productResultModel?.Data.OrderByDescending(p => p.Id).FirstOrDefault().Id;


                    return Json(new { success = true, redirectUrl = Url.Action("CreateProducthasImages", "ProducthasImage", new { area = "Admin", id = productId }) });
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



        [HttpGet]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Products/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var products = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateProductModel>(jsonString);
                return View(products);
            }
            return View();
        }


        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(UpdateProductModel model)
        {
            var httpClient = _httpClientFactory.CreateClient();

            if (model.Data.ProductImage != null)
            {


                string url = $"https://localhost:7171/api/Products/{model.Data.Id}";

                // Öncelikle ürünün detaylarını alarak fotoğraf yolunu
                var productResponse = await httpClient.GetAsync(url);

                //Dosyayı Images Klasöründen silme işlemi
                if (productResponse.IsSuccessStatusCode)
                {
                    var productJson = await productResponse.Content.ReadAsStringAsync();
                    var productResultModel = JsonConvert.DeserializeObject<GetByProductIdModel>(productJson);

                    // Ürünün veritabanında kayıtlı olan yolunu alalım
                    var product = productResultModel.Data.CoverPhoto;

                    if (product != null && !string.IsNullOrEmpty(product))
                    {
                        var filePath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.TrimStart('/'));
                        if (System.IO.File.Exists(filePath1))
                        {
                            System.IO.File.Delete(filePath1);
                        }
                    }
                }

                // Rastgele 8 karakterli bir string oluşturmak için
                var randomFileName = Path.GetRandomFileName().Replace(".", "").Substring(0, 8);

                // Dosya uzantısını al
                var fileExtension = Path.GetExtension(model.Data.ProductImage.FileName);

                // Rastgele dosya adı ve orijinal dosya uzantısını birleştir
                var fileName = randomFileName + fileExtension;

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Data.ProductImage.CopyTo(stream);
                }

                // Dosya yolunu modele kaydetme
                model.Data.CoverPhoto = "/images/" + fileName;

            }
            else
            {
                var data = await httpClient.GetAsync("https://localhost:7171/api/Products/" + model.Data.Id);
                if (data.IsSuccessStatusCode)
                {
                    var jsonString = await data.Content.ReadAsStringAsync();
                    var productsConvert = Newtonsoft.Json.JsonConvert.DeserializeObject<GetByProductIdModel>(jsonString);
                    model.Data.CoverPhoto = productsConvert?.Data.CoverPhoto;
                }

            }

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model.Data);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"https://localhost:7171/api/Products/", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "ProductDetail", new { area = "Admin", id=model.Data.Id }) });
            }
            else
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<List<ValidationError>>(responseContent);

                var errorDict = errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return Json(new { success = false, errors = errorDict });
            }

        }


        [HttpGet]
        [Route("UpdateProductDescription")]
        public async Task<IActionResult> UpdateProductDescription(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7171/api/Products/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var products = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateProductModel>(jsonString);
                return View(products);
            }
            return View();
        }


        [HttpPut]
        [Route("UpdateProductDescription")]
        public async Task<IActionResult> UpdateProductDescription(UpdateProductModel model)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var data = await httpClient.GetAsync("https://localhost:7171/api/Products/" + model.Data.Id);
            if (data.IsSuccessStatusCode)
            {
                var jsonString = await data.Content.ReadAsStringAsync();
                var productsConvert = Newtonsoft.Json.JsonConvert.DeserializeObject<GetByProductIdModel>(jsonString);
                model.Data.CoverPhoto = productsConvert?.Data.CoverPhoto;
            }

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(model.Data);

            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync($"https://localhost:7171/api/Products/", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true, redirectUrl = Url.Action("Index", "ProductDetail", new { area = "Admin", id = model.Data.Id }) });
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
