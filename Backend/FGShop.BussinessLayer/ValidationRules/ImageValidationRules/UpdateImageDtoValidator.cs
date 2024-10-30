using FGShop.DtoLayer.ImageDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.ImageValidationRules
{
    public class UpdateImageDtoValidator: AbstractValidator<UpdateImageDto>
    {
        public UpdateImageDtoValidator()
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image URL alanı zorunludur.");
        }
    }
}
