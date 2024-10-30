using FGShop.DtoLayer.SizeDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.SizeValidationRules
{
    public class CreateSizeDtoValidator: AbstractValidator<CreateSizeDto>
    {
        public CreateSizeDtoValidator()
        {
            RuleFor(x => x.SizeName)
               .NotEmpty().WithMessage("Beden alanı zorunludur.");
        }
    }
}
