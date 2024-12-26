using FGShop.DtoLayer.AuthDtos;
using FGShop.DtoLayer.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
	public interface IUserInformationService
	{
		Task<UserListDto> GetByUserName(string userName);
		Task<string> GetByUserId(int id);
		Task<GetByUserIdDto> GetByUserIdInformation(int id);
		Task<UpdateUserInformationDto> UpdateUserInformationDto(UpdateUserInformationDto dto);


    }
}
