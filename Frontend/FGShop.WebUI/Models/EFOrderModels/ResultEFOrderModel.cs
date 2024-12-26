

namespace FGShop.WebUI.Models.EFOrderModels
{
    public class ResultEFOrderModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? ProductName { get; set; }
        public int? OrderQuantity { get; set; }
        public string? SizeName { get; set; }
        public string? ColorName { get; set; }
        public int? StatusId { get; set; }
        public string? StatusName { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Price { get; set; }
        public string? CoverPhoto { get; set; }
        public int ProductId { get; set; }

        public int ColorId { get; set; }
        public int SizeId { get; set; }



        //Adress bilgileri
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Neighbourhood { get; set; }
        public string? Address { get; set; }

        
        public string FirstName { get; set; }
        public string SurName { get; set; }



    }
}
