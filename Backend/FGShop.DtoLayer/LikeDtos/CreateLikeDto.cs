﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.LikeDtos
{
    public class CreateLikeDto
    {
        public int? ProductId { get; set; }
        public int? UserId { get; set; }
    }
}