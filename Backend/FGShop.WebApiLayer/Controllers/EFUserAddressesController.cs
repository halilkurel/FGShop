using FGShop.BussinessLayer.EntityFremawork.EfOrder;
using FGShop.BussinessLayer.EntityFremawork.EFUserAddress;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFUserAddressesController : ControllerBase
    {
        private readonly IEFUserAddressService _userAddressService;

        public EFUserAddressesController(IEFUserAddressService userAddressService)
        {
            _userAddressService = userAddressService;
        }

        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var data = await _userAddressService.GetByUserId(userId);
            return Ok(data);
        }
    }
}
