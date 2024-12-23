﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.UserAddressDtos
{
    public class UpdateUserAddressDto
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? District { get; set; }
        public string? Neighbourhood { get; set; }
        public string? Address { get; set; }
    }
}
