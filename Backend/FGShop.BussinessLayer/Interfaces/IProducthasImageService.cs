using FGShop.CommanLayer;
using FGShop.DtoLayer.ProducthasImageDto;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IProducthasImageService
    {
        Task<IResponse<List<ResultProducthasImageDto>>> GetAll();
        Task<IResponse<CreateProducthasImageDto>> Create(CreateProducthasImageDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateProducthasImageDto>> Update(UpdateProducthasImageDto dto);
    }
}
