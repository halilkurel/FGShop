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
        public async Task<IActionResult> GetByProductIdStockList(int id)
        {
            var response = await _service.GetByProductIdStock(id);

            return Ok(response);
        }

        [HttpGet("GetByProductIdProducthasStockList/{id}")]
        public async Task<IActionResult> GetByProductIdProducthasStockList(int id)
        {
            var response = await _service.GetByProductIdProducthasStockList(id);

            return Ok(response);
        }


    }
}
