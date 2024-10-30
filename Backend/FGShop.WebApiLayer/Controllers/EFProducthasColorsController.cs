using FGShop.BussinessLayer.EntityFremawork.EfProducthasColor;
using FGShop.BussinessLayer.EntityFremawork.EFProducthasImage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFProducthasColorsController : ControllerBase
    {
        private readonly IEfProducthasColorService _efProducthasColorService;

        public EFProducthasColorsController(IEfProducthasColorService efProducthasColorService)
        {
            _efProducthasColorService = efProducthasColorService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByProductIdColorList(int id)
        {
            var response = await _efProducthasColorService.GetByProductIdColor(id);

            return Ok(response);
        }

        [HttpGet("GetByProductIdProducthasColorList/{id}")]
        public async Task<IActionResult> GetByProductIdProducthasColorList(int id)
        {
            var response = await _efProducthasColorService.GetByProductIdProducthasColorList(id);

            return Ok(response);
        }


    }
}
