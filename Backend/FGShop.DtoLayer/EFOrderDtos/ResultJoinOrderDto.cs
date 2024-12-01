using FGShop.DtoLayer.AuthDtos;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.OrderDtos;
using FGShop.DtoLayer.ProductDtos;
using FGShop.DtoLayer.SizeDtos;
using FGShop.DtoLayer.StatusDtos;
using FGShop.DtoLayer.UserAddressDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.DtoLayer.EFOrderDtos
{
    public class ResultJoinOrderDto
    {
        public GetByOrderIdDto? GetByOrderId { get; set; }
        public GetByProductIdDto? GetByProductId { get; set; }
        public UserListDto? UserList { get; set; }
        public ResultUserAddressDto? ResultUserAddress { get; set; }
        public GetBySizeIdDto? GetBySizeId { get; set; }
        public GetByColorIdDto? GetByColorId { get; set; }
        public ResultStatusDto? ResultStatus { get; set; }



    }
}
