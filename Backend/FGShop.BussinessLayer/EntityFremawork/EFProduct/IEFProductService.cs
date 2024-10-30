using FGShop.DtoLayer.EFProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFProduct
{
	public interface IEFProductService
	{
		Task<ResultEFProductDto> GetByProductIdResultAll(int id);
	}
}
