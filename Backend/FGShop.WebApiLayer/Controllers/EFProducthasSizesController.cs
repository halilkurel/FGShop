using FGShop.BussinessLayer.EntityFremawork.EFProducthasImage;
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

        public EFProducthasSizesController(IEFProducthasSizeService producthasSizeService)
        {
            _service = producthasSizeService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByProductIdList(int id)
        {
            var response = await _service.GetByProductId(id);

            return Ok(response);
        }


    }
}
