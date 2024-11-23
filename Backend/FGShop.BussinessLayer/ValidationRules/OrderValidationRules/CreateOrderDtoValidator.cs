using FGShop.DtoLayer.OrderDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.ValidationRules.OrderValidationRules
{
    public class CreateOrderDtoValidator: AbstractValidator<CreateOrderDto> 
    {
        public CreateOrderDtoValidator()
        {
            
        }
    }
}
