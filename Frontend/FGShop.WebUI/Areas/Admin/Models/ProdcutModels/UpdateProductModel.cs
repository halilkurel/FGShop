using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Areas.Admin.Models.ProdcutModels
{

    public class UpdateProductModel
    {
        public GetByProductResult? Data { get; set; }
        public ValidationError ValidationErrors { get; set; }
        public string Message { get; set; }
        public int ResponseType { get; set; }
    }
    public class GetByProductResult
    {
        public int? Id { get; set; }
        public string? ProductName { get; set; }
        public string? CoverPhoto { get; set; }
        public IFormFile? ProductImage { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
        public string? Description2 { get; set; }


    }
}
