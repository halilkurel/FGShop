﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.ProucthasSizeDtos
{
    public class UpdateProducthasSizeDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int SizeId { get; set; }
    }
}