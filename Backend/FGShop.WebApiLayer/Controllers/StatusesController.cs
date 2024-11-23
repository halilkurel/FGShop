using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DtoLayer.StatusDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusesController : ControllerBase
    {
        private readonly IStatusService _service;

        public StatusesController(IStatusService statusService)
        {
            _service = statusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllStatus()
        {
            var data = await _service.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetById<ResultStatusDto>(id);

            if (response == null)
            {
                return NotFound(); // Eğer ürün bulunamazsa 404 Not Found döndürülebilir.
            }

            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            await _service.Remove(id);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateStatusDto dto)
        {
            var response = await _service.Update(dto);


            if (response == null)
            {
                return NotFound(); // Eğer güncellenmeye çalışılan ürün bulunamazsa 404 Not Found döndürülebilir.
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateStatusDto dto)
        {
            var response = await _service.Create(dto);

            return Ok(response);
        }
    }
}
