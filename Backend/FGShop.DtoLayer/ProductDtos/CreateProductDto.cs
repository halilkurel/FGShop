﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.ProductDtos
{
    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public string? CoverPhoto { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string? Description2 { get; set; }
    }
}