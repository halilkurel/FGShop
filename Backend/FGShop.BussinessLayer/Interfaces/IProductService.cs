using FGShop.CommanLayer;
using FGShop.DtoLayer.ProductDtos;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IProductService
    {
        Task<IResponse<List<ResultProductDto>>> GetAll();
        Task<List<ResultProductDto>> GetLastSixProduct();
        Task<IResponse<CreateProductDto>> Create(CreateProductDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateProductDto>> Update(UpdateProductDto dto);
    }
}
