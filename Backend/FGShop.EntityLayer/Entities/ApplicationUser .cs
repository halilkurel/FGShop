﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.EntityLayer.Entities
{
	public class ApplicationUser: IdentityUser<int>
	{
        public string? Name { get; set; }
        public string? Surname { get; set; }


    }
}
