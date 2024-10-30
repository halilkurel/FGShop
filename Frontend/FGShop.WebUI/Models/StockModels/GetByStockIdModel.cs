using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Models.StockModels
{
    public class GetByStockIdModel
    {
        public GetByIdStock? Data { get; set; }
        public ValidationError? ValidationErrors { get; set; }
        public string? Message { get; set; }
        public int? ResponseType { get; set; }


        public class GetByIdStock
        {
            public int? Id { get; set; }
            public int? StockQuantity { get; set; }

        }
    }
}
