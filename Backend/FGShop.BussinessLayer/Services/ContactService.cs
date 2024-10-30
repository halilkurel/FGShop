using AutoMapper;
using FGShop.BussinessLayer.DependencyResolvers.Extensions;
using FGShop.BussinessLayer.Interfaces;
using FGShop.CommanLayer;
using FGShop.DataAccessLayer.Context;
using FGShop.DataAccessLayer.UnitOfWork;
using FGShop.DtoLayer.ContactDtos;
using FGShop.EntityLayer.Entities;
using FluentValidation;


namespace FGShop.BussinessLayer.Services
{
    internal class ContactService : IContactService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateContactDto> _createValidator;
        private readonly IValidator<UpdateContactDto> _updateValidator;
        private readonly FGShopContext _context;

        public ContactService(IUow uow, IMapper mapper, IValidator<CreateContactDto> createValidator, IValidator<UpdateContactDto> updateValidator, FGShopContext context)
        {
            _uow = uow;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _context = context;
        }

        public async Task<IResponse<CreateContactDto>> Create(CreateContactDto dto)
        {
            var ValidationResult = _createValidator.Validate(dto);
            if (ValidationResult.IsValid)
            {
                await _uow.GetRepository<Contact>().Create(_mapper.Map<Contact>(dto));
                await _uow.SaveChanges();

                return new Response<CreateContactDto>(ResponseType.Success, dto);
            }

            else
            {

                return new Response<CreateContactDto>(ResponseType.ValidationError, dto, ValidationResult.CovertToCustomValidationError());
            }
        }

        public async Task<IResponse<List<ResultContactDto>>> GetAll()
        {
            var data = _mapper.Map<List<ResultContactDto>>(await _uow.GetRepository<Contact>().GetAll());

            return new Response<List<ResultContactDto>>(ResponseType.Success, data);
        }

        public async Task<IResponse<IDto>> GetById<IDto>(int id)
        {
            var data = _mapper.Map<IDto>(await _uow.GetRepository<Contact>().GetByFilter(x => x.Id == id));
            if (data == null)
            {
                return new Response<IDto>(ResponseType.NotFound, $"{id} ait data bulunamadı");
            }
            return new Response<IDto>(ResponseType.Success, data);
        }

        public async Task<IResponse> Remove(int id)
        {
            var deletedEntity = await _uow.GetRepository<Contact>().GetByFilter(x => x.Id == id);
            if (deletedEntity != null)
            {
                _uow.GetRepository<Contact>().Remove(deletedEntity);
                await _uow.SaveChanges();
                return new Response(ResponseType.Success);
            }
            else
            {
                return new Response(ResponseType.NotFound, $"{id} ye ait data bulunamadı");
            }
        }

        public async Task<IResponse<UpdateContactDto>> Update(UpdateContactDto dto)
        {
            var result = _updateValidator.Validate(dto);
            if (result.IsValid)
            {
                var updatedEntity = await _uow.GetRepository<Contact>().Find(dto.Id);
                if (updatedEntity != null)
                {
                    _uow.GetRepository<Contact>().Update(_mapper.Map<Contact>(dto), updatedEntity);
                    await _uow.SaveChanges();

                    return new Response<UpdateContactDto>(ResponseType.Success, dto);
                }
                return new Response<UpdateContactDto>(ResponseType.NotFound, $"{dto.Id} ait data bulunamadı");
            }
            else
            {
                return new Response<UpdateContactDto>(ResponseType.ValidationError, dto, result.CovertToCustomValidationError());
            }
        }
    }
}
