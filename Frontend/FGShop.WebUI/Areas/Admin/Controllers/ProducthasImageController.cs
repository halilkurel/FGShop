using FGShop.WebUI.Models.ImageModels;
using FGShop.WebUI.Models.ProducthasImageModels;
using FGShop.WebUI.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProducthasImage")]
    public class ProducthasImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProducthasImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{id}")]
        [Route("CreateProducthasImages")]
        public async Task<IActionResult> CreateProducthasImages(int id)
        {
            ViewBag.ProductId = id;
            return View();
        }


        [HttpPost]
        [Route("CreateProducthasImages")]
        public async Task<IActionResult> CreateProducthasImages(CreateProducthasImageParameterModel model)
        {
            using var client = new HttpClient();
            List<int> imageIds = new List<int>();

            foreach (var image in model.Images)
            {
                // Rastgele dosya adı oluştur
                var randomFileName = Path.GetRandomFileName().Replace(".", "").Substring(0, 8);
                var fileExtension = Path.GetExtension(image.FileName);
                var fileName = randomFileName + fileExtension;

                // Dosyayı wwwroot/images klasörüne kaydetme
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                await image.CopyToAsync(fileStream);

                // API'ye dosya adı ve URL'siyle gönderim yap
                var imageModel = new
                {
                    ImageUrl = "/images/" + fileName
                };

                var jsonContent = new StringContent(JsonConvert.SerializeObject(imageModel), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("https://localhost:7171/api/Images", jsonContent);

                if (response.IsSuccessStatusCode)
                {
                    var responseMessage2 = await client.GetAsync("https://localhost:7171/api/Images");
                    var imageJson = await responseMessage2.Content.ReadAsStringAsync();
                    var imageResultModel = JsonConvert.DeserializeObject<ResultImageModel>(imageJson);

                    // Yeni oluşturulan ürünün ID'sini al
                    var imageId = imageResultModel?.Data.OrderByDescending(p => p.Id)?.FirstOrDefault()?.Id;
                    imageIds.Add((int)imageId);
                    
                }
            }

            // ProducthasImage API'ye Product ve Image ilişkilendirmesi için POST işlemi yapalım
            foreach (var imageId in imageIds)
            {
                var productHasImageModel = new CreateProducthasImageModel
                {
                    ProductId = model.ProductId,
                    ImageId = imageId
                };

                var jsonContent = new StringContent(JsonConvert.SerializeObject(productHasImageModel), Encoding.UTF8, "application/json");
                var productHasImageResponse = await client.PostAsync("https://localhost:7171/api/ProducthasImages", jsonContent);

                if (!productHasImageResponse.IsSuccessStatusCode)
                {
                    ModelState.AddModelError("", "Ürün ve resim ilişkilendirilirken hata oluştu.");
                }
            }

            return Json(new { success = true, redirectUrl = Url.Action("CreateProducthasCategory", "ProducthasCategory", new { area = "Admin", id = model.ProductId }) });
        }


        [HttpGet("{id}")]
        [Route("UpdateProducthasImages")]
        public async Task<IActionResult> UpdateProducthasImages(int id)
        {
            ViewBag.ProductId = id;
            return View();

            
            
            
        }

        [HttpPost]
        [Route("UpdateProducthasImages")]
        public async Task<IActionResult> UpdateProducthasImages(UpdateImageModel model)
        {
            var ProductId = model.ProductId;
            using var client = new HttpClient();
            List<int> deleteImageIds = new List<int>();
            List<int> imageIds = new List<int>();


            if (ModelState.IsValid)
            {
                if(model.ImageFile != null)
                {

                    string url = $"https://localhost:7171/api/EFProducthasImages/{ProductId}";

                    // Öncelikle ürünün detaylarını alarak fotoğraf yolunu
                    var imageResponse = await client.GetAsync(url);

                    //Dosyayı Images Klasöründen silme işlemi
                    if (imageResponse.IsSuccessStatusCode)
                    {
                        var imageJson = await imageResponse.Content.ReadAsStringAsync();
                        var imageResultModel = JsonConvert.DeserializeObject<List<ImageResult>>(imageJson);

                        // Ürünün veritabanında kayıtlı olan yolunu alalım
                        foreach(var item in imageResultModel)
                        {
                            var image = item.ImageUrl;
                            if (image != null && !string.IsNullOrEmpty(image))
                            {
                                var filePath1 = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", image.TrimStart('/'));
                                if (System.IO.File.Exists(filePath1))
                                {
                                    System.IO.File.Delete(filePath1);
                                }
                            }
                            deleteImageIds.Add((int)item.Id);
                        }

                        foreach(var item in deleteImageIds)
                        {
                            string deleteUrl = $"https://localhost:7171/api/Images/{item}";
                            var response = await client.DeleteAsync(deleteUrl);
                        }

                        foreach (var image in model.ImageFile)
                        {
                            // Rastgele dosya adı oluştur
                            var randomFileName = Path.GetRandomFileName().Replace(".", "").Substring(0, 8);
                            var fileExtension = Path.GetExtension(image.FileName);
                            var fileName = randomFileName + fileExtension;

                            // Dosyayı wwwroot/images klasörüne kaydetme
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                            using var fileStream = new FileStream(filePath, FileMode.Create);
                            await image.CopyToAsync(fileStream);

                            // API'ye dosya adı ve URL'siyle gönderim yap
                            var imageModel = new
                            {
                                ImageUrl = "/images/" + fileName
                            };

                            var jsonContent = new StringContent(JsonConvert.SerializeObject(imageModel), Encoding.UTF8, "application/json");
                            var response = await client.PostAsync("https://localhost:7171/api/Images", jsonContent);

                            if (response.IsSuccessStatusCode)
                            {
                                var responseMessage2 = await client.GetAsync("https://localhost:7171/api/Images");
                                var image2Json = await responseMessage2.Content.ReadAsStringAsync();
                                var image2ResultModel = JsonConvert.DeserializeObject<ResultImageModel>(image2Json);

                                // Yeni oluşturulan ürünün ID'sini al
                                var imageId2 = image2ResultModel?.Data?.OrderByDescending(p => p.Id)?.FirstOrDefault()?.Id;
                                imageIds.Add((int)imageId2);

                            }
                        }

                        // ProducthasImage API'ye Product ve Image ilişkilendirmesi için POST işlemi yapalım
                        foreach (var imageId in imageIds)
                        {
                            var productHasImageModel = new CreateProducthasImageModel
                            {
                                ProductId = ProductId.Value,
                                ImageId = imageId
                            };

                            var jsonContent = new StringContent(JsonConvert.SerializeObject(productHasImageModel), Encoding.UTF8, "application/json");
                            var productHasImageResponse = await client.PostAsync("https://localhost:7171/api/ProducthasImages", jsonContent);

                            if (!productHasImageResponse.IsSuccessStatusCode)
                            {
                                ModelState.AddModelError("", "Ürün ve resim ilişkilendirilirken hata oluştu.");
                            }
                        }

                        return Json(new { success = true, redirectUrl = Url.Action("Index", "ProductDetail", new { area = "Admin", id = ProductId }) });
                    }
                }
            }
            return View();
        }

    }
}
