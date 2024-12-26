using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.EFLikeDtos
{
    public class ResultLikeDto
    {
        public int? LikeId { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public string? ConvertPhoto { get; set; }
        public int? UserId { get; set; }

    }
}
