using FGShop.WebUI.Models.ContactModels;
using FGShop.WebUI.Models.ValdiationModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.WebUI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ContactController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult ContactFormPartial()
        {
            return PartialView(new CreateContactModel());
        }

        [HttpPost]
        public async Task<IActionResult> ContactFormPartial(CreateContactModel model)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(model);
            StringContent stringContent = new(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7171/api/Contacts", stringContent);

            if (responseMessage.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }
            else
            {
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var errors = JsonConvert.DeserializeObject<List<ValidationError>>(responseContent);
                
                var errorDict = errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
                
                return Json(errorDict);
            }
        }


    }
}
