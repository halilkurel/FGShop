﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.EntityLayer.Entities
{
    public class Order: BaseEntity
    {
        public int UserId { get; set; }
        public int? ProductId { get; set; }
        public int OrderQuantity { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int StatusId { get; set; }
        public int UserAddressId { get; set; }
        public DateTime OrderDate { get; set; }


        public ApplicationUser? User { get; set; }
        public Size? Size { get; set; }
        public Color? Color { get; set; }
        public Status? Stasus { get; set; }
        public UserAddress? UserAddress { get; set; }
        public Product? Product { get; set; }


    }
}
