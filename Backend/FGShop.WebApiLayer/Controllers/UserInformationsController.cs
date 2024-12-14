using FGShop.BussinessLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserInformationsController : ControllerBase
	{
		private readonly IUserInformationService _userInformationService;

		public UserInformationsController(IUserInformationService userInformationService)
		{
			_userInformationService = userInformationService;
		}

		[HttpGet("{userName}")]
		public async Task<IActionResult> GetUserByUsernameInformation(string userName)
		{
			var data = await _userInformationService.GetByUserName(userName);
			return Ok(data);
		}

		[HttpGet("GetByUserIdResultUserName/{id}")]
		public async Task<IActionResult> GetByUserIdResultUserName(int id)
		{
			var data = await _userInformationService.GetByUserId(id);
			return Ok(data);
		}
	}
}
