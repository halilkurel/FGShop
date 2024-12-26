using FGShop.BussinessLayer.EntityFremawork.EFLike;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFLikesController : ControllerBase
    {
        private readonly IEFLikeService _service;

        public EFLikesController(IEFLikeService service)
        {
            _service = service;
        }

        // User'a ail tüm like verilerini getir
        [HttpGet("GetByUserIdGetAllLikes/{userId}")]
        public async Task<IActionResult> GetByUserIdGetAllLikes(int userId)
        {
            var data = await _service.GetAll(userId);
            return Ok(data);
        }

		// User'a ail tüm like verilerini getir
		[HttpGet("CheckLikeStatusGetByProductIdandUserId/{productId}/{userId}")]
		public async Task<IActionResult> CheckLikeStatusGetByProductIdandUserId(int productId,int userId)
		{
			var data = await _service.CheckLikeStatusAsync(productId,userId);
			return Ok(data);
		}

        [HttpGet("GetByProductIdandUserId/{productId}/{userId}")]
        public async Task<IActionResult> GetByProductIdandUserId(int productId, int userId)
        {
            var data = await _service.GetByProductIdandUserId(productId,userId);
            return Ok(data);
        }

	}
}
