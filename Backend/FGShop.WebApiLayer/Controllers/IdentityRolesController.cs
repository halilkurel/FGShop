using FGShop.BussinessLayer.Interfaces;
using FGShop.BussinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityRolesController : ControllerBase
    {
        private readonly IIdentityRoleService _roleService;

        public IdentityRolesController(IIdentityRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(string roleName)
        {
            var result = await _roleService.CreateRoleAsync(roleName);
            if (result.Succeeded)
            {
                return Ok("Role added successfully.");
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("GetAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }


       
    }
}
