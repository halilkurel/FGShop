using FGShop.DtoLayer.ContactDtos;
using FluentValidation;

namespace FGShop.BussinessLayer.ValidationRules.ContactValidationRules
{
    public class UpdateContactDtoValidator : AbstractValidator<UpdateContactDto>
    {
        public UpdateContactDtoValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty().WithMessage("Mesaj alanı boş olamaz.")
                .MaximumLength(500).WithMessage("Mesaj 500 karakterden uzun olamaz.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta alanı boş olamaz.")
                .EmailAddress().WithMessage("Geçersiz e-posta formatı.");
        }
    }
}
