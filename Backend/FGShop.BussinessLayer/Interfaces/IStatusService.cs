using FGShop.CommanLayer;
using FGShop.DtoLayer.StatusDtos;
using FGShop.DtoLayer.StatusDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IStatusService
    {
        Task<List<ResultStatusDto>> GetAll();
        Task<CreateStatusDto> Create(CreateStatusDto dto);
        Task<IDto> GetById<IDto>(int id);
        Task Remove(int id);
        Task<UpdateStatusDto> Update(UpdateStatusDto dto);
    }
}
