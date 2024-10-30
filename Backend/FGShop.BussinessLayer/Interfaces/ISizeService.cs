using FGShop.CommanLayer;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.SizeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface ISizeService
    {
        Task<IResponse<List<ResultSizeDto>>> GetAll();
        Task<IResponse<CreateSizeDto>> Create(CreateSizeDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateSizeDto>> Update(UpdateSizeDto dto);
    }
}
