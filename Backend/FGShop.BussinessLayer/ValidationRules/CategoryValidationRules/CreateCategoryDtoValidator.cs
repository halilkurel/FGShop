using FGShop.DtoLayer.CategoryDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.CategoryValidationRules
{
    public class CreateCategoryDtoValidator: AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            // Kategori adı için kurallar
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Kategori adı boş geçilemez.")
                .Length(3, 100).WithMessage("Kategori adı 3 ile 100 karakter içermelidir.");

        }
    }
}
