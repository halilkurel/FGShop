using FGShop.CommanLayer;
using FGShop.DtoLayer.CategoryDtos;


namespace FGShop.BussinessLayer.Interfaces
{
    public interface ICategoryService
    {
        Task<IResponse<List<ResultCategoryDto>>> GetAll();
        Task<List<ResultCategoryDto>> GetFirst3Category();
        Task<IResponse<CreateCategoryDto>> Create(CreateCategoryDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateCategoryDto>> Update(UpdateCategoryDto dto);
    }
}
