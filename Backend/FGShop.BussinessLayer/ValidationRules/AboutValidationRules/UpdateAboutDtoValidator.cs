using FGShop.DtoLayer.AboutDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.AboutValidationRules
{
	public class UpdateAboutDtoValidator : AbstractValidator<UpdateAboutDto>
	{
		public UpdateAboutDtoValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage("Title alanı boş olamaz.")
				.MaximumLength(100).WithMessage("Title alanı en fazla 100 karakter olabilir.");

			RuleFor(x => x.Description)
				.NotEmpty().WithMessage("Description alanı boş olamaz.")
				.MaximumLength(500).WithMessage("Description alanı en fazla 500 karakter olabilir.");

		}
	}
}
