﻿namespace FGShop.WebUI.Models.OrderModels
{
    public class UpdateOrderModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int OrderQuantity { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int StatusId { get; set; }
        public int UserAddressId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
