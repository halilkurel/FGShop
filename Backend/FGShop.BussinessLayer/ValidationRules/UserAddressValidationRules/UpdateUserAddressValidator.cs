using FGShop.DtoLayer.UserAddressDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.UserAddressValidationRules
{
    public class UpdateUserAddressValidator: AbstractValidator<UpdateUserAddressDto>
    {
        public UpdateUserAddressValidator()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage("Kullanıcı ID boş geçilemez.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email adresi boş geçilemez.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş geçilemez.")
                .Matches(@"^\+?\d{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Ülke adı boş geçilemez.")
                .Length(2, 50).WithMessage("Ülke adı 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("Şehir adı boş geçilemez.")
                .Length(2, 50).WithMessage("Şehir adı 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.District)
                .NotEmpty().WithMessage("İlçe adı boş geçilemez.")
                .Length(2, 50).WithMessage("İlçe adı 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.Neighbourhood)
                .NotEmpty().WithMessage("Mahalle adı boş geçilemez.")
                .Length(2, 50).WithMessage("Mahalle adı 2 ile 50 karakter arasında olmalıdır.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres bilgisi boş geçilemez.")
                .Length(5, 200).WithMessage("Adres bilgisi 5 ile 200 karakter arasında olmalıdır.");
        }
    }
}
