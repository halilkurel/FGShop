namespace FGShop.WebUI.Models.UserAddressModels
{
    public class CreateUserAddressModel
    {
        public int? UserId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Neighbourhood { get; set; }
        public string? Address { get; set; }
    }
}
