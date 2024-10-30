using FGShop.BussinessLayer.EntityFremawork.EFProducthasCategory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFProducthasCategoriesController : ControllerBase
    {
        private readonly IEFProducthasCategoryService _EFProducthasCategoryService;

        public EFProducthasCategoriesController(IEFProducthasCategoryService eFProducthasCategoryService)
        {
            _EFProducthasCategoryService = eFProducthasCategoryService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByProductIdCategoryList(int id)
        {
            var response = await _EFProducthasCategoryService.GetByProductIdCategory(id);

            return Ok(response);
        }

        [HttpGet("GetByProductIdProducthasCategoryList/{id}")]
        public async Task<IActionResult> GetByProductIdProducthasCategoryList(int id)
        {
            var response = await _EFProducthasCategoryService.GetByProductIdProducthasCategoryList(id);

            return Ok(response);
        }

    }
}
