using FGShop.CommanLayer;
using FGShop.DtoLayer.ProducthasColorDtos;


namespace FGShop.BussinessLayer.Interfaces
{
    public interface IProducthasColorService
    {
        Task<IResponse<List<ResultProducthasColorDto>>> GetAll();
        Task<IResponse<CreateProducthasColorDto>> Create(CreateProducthasColorDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateProducthasColorDto>> Update(UpdateProducthasColorDto dto);
    }
}
