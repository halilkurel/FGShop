﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.AboutDtos
{
	public class UpdateAboutDto
	{
        public int Id { get; set; }
        public string Title { get; set; }
		public string Description { get; set; }
		public string? Description2 { get; set; }
		public string ImageUrl { get; set; }
	}
}