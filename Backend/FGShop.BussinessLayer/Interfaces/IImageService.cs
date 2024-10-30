using FGShop.CommanLayer;
using FGShop.DtoLayer.ImageDtos;
using FGShop.DtoLayer.SliderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IImageService
    {
        Task<IResponse<List<ResultImageDto>>> GetAll();
        Task<IResponse<CreateImageDto>> Create(CreateImageDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateImageDto>> Update(UpdateImageDto dto);
    }
}
