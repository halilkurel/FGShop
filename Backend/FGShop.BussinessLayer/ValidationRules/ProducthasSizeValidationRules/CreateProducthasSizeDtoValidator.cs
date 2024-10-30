using FGShop.DtoLayer.ProucthasSizeDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.ProducthasSizeValidationRules
{
    public class CreateProducthasSizeDtoValidator: AbstractValidator<CreateProducthasSizeDto>
    {
        public CreateProducthasSizeDtoValidator()
        {
            RuleFor(x => x.SizeId)
    .NotEmpty().WithMessage("Beden boş geçilemez.");

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Ürün boş geçilemez.");
        }
    }
}
