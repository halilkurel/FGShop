using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Models.StockModels
{
    public class UpdateStockModel
    {
        public UpdateStock? Data { get; set; }
        public ValidationError? ValidationErrors { get; set; }
        public string? Message { get; set; }
        public int? ResponseType { get; set; }


        public class UpdateStock
        {
            public int? Id { get; set; }
            public int? StockQuantity { get; set; }

        }
    }
}
