using FGShop.WebUI.Models.BasketModels;
using FGShop.WebUI.Models.CartModels;
using FGShop.WebUI.Models.ColorModels;
using FGShop.WebUI.Models.OrderModels;
using FGShop.WebUI.Models.UserAddressModels;
using FGShop.WebUI.Models.ValdiationModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace FGShop.WebUI.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CartController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity, int sizeId, int colorId)
        {
            var client = _httpClientFactory.CreateClient();

            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "LoginSignIn");
            }
            else
            {
                var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var userId = Convert.ToInt32(userIdClaim);

                // Mevcut kullanıcının sepetini API'den çek
                var response = await client.GetAsync($"https://localhost:7171/api/EFBaskets/GetByUserId/{userId}");
                var jsonString = await response.Content.ReadAsStringAsync();
                var basketList = JsonConvert.DeserializeObject<List<ResultBasketModel>>(jsonString);

                // Aynı üründen sepette var mı kontrol et
                var existingItem = basketList?.FirstOrDefault(b =>
                    b.ProductId == productId &&
                    b.ColorId == colorId &&
                    b.SizeId == sizeId);

                if (existingItem != null)
                {
                    // Eğer ürün varsa miktarı artır
                    existingItem.OrderQuantity += quantity;
                    existingItem.Id = basketList.Where(x => x.UserId == userId && x.ProductId == productId).Select(c => c.Id).FirstOrDefault();

                    var updatedJsonData = JsonConvert.SerializeObject(existingItem);
                    StringContent updateContent = new(updatedJsonData, Encoding.UTF8, "application/json");

                    // Sepeti güncellemek için PUT isteği gönder
                    var updateResponse = await client.PutAsync($"https://localhost:7171/api/Baskets/", updateContent);

                    if (updateResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "ProductDetail", new { id = productId });
                    }
                    else
                    {
                        // Güncelleme başarısızsa hata sayfası gösterebilirsiniz
                        return View("Error");
                    }
                }
                else
                {
                    // Ürün sepette yoksa, yeni bir sepet kaydı oluştur
                    CreateBasketModel newBasketItem = new()
                    {
                        ColorId = colorId,
                        OrderQuantity = quantity,
                        SizeId = sizeId,
                        ProductId = productId,
                        UserId = userId
                    };

                    var newJsonData = JsonConvert.SerializeObject(newBasketItem);
                    StringContent createContent = new(newJsonData, Encoding.UTF8, "application/json");

                    // Yeni ürünü sepete ekle
                    var createResponse = await client.PostAsync("https://localhost:7171/api/Baskets", createContent);

                    if (createResponse.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index", "ProductDetail", new { id = productId });
                    }
                    else
                    {
                        // Ekleme başarısızsa hata sayfası gösterebilirsiniz
                        return View("Error");
                    }
                }
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveBasket(int id)
        {

            var httpClient = _httpClientFactory.CreateClient();
            string url = $"https://localhost:7171/api/Baskets/{id}";

            // Ürünü silme işlemi
            var response = await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                return Json(new { success = false, message = errorMessage });
            }


        }

        [HttpPut]
        public async Task<IActionResult> UpdateBasket([FromBody] List<UpdateCartModel> model)
        {
            var httpClient = _httpClientFactory.CreateClient();
            bool control = true;

            foreach (var item in model)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(item);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"https://localhost:7171/api/Baskets/", content);
                if (response.IsSuccessStatusCode)
                {
                    control = true;
                }
                else control = false;


            }
            if (control)
            {
                return Json(new { success = true });
            }
            else return Json(new { success = false });

        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderModel model)
        {
            // client oluştur
            var client = _httpClientFactory.CreateClient();

            //UserId' yi alıyorum
            var userId = model.UserId;

            //adres tablosnda userId nin verisi var mı kontrol et
            var addressResponse1 = await client.GetAsync($"https://localhost:7171/api/EFUserAddresses/GetByUserId/{userId}");


            //eğer yoksa
            if (addressResponse1.StatusCode != System.Net.HttpStatusCode.OK)
            {


                var userAdressModel = new CreateUserAddressModel
                {
                    UserId = model.UserId,
                    Country = model.Country,
                    City = model.City,
                    Address = model.Address,
                    District = model.District,
                    Email = model.Email,
                    Neighbourhood = model.Neighbourhood,
                    PhoneNumber = model.PhoneNumber
                };


                //Kullanıcının adres bilgileri address tablosuna kaydediliyor.
                var jsonData = JsonConvert.SerializeObject(userAdressModel);
                StringContent stringContent = new(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:7171/api/UserAddresses", stringContent);

                //Kaydetme işlemi başarılıysa
                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseUserName = await client.GetAsync($"https://localhost:7171/api/UserInformations/GetByUserIdResultUserName/{userId}");
                    var userName = await responseUserName.Content.ReadAsStringAsync();


                    bool control2 = true;
                    foreach (var item in model.products)
                    {

                        var order = new CreateOrderApiModel
                        {
                            UserName = userName,
                            ColorName = item.ColorName,
                            SizeId= Convert.ToInt32(item.SizeId),
                            ColorId= Convert.ToInt32(item.ColorId),
                            SizeName = item.SizeName,
                            OrderQuantity = item.OrderQuantity,
                            ProductName = item.ProductName,
                            ProductId = Convert.ToInt32(item.ProductId),
                            StatusId = 4,
                            OrderDate = DateTime.Now,
                            PhoneNumber = userAdressModel.PhoneNumber,
                            Neighbourhood = userAdressModel.Neighbourhood,
                            Email = userAdressModel.Email,
                            District = userAdressModel.District,
                            Address = userAdressModel.Address,
                            City = userAdressModel.City,
                            Country = userAdressModel.Country,

                        };

                        var jsonData2 = JsonConvert.SerializeObject(order);
                        StringContent stringContent2 = new(jsonData2, Encoding.UTF8, "application/json");
                        var responseMessage2 = await client.PostAsync("https://localhost:7171/api/Orders", stringContent2);

                        if (responseMessage2.IsSuccessStatusCode)
                        {
                            control2 = true;
                        }
                        else
                        {
                            control2 = false;
                        }
                    }
                    if (control2)
                    {
                        // Siparişi verilen sepeti sil
                        var basketResponse = await client.DeleteAsync($"https://localhost:7171/api/EFBaskets/{userId}");

                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }

                }
                else
                {
                    return Json(new { success = false });
                }

            }
            // Eğer kullanıcının daha önceden kayıtlı bir adresi varsa;
            else
            {
                // adres id si alıyor
                var addressJson1 = await addressResponse1.Content.ReadAsStringAsync();

                var UserAddresModel1 = JsonConvert.DeserializeObject<ResultUserAddressModel>(addressJson1);
                var addressId1 = UserAddresModel1.Id;

                //adres bilgileri için model oluşturuluyor
                var updateModel = new UpdateUserAddressModel
                {
                    Id= addressId1,
                    UserId = model.UserId,
                    Country = model.Country,
                    City = model.City,
                    Address = model.Address,
                    District = model.District,
                    Email = model.Email,
                    Neighbourhood = model.Neighbourhood,
                    PhoneNumber = model.PhoneNumber,

                };

                // adres tablosuna güncelleme işlemi gerçekleşiyor
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(updateModel);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"https://localhost:7171/api/UserAddresses/", content);

                // Güncelleme işlemi başarılıysa
                if (response.IsSuccessStatusCode)
                {

                    var responseUserName = await client.GetAsync($"https://localhost:7171/api/UserInformations/GetByUserIdResultUserName/{userId}");
                    var userName = await responseUserName.Content.ReadAsStringAsync();


                    // Order kayıt işlemi gerçekleşiyor
                    bool control = true;
                    foreach (var item in model.products)
                    {


                        var order = new CreateOrderApiModel
                        {
                            UserName = userName,
                            ColorName = item.ColorName,
                            SizeName = item.SizeName,
                            SizeId = Convert.ToInt32(item.SizeId),
                            ColorId = Convert.ToInt32(item.ColorId),
                            OrderQuantity = item.OrderQuantity,
                            ProductName = item.ProductName,
                            ProductId = Convert.ToInt32(item.ProductId),
                            StatusId = 4,
                            OrderDate = DateTime.Now,
                            PhoneNumber = updateModel.PhoneNumber,
                            Neighbourhood = updateModel.Neighbourhood,
                            Email = updateModel.Email,
                            District = updateModel.District,
                            Address = updateModel.Address,
                            City = updateModel.City,
                            Country = updateModel.Country,

                        };

                        var jsonData2 = JsonConvert.SerializeObject(order);
                        StringContent stringContent2 = new(jsonData2, Encoding.UTF8, "application/json");
                        var responseMessage2 = await client.PostAsync("https://localhost:7171/api/Orders", stringContent2);

                        if (responseMessage2.IsSuccessStatusCode)
                        {
                            control = true;
                        }
                        else
                        {
                            control = false;
                        }
                    }

                    if (control)
                    {
                        // Siparişi verilen sepeti sil
                        var basketResponse = await client.DeleteAsync($"https://localhost:7171/api/EFBaskets/{userId}");

                        return Json(new { success = true });
                    }
                    else
                    {
                        return Json(new { success = false });
                    }

                }
                else
                {
                    return Json(new { success = false });
                }



            }

        }


		[HttpDelete("RemoveLike/{id}")]
		public async Task<IActionResult> RemoveLike(int id)
		{

			var httpClient = _httpClientFactory.CreateClient();
			string url = $"https://localhost:7171/api/Likes/{id}";

			// Ürünü silme işlemi
			var response = await httpClient.DeleteAsync(url);

			if (response.IsSuccessStatusCode)
			{
				return Json(new { success = true });
			}
			else
			{
				var errorMessage = await response.Content.ReadAsStringAsync();
				return Json(new { success = false, message = errorMessage });
			}


		}

	}
}

