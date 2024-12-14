namespace FGShop.WebUI.Models.ProducthasSizeModels
{
    public class ResultProducthasSizeModel
    {
        public int Id { get; set; }
        public int ProducthasColorId { get; set; }
        public int SizeId { get; set; }
        public string? SizeName { get; set; }
        public int ColorId { get; set; }
        public string? ColorName { get; set; }
    }
}
