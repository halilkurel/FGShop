using FGShop.EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FGShop.BussinessLayer.Interfaces
{
    public interface IIdentityRoleService
    {
        Task<IdentityResult> CreateRoleAsync(string roleName);
        Task<List<ApplicationRole>> GetAllRolesAsync();
        Task<IdentityResult> DeleteRoleAsync(string roleName);
    }
}
