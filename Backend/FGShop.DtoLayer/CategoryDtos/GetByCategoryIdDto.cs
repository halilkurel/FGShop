using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.CategoryDtos
{
    public class GetByCategoryIdDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string? CoverPhoto { get; set; }
    }
}
