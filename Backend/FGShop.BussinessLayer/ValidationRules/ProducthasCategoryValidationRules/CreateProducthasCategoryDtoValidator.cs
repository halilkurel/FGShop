using FGShop.DtoLayer.ProducthasCategoryDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.ProducthasCategoryValidationRules
{
    public class CreateProducthasCategoryDtoValidator: AbstractValidator<CreateProducthasCategoryDto>
    {
        public CreateProducthasCategoryDtoValidator()
        {
            // Kategori adı için kurallar
            RuleFor(x => x.CategoryId)
                .NotEmpty().WithMessage("Kategori boş geçilemez.");

            // Product adı için kurallar
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Ürün boş geçilemez.");
        }
    }
}
