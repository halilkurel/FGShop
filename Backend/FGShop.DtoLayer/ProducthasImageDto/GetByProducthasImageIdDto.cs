using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.ProducthasImageDto
{
    public class GetByProducthasImageIdDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ImageId { get; set; }
    }
}
