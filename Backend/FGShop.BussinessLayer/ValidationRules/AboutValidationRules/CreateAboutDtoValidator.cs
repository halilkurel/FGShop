using FGShop.DtoLayer.AboutDtos;
using FluentValidation;

namespace FGShop.BussinessLayer.ValidationRules.AboutValidationRules
{
	public class CreateAboutDtoValidator : AbstractValidator<CreateAboutDto>
	{
		public CreateAboutDtoValidator()
		{
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage("Title alanı boş olamaz.")
				.MaximumLength(100).WithMessage("Title alanı en fazla 100 karakter olabilir.");

			RuleFor(x => x.Description)
				.NotEmpty().WithMessage("Description alanı boş olamaz.")
				.MaximumLength(500).WithMessage("Description alanı en fazla 500 karakter olabilir.");

			RuleFor(x => x.ImageUrl)
				.NotEmpty().WithMessage("ImageUrl alanı boş olamaz.");
		}
	}
}
