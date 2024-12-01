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

			// userId yi int değere çevir
			int userId = model.Products.Select(x => x.UserId).FirstOrDefault();

			//adres tablosnda userId nin verisi var mı kontrol et
            var addressResponse1 = await client.GetAsync($"https://localhost:7171/api/EFUserAddresses/GetByUserId/{userId}");
            

            //eğer yoksa
            if (addressResponse1.StatusCode != System.Net.HttpStatusCode.OK)
			{

                //Kullanıcının adres bilgileri address tablosuna kaydediliyor.
                var jsonData = JsonConvert.SerializeObject(model.Address);
                StringContent stringContent = new(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:7171/api/UserAddresses", stringContent);

				//Kaydetme işlemi başarılıysa
                if (responseMessage.IsSuccessStatusCode)
                {
					// Kaydettiğimiz user'a ait adresin id'si alınıyor
                    var addressResponse = await client.GetAsync($"https://localhost:7171/api/EFUserAddresses/GetByUserId/{userId}");
                    var addressJson = await addressResponse.Content.ReadAsStringAsync();

                    var UserAddresModel = JsonConvert.DeserializeObject<ResultUserAddressModel>(addressJson);
                    var addressId = UserAddresModel?.Id;

					// modelden gelen ürün bilgileri order api sine uygun bir şekilde foreach ile dönüyor ve post işlemi gerçekleşiyor.
					bool control2 = true;
                    foreach (var item in model.Products)
                    {
						
                        var order = new CreateOrderApiModel
                        {
							ProductId= item.ProductId,
                            ColorId = item.ColorId,
                            OrderQuantity = item.OrderQuantity,
                            SizeId = item.SizeId,
                            UserAddressId = Convert.ToInt32(addressId),
                            UserId = item.UserId,
                            StatusId = 4,
                            OrderDate = DateTime.Now,
                        };

                        var jsonData2 = JsonConvert.SerializeObject(order);
                        StringContent stringContent2 = new(jsonData2, Encoding.UTF8, "application/json");
                        var responseMessage2 = await client.PostAsync("https://localhost:7171/api/Orders", stringContent2);

                        if (responseMessage2.IsSuccessStatusCode)
                        {
                            control2 = true;
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
                var addressId1 = UserAddresModel1?.Id;

				//adres bilgileri için model oluşturuluyor
				var updateModel = new UpdateUserAddressModel
				{
					Id = addressId1.Value,
					Address = model.Address.Address,
					City = model.Address.City,
					Country = "Türkiye",
					District = model.Address.District,
					Email = model.Address.Email,
					Neighbourhood = model.Address.Neighbourhood,
					PhoneNumber = model.Address.PhoneNumber,
					UserId = model.Address.UserId

				};

				// adres tablosuna güncelleme işlemi gerçekleşiyor
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(updateModel);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"https://localhost:7171/api/UserAddresses/", content);

				// Güncelleme işlemi başarılıysa
                if (response.IsSuccessStatusCode)
                {
					// Order kayıt işlemi gerçekleşiyor
					bool control = true;
                    foreach (var item in model.Products)
                    {
                        var order = new CreateOrderApiModel
                        {
                            ProductId = item.ProductId,
                            ColorId = item.ColorId,
                            OrderQuantity = item.OrderQuantity,
                            SizeId = item.SizeId,
                            UserAddressId = Convert.ToInt32(addressId1.Value),
                            UserId = item.UserId,
                            StatusId = 4,
                            OrderDate = DateTime.Now,
                        };

                        var jsonData2 = JsonConvert.SerializeObject(order);
                        StringContent stringContent2 = new(jsonData2, Encoding.UTF8, "application/json");
                        var responseMessage2 = await client.PostAsync("https://localhost:7171/api/Orders", stringContent2);

                        if (responseMessage2.IsSuccessStatusCode)
                        {
                            control = true;
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
	}
}
