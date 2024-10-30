using FGShop.DtoLayer.ProducthasStockDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.ProducthasStockValidationRules
{
    public class CreateProducthasStockDtoValidator: AbstractValidator<CreateProducthasStockDto>
    {
        public CreateProducthasStockDtoValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage("Ürün boş geçilemez.");

            RuleFor(x => x.StockId)
                .NotEmpty().WithMessage("Stok boş geçilemez.");
        }
    }
}
