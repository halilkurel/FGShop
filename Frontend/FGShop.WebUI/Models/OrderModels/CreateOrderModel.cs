using FGShop.WebUI.Models.BasketModels;
using FGShop.WebUI.Models.StatusModels;
using FGShop.WebUI.Models.UserAddressModels;

namespace FGShop.WebUI.Models.OrderModels
{
    public class CreateOrderModel
    {




        //Adress bilgileri
        public int? UserId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Neighbourhood { get; set; }
        public string? Address { get; set; }
        public List<ProductModel> products { get; set; }





    }

    public class ProductModel
    {
        public int? BasketId { get; set; }
        
        public int? SizeId { get; set; }
        public int? ColorId { get; set; }
        public int? ProductId { get; set; }

        public string? ProductName { get; set; }
        public int? OrderQuantity { get; set; }
        public string? SizeName { get; set; }
        public string? ColorName { get; set; }

    }
}
