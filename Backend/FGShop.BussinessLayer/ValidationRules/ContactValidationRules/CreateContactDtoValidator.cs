using FGShop.DtoLayer.ContactDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.ContactValidationRules
{
    public class CreateContactDtoValidator: AbstractValidator<CreateContactDto>
    {
        public CreateContactDtoValidator()
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
