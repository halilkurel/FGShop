using FGShop.CommanLayer;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.ImageDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IColorService
    {
        Task<IResponse<List<ResultColorDto>>> GetAll();
        Task<IResponse<CreateColorDto>> Create(CreateColorDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateColorDto>> Update(UpdateColorDto dto);
    }
}
