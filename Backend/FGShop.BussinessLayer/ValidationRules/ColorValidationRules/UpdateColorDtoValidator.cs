using FGShop.DtoLayer.ColorDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.ColorValidationRules
{
    public class UpdateColorDtoValidator: AbstractValidator<UpdateColorDto>
    {
        public UpdateColorDtoValidator()
        {
            RuleFor(x => x.ColorName)
                .NotEmpty().WithMessage("Renk alanı boş olamaz.")
                .MaximumLength(40).WithMessage("Title alanı en fazla 40 karakter olabilir.");
        }
    }
}
