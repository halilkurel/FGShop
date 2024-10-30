using FGShop.DtoLayer.StockDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.StockValidationRules
{
    public class CreateStockDtoValidator: AbstractValidator<CreateStockDto>
    {
        public CreateStockDtoValidator()
        {
            RuleFor(x => x.StockQuantity)
                .NotEmpty().WithMessage("Beden alanı zorunludur.");
        }
    }
}
