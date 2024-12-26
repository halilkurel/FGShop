using FGShop.DtoLayer.EFLikeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.EntityFremawork.EFLike
{
    public interface IEFLikeService
    {
        Task<List<ResultLikeDto>> GetAll(int userId);
        Task<bool> CheckLikeStatusAsync(int productId, int userId);
        Task<int> GetByProductIdandUserId(int productId, int userId);
        Task<int> GetByUserIdLikeQuantity(int userId);
    }
}
