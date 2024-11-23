using FGShop.BussinessLayer.EntityFremawork.EfOrder;
using FGShop.CommanLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EFBasketsController : ControllerBase
	{
		private readonly IEFBasketService _service;

		public EFBasketsController(IEFBasketService service)
		{
			_service = service;
		}

		[HttpGet("GetByUserId/{userId}")]
		public async Task<IActionResult> GetByUserId(int userId)
		{
			var data = await _service.GetByUserId(userId);
			return Ok(data);
		}


		[HttpGet("{userId}")]
		public async Task<IActionResult> GetOrderByIds(int userId)
		{
			var response = await _service.GetProductDetailByUserId(userId);
			return Ok(response);
		}

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Remove(int userId)
        {
			await _service.UserIdBasketRemove(userId);

            return Ok();
        }

    }
}
