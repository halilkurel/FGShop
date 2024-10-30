using FGShop.BussinessLayer.EntityFremawork.EfProducthasColor;
using FGShop.BussinessLayer.EntityFremawork.EFProducthasSize;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFProducthasSizesController : ControllerBase
    {
        private readonly IEFProducthasSizeService _service;

        public EFProducthasSizesController(IEFProducthasSizeService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByProductIdColorList(int id)
        {
            var response = await _service.GetByProductIdSize(id);

            return Ok(response);
        }

        [HttpGet("GetByProductIdProducthasSizeList/{id}")]
        public async Task<IActionResult> GetByProductIdProducthasSizeList(int id)
        {
            var response = await _service.GetByProductIdSProducthasSizeList(id);

            return Ok(response);
        }

    }
}
