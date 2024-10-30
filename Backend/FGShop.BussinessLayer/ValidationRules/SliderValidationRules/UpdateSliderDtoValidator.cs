using FGShop.DtoLayer.SliderDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.SliderValidationRules
{
	public class UpdateSliderDtoValidator: AbstractValidator<UpdateSliderDto>
	{
        public UpdateSliderDtoValidator()
        {
			// Title alanı gereklidir ve 5 ile 100 karakter arasında olmalıdır
			RuleFor(x => x.Title)
				.NotEmpty().WithMessage("Title alanı zorunludur.")
				.Length(5, 100).WithMessage("Title 5 ile 100 karakter arasında olmalıdır.");

			// Description alanı gereklidir ve en az 10 karakter olmalıdır
			RuleFor(x => x.Description)
				.NotEmpty().WithMessage("Description alanı zorunludur.")
				.MinimumLength(10).WithMessage("Description en az 10 karakter uzunluğunda olmalıdır.");

			// ImageUrl geçerli bir URL olmalıdır ve zorunludur
			RuleFor(x => x.ImageUrl)
				.NotEmpty().WithMessage("Image URL alanı zorunludur.");

		}
    }
}
