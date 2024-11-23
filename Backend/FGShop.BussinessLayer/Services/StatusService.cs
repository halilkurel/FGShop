using AutoMapper;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.AboutDtos;
using FGShop.DtoLayer.StatusDtos;
using FGShop.EntityLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Services
{
    public class StatusService : IStatusService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;

        public StatusService(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<CreateStatusDto> Create(CreateStatusDto dto)
        {

            var statusEntity = _mapper.Map<Status>(dto);
            await _uow.GetRepository<Status>().Create(statusEntity);
            await _uow.SaveChanges();
            return dto;
        }

        public async Task<List<ResultStatusDto>> GetAll()
        {
            var data = _mapper.Map<List<ResultStatusDto>>(await _uow.GetRepository<Status>().GetAll());
            return data;
        }

        public async Task<IDto> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Status>().GetByFilter(x => x.Id == id));

            return data;
        }

        public async Task Remove(int id)
        {
            var deletedEntity =  await _uow.GetRepository<Status>().GetByFilter(x => x.Id == id);
            _uow.GetRepository<Status>().Remove(deletedEntity);
            await _uow.SaveChanges();
        }

        public async Task<UpdateStatusDto> Update(UpdateStatusDto dto)
        {
                var updatedEntity = await _uow.GetRepository<Status>().Find(dto.Id);

                    _uow.GetRepository<Status>().Update(_mapper.Map<Status>(dto), updatedEntity);
                    await _uow.SaveChanges();

                return dto;
        }
    }
}
