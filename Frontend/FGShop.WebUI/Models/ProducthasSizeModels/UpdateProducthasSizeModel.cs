namespace FGShop.WebUI.Models.ProducthasSizeModels
{
    public class UpdateProducthasSizeModel
    {
        public List<SelectedSizeModel> SelectedSizes { get; set; }
        public List<DeselectedSizeModel> DeselectedSizes { get; set; }
        public int ProductId { get; set; }
    }

    public class SelectedSizeModel
    {
        public int ProducthasColorAndSizeId { get; set; }
        public int ProducthasColorId { get; set; }
        public int SizeId { get; set; }
    }

    public class DeselectedSizeModel
    {
        public int ProducthasColorAndSizeId { get; set; }
    }
}
