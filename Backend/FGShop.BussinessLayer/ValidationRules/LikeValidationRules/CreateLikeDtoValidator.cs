using FGShop.DtoLayer.LikeDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.LikeValidationRules
{
    public class CreateLikeDtoValidator: AbstractValidator<CreateLikeDto>
    {
        public CreateLikeDtoValidator() {

        }
    }
}
