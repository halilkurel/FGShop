﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.ProducthasColorAndSizehasStockDtos
{
    public class UpdateProducthasColorAndSizehasStockDto
    {
        public int Id { get; set; }
        public int ProducthasColorAndSizeId { get; set; }
        public int Stock { get; set; }
    }
}