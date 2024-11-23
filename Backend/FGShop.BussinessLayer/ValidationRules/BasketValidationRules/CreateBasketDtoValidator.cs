using FGShop.DtoLayer.BasketDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.BasketValidationRules
{
	public class CreateBasketDtoValidator: AbstractValidator<CreateBasketDto>
	{
        public CreateBasketDtoValidator()
        {
			RuleFor(x => x.ProductId)
	.NotNull().WithMessage("Ürün ID boş olamaz.")
	.GreaterThan(0).WithMessage("Ürün ID 0'dan büyük olmalıdır.");

			RuleFor(x => x.ColorId)
				.NotNull().WithMessage("Renk ID boş olamaz.")
				.GreaterThan(0).WithMessage("Renk ID 0'dan büyük olmalıdır.");

			RuleFor(x => x.SizeId)
				.NotNull().WithMessage("Beden ID boş olamaz.")
				.GreaterThan(0).WithMessage("Beden ID 0'dan büyük olmalıdır.");

			RuleFor(x => x.OrderQuantity)
				.NotNull().WithMessage("Sipariş miktarı boş olamaz.")
				.GreaterThan(0).WithMessage("Sipariş miktarı 0'dan büyük olmalıdır.")
				.LessThanOrEqualTo(100).WithMessage("Sipariş miktarı 100'den fazla olamaz.");
		}
    }
}
