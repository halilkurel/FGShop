using FGShop.DtoLayer.ImageDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.ImageValidationRules
{
    public class CreateImageDtoValidor: AbstractValidator<CreateImageDto>
    {
        public CreateImageDtoValidor()
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("Image URL alanı zorunludur.");
        }
    }
}
