using FGShop.WebUI.Models.BasketModels;
using FGShop.WebUI.Models.StatusModels;
using FGShop.WebUI.Models.UserAddressModels;

namespace FGShop.WebUI.Models.OrderModels
{
    public class CreateOrderModel
    {
        public List<ProductOrderModel>? Products { get; set; } // Ürün bilgileri
        public AddressModel? Address { get; set; } // Adres bilgileri

    }
}
