namespace FGShop.WebUI.Models.ProducthasColorModels
{
    public class UpdateProducthasColorRequest
    {
        public List<UpdateProducthasColorModel> AddModels { get; set; }
        public List<UpdateProducthasColorModel> RemoveModels { get; set; }
        public int productId { get; set; }
    }

}
