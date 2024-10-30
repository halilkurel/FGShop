using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.ProducthasStockDtos
{
    public class ResultProducthasStockDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int StockId { get; set; }
    }
}
