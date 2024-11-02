namespace FGShop.WebUI.Models.AuthModels
{
    public class LoginResponse
    {
        public string? Token { get; set; }
        public string? Message { get; set; }
        public int? ResponseType { get; set; }
    }
}
