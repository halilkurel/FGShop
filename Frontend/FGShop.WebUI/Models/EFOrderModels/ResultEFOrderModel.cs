using FGShop.WebUI.Models.ColorModels;
using FGShop.WebUI.Models.OrderModels;
using FGShop.WebUI.Models.ProductModels;
using FGShop.WebUI.Models.StatusModels;
using FGShop.WebUI.Models.UserAddressModels;
using FGShop.WebUI.Models.UserModels;
using static FGShop.WebUI.Models.OrderModels.GetByOrderModel;
using static FGShop.WebUI.Models.SizeModels.GetBySizeIdModel;

namespace FGShop.WebUI.Models.EFOrderModels
{
    public class ResultEFOrderModel
    {
        public GetByOrderModelSub? GetByOrderId { get; set; }
        public GetByProductResult? GetByProductId { get; set; }
        public UserInformationModel? UserList { get; set; }
        public ResultUserAddressModel? ResultUserAddress { get; set; }
        public GetByIdSize? GetBySizeId { get; set; }
        public GetByColorIdResult? GetByColorId { get; set; }
        public GetByStatusIdModel? ResultStatus { get; set; }

    }
}
