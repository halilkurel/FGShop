namespace FGShop.WebUI.Models.EFLikeModels
{
    public class GetByUserIdGetAllLikes
    {
        public int? LikeId { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public string? ConvertPhoto { get; set; }
        public int? UserId { get; set; }
    }
}
