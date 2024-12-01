using FGShop.WebUI.Models.CartModels;
using FGShop.WebUI.Models.ColorModels;
using FGShop.WebUI.Models.EFOrderModels;
using FGShop.WebUI.Models.OrderModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FGShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/Order")]
    [Authorize(Policy = "RequireAdministratorRole")]
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public OrderController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("Index")]
        public async  Task<IActionResult> Index()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7171/api/EFOrders");

            var jsonString = await response.Content.ReadAsStringAsync();
            var orders = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultEFOrderModel>>(jsonString);
            return View(orders);
        }

        [Route("OrderDetailPage/{orderId}")]
        public async Task<IActionResult> OrderDetailPage(int orderId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7171/api/EFOrders/{orderId}");

            var jsonString = await response.Content.ReadAsStringAsync();
            var order = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultEFOrderModel>(jsonString);
            return View(order);

        }
        
        //iptal edlien orderlar
        [Route("CancelledOrders")]
        public async Task<IActionResult> CancelledOrders()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7171/api/EFOrders/ListCancelledOrders");

            var jsonString = await response.Content.ReadAsStringAsync();
            var orders = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultEFOrderModel>>(jsonString);
            return View(orders);
        }


        //Tamamlanan  orderlar
        [Route("OrderComleted")]
        public async Task<IActionResult> OrderComleted()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7171/api/EFOrders/ListOrderCompleted");

            var jsonString = await response.Content.ReadAsStringAsync();
            var orders = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultEFOrderModel>>(jsonString);
            return View(orders);
        }



        //Onaylanmamış orderlar
        [Route("Unapproved")]
        public async Task<IActionResult> Unapproved()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7171/api/EFOrders/ListUnapprovedOrders");

            var jsonString = await response.Content.ReadAsStringAsync();
            var orders = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultEFOrderModel>>(jsonString);
            return View(orders);
        }

        //Onaylanmış orderlar
        [Route("Approved")]
        public async Task<IActionResult> Approved()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7171/api/EFOrders/ListApprovedOrders");

            var jsonString = await response.Content.ReadAsStringAsync();
            var orders = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ResultEFOrderModel>>(jsonString);
            return View(orders);
        }

        //Siparişi onayla butonu
        [Route("StatusConfirmed")]
        public async Task<IActionResult> StatusConfirmed(int orderId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7171/api/Orders/StatusConfirmed/{orderId}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(
                    actionName: "OrderDetailPage",
                    controllerName: "Order",
                    routeValues: new { area = "Admin", orderId = orderId }
                );
            }
            else
            {
                return View(); // Hata durumunda bir hata sayfasına yönlendirebilirsin.
            }
        }


        //Siparişi iptal et butonu
        [Route("CancelOrder")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var client = _httpClientFactory.CreateClient();
            var response =  await client.GetAsync($"https://localhost:7171/api/Orders/CancelOrder/{orderId}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(
                    actionName: "OrderDetailPage",
                    controllerName: "Order",
                    routeValues: new { area = "Admin", orderId = orderId }
                );
            }
            else
            {
                return View(); // Hata durumunda bir hata sayfasına yönlendirebilirsin.
            }
        }

        //Siparişi tamam et butonu
        [Route("CompletetheOrder")]
        public async Task<IActionResult> CompletetheOrder(int orderId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"https://localhost:7171/api/Orders/CompletetheOrder/{orderId}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(
                    actionName: "OrderDetailPage",
                    controllerName: "Order",
                    routeValues: new { area = "Admin", orderId = orderId }
                );
            }
            else
            {
                return View(); // Hata durumunda bir hata sayfasına yönlendirebilirsin.
            }
        }

        [HttpDelete("{id}")]
        [Route("Admin/Color/DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            string url = $"https://localhost:7171/api/Orders/{id}";

            

            // Ürünü silme işlemi
            var response = await httpClient.DeleteAsync(url);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Sipariş silinemedi" });
            }
        }
    }
}
