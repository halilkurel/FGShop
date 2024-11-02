namespace FGShop.WebUI.Models.JwtModels
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int ExpiryDuration { get; set; }
    }
}
