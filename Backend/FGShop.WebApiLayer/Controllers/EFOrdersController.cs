using FGShop.BussinessLayer.EntityFremawork.EFOrder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FGShop.WebApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EFOrdersController : ControllerBase
    {
        private readonly IEFOrderService _orderService;

        public EFOrdersController(IEFOrderService orderService)
        {
            _orderService = orderService;
        }

        // Tüm Order'u listele
        [HttpGet]
        public async Task<IActionResult> GetOrderList()
        {
            var data = await _orderService.OrderList();
            return Ok(data);
        }

        [HttpGet("Last4OrderList")]
        public async Task<IActionResult> Last4OrderList()
        {
            var data = await _orderService.Last4OrderList();
            return Ok(data);
        }

        //İptal edlien order'ları listele
        [HttpGet("ListCancelledOrders")]
        public async Task<IActionResult> ListCancelledOrders()
        {
            var data = await _orderService.ListCancelledOrders();
            return Ok(data);
        }

        //Onaylanmış Order'ları listele
        [HttpGet("ListApprovedOrders")]
        public async Task<IActionResult> ListApprovedOrders()
        {
            var data = await _orderService.ListApprovedOrders();
            return Ok(data);
        }

        //Sipariş Tamamlanmış order'ları listele
        [HttpGet("ListOrderCompleted")]
        public async Task<IActionResult> ListOrderCompleted()
        {
            var data = await _orderService.ListOrderCompleted();
            return Ok(data);
        }

        //Onaylanmamış, Onay bekleyen orderları listele
        [HttpGet("ListUnapprovedOrders")]
        public async Task<IActionResult> ListUnapprovedOrders()
        {
            var data = await _orderService.ListUnapprovedOrders();
            return Ok(data);
        }


        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetByOrderId(int orderId)
        {
            var data = await _orderService.GetByOrderId(orderId);
            return Ok(data);
        }

        [HttpGet("GetByUserNameOrders/{userName}")]
        public async Task<IActionResult> GetByUserNameOrders(string userName)
        {
            var data = await _orderService.GetByUserNameOrders(userName);
            return Ok(data);
        }
        
    }
}
