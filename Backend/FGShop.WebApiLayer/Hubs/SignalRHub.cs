using FGShop.BussinessLayer.EntityFremawork.EFLike;
using FGShop.BussinessLayer.EntityFremawork.EfOrder;
using FGShop.BussinessLayer.EntityFremawork.EFOrder;
using Microsoft.AspNetCore.SignalR;

namespace FGShop.WebApiLayer.Hubs
{
    public class SignalRHub : Hub
    {
        private readonly IEFBasketService _ebasketService;
        private readonly IEFLikeService _efLikeService;
        private readonly IEFOrderService _efOrderService;

        public SignalRHub(IEFBasketService ebasketService, IEFLikeService efLikeService, IEFOrderService efOrderService)
        {
            _ebasketService = ebasketService;
            _efLikeService = efLikeService;
            _efOrderService = efOrderService;
        }

        public async Task SendBasket(int userId)
        {
            var basketCount = await _ebasketService.GetByUserIdBasketQuantity(userId);
            await Clients.All.SendAsync("ReceiveBasketCount", basketCount);
        }

        public async Task SendLike(int userId)
        {
            var likeCount = await _efLikeService.GetByUserIdLikeQuantity(userId);
            await Clients.All.SendAsync("ReceiveLikeCount", likeCount);
        }

        public async Task SendDashboard()
        {
            var last4OrderList = await _efOrderService.Last4OrderList();
            await Clients.All.SendAsync("Receivelast4OrderList", last4OrderList);
        }


    }
}
