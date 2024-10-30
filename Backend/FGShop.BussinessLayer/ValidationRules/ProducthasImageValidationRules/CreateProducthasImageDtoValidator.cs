using FGShop.DtoLayer.ProducthasImageDto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.ProducthasImageValidationRules
{
    public class CreateProducthasImageDtoValidator: AbstractValidator<CreateProducthasImageDto>
    {
        public CreateProducthasImageDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Ürün alanı zorunludur.");

            RuleFor(x => x.ImageId)
                .NotEmpty().WithMessage("Resim alanı zorunludur.");
        }
    }
}
