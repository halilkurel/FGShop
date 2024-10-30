using FGShop.BussinessLayer.EntityFremawork.EFProduct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class EFProductsController : ControllerBase
	{
		private readonly IEFProductService _productService;

		public EFProductsController(IEFProductService productService)
		{
			_productService = productService;
		}

		[HttpGet("GetByProductIdProductAllResult/{id}")]
		public async Task<IActionResult> GetByProductIdProductAllResult(int id)
		{
			var response = await _productService.GetByProductIdResultAll(id);

			return Ok(response);
		}
	}
}
