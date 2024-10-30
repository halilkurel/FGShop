using FGShop.DtoLayer.ProductDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.ProductValidationRules
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            // Ürün adı için kurallar
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Ürün adı boş geçilemez.")
                .Length(3, 100).WithMessage("Ürün adı 3 ile 100 karakter içermelidir.");


            // Fiyat için kurallar
            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Fiyat sıfırdan büyük olmalıdır.")
                .ScalePrecision(2, 18).WithMessage("Fiyat en fazla 18 basamak ve 2 ondalık basamak içerebilir.");

        }
    }
}
