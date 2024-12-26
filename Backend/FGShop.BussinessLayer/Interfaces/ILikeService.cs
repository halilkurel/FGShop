using FGShop.CommanLayer;
using FGShop.DtoLayer.AboutDtos;
using FGShop.DtoLayer.LikeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface ILikeService
    {
        Task<List<ResultLikeDto>> GetAll();
        Task<IResponse<CreateLikeDto>> Create(CreateLikeDto dto);
        Task<IDto> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateLikeDto>> Update(UpdateLikeDto dto);
    }
}
