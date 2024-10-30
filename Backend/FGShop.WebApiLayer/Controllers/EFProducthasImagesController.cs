using FGShop.BussinessLayer.EntityFremawork.EFProducthasImage;
using FGShop.CommanLayer;
using FGShop.DtoLayer.AboutDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFProducthasImagesController : ControllerBase
    {
        private readonly IEFProducthasImageService _producthasImageService;

        public EFProducthasImagesController(IEFProducthasImageService producthasImageService)
        {
            _producthasImageService = producthasImageService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByProductIdImageList(int id)
        {
            var response = await _producthasImageService.GetByProductIdImage(id);

            return Ok(response);
        }
    }
}
