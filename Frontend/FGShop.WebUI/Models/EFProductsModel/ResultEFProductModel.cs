using FGShop.WebUI.Models.ColorModels;
using FGShop.WebUI.Models.SizeModels;
using static FGShop.WebUI.Models.StockModels.ResultStockModel;

namespace FGShop.WebUI.Models.EFProductsModel
{
	public class ResultEFProductModel
	{
		public int? ProductId { get; set; }
		public string? ProductName { get; set; }
		public string? CoverPhoto { get; set; }
		public decimal? Price { get; set; }
		public string? Description { get; set; }
		public string? Description2 { get; set; }


		public List<SizeResult>? Sizes { get; set; }
		public List<ColorResult>? Colors { get; set; }
		public ResultStock? Stocks { get; set; }
	}
}
