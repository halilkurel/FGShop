﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.CommanLayer
{
    public interface IResponse<T>: IResponse
    {
        T Data { get; set; }
        List<CustomValidationError> ValidationErrors { get; set; }
        string Token { get; set; }
    }
}
