using FGShop.CommanLayer;
using FGShop.DtoLayer.ColorDtos;
using FGShop.DtoLayer.ProucthasSizeDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IProducthasSizeService
    {
        Task<IResponse<List<ResultProducthasSizeDto>>> GetAll();
        Task<IResponse<CreateProducthasSizeDto>> Create(CreateProducthasSizeDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateProducthasSizeDto>> Update(UpdateProducthasSizeDto dto);
    }
}
