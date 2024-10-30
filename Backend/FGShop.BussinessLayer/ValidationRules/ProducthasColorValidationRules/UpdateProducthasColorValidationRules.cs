using FGShop.DtoLayer.ProducthasColorDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.ProducthasColorValidationRules
{
    public class UpdateProducthasColorValidationRules: AbstractValidator<UpdateProducthasColorDto>
    {
        public UpdateProducthasColorValidationRules()
        {
            // Kategori adı için kurallar
            RuleFor(x => x.ColorId)
                .NotEmpty().WithMessage("Renk boş geçilemez.");

            // Product adı için kurallar
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Ürün boş geçilemez.");
        }
    }
}
