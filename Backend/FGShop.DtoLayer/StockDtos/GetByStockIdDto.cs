using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.StockDtos
{
    public class GetByStockIdDto
    {
        public int Id { get; set; }
        public int? StockQuantity { get; set; }
    }
}
