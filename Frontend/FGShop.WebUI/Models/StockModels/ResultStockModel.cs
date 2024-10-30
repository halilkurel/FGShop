using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Models.StockModels
{
    public class ResultStockModel
    {
        public List<ResultStock>? Data { get; set; }
        public ValidationError? ValidationErrors { get; set; }
        public string? Message { get; set; }
        public int? ResponseType { get; set; }


        public class ResultStock
        {
            public int? Id { get; set; }
            public int? StockQuantity { get; set; }

        }
    }
}
