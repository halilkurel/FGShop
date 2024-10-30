using FGShop.CommanLayer;
using FGShop.DtoLayer.ContactDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IContactService
    {
        Task<IResponse<List<ResultContactDto>>> GetAll();
        Task<IResponse<CreateContactDto>> Create(CreateContactDto dto);
        Task<IResponse<IDto>> GetById<IDto>(int id);
        Task<IResponse> Remove(int id);
        Task<IResponse<UpdateContactDto>> Update(UpdateContactDto dto);
    }
}
