using FGShop.BussinessLayer.EntityFremawork.EFProducthasStock;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFProducthasStocksController : ControllerBase
    {
        private readonly IEFProducthasStockService _service;

        public EFProducthasStocksController(IEFProducthasStockService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByProductIdColorList(int id)
        {
            var response = await _service.GetByProductId(id);

            return Ok(response);
        }


        [HttpGet("GetByProductIdAndColorSizeStock/{id}")]
        public async Task<IActionResult> GetByProductIdAndColorSizeStock(int id)
        {
            var response = await _service.GetByProductIdAndColorSizeStock(id);

            return Ok(response);
        }


        
    }
}
