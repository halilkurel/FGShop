using FGShop.DtoLayer.EFProductDtos;
using FGShop.DtoLayer.SizeDtos;
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

		//Bu metotta ürünün rengine ait olan bedenleri listeleyeceğiz
		Task<List<ResultSizeDto>> GetByProductandColorIdResultAll(int productId, int colorId);
	}
}
