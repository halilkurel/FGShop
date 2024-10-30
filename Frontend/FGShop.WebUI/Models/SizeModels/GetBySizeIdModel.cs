using FGShop.WebUI.Models.ValdiationModels;

namespace FGShop.WebUI.Models.SizeModels
{
    public class GetBySizeIdModel
    {
        public GetByIdSize? Data { get; set; }
        public ValidationError? ValidationErrors { get; set; }
        public string? Message { get; set; }
        public int? ResponseType { get; set; }


        public class GetByIdSize
        {
            public int? Id { get; set; }
            public string? SizeName { get; set; }

        }
    }
}
