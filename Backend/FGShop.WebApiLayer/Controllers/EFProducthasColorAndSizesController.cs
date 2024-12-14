using FGShop.BussinessLayer.EntityFremawork.EFProducthasCategory;
using FGShop.BussinessLayer.EntityFremawork.EFProducthasColorAndSize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFProducthasColorAndSizesController : ControllerBase
    {
        private readonly IEFProducthasColorAndSizeService _service;

        public EFProducthasColorAndSizesController(IEFProducthasColorAndSizeService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByProductIdList(int id)
        {
            var response = await _service.GetByProductId(id);

            return Ok(response);
        }

        [HttpGet("LastAddedDataId/{id}")]
        public async Task<IActionResult> LastAddedDataId(int id)
        {
            var response = await _service.LastAddedDataId(id);

            return Ok(response);
        }
    }
}
