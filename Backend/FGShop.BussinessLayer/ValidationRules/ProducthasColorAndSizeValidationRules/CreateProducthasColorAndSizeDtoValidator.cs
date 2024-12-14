using FGShop.DtoLayer.ProducthasColorAndProducthasSizeDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.ProducthasColorAndProducthasSizeStockRules
{
	public class CreateProducthasColorAndSizeDtoValidator: AbstractValidator<CreateProducthasColorAndSizeDto>
	{
		public CreateProducthasColorAndSizeDtoValidator()
		{
			// Kategori adı için kurallar
			RuleFor(x => x.ProducthasColorId)
				.NotEmpty().WithMessage("ProducthasColorId boş geçilemez.");

			// Product adı için kurallar
			RuleFor(x => x.SizeId)
				.NotEmpty().WithMessage("ProducthasSizeId boş geçilemez.");
		}
	}
}
