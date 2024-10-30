using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DtoLayer.AboutDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	
	public class AboutsController : ControllerBase
	{
		private readonly IAboutService _service;

		public AboutsController(IAboutService categoryService)
		{
			_service = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllAbout()
		{
			var data = await _service.GetAll();
			return Ok(data);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var response = await _service.GetById<ResultAboutDto>(id);

			if (response.ResponseType == ResponseType.NotFound)
			{
				return NotFound(response.Message); // Eğer ürün bulunamazsa 404 Not Found döndürülebilir.
			}

			return Ok(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Remove(int id)
		{
			var response = await _service.Remove(id);

			if (response.ResponseType == ResponseType.NotFound)
			{
				return NotFound(response.Message); // Eğer silinmeye çalışılan ürün bulunamazsa 404 Not Found döndürülebilir.
			}

			return Ok(response);
		}

		[HttpPut]
		public async Task<IActionResult> Update([FromBody] UpdateAboutDto dto)
		{
			var response = await _service.Update(dto);

			if (response.ResponseType == ResponseType.ValidationError)
			{
				return BadRequest(response.ValidationErrors); // Validation hatalarını döndürmek için.
			}

			if (response.ResponseType == ResponseType.NotFound)
			{
				return NotFound(response.Message); // Eğer güncellenmeye çalışılan ürün bulunamazsa 404 Not Found döndürülebilir.
			}

			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateAsync([FromBody] CreateAboutDto dto)
		{
			var response = await _service.Create(dto);

			if (response.ResponseType == ResponseType.ValidationError)
			{
				return BadRequest(response.ValidationErrors); // Validation hatalarını döndürmek için.
			}

			return Ok(response);
		}
	}
}
