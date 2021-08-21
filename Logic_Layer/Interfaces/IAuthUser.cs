using Data_Access.Data.Entities;
using Logic_Layer.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Logic_Layer.Interfaces
{
    public interface IAuthUser : IGenericRepository<AuthUser>
    {
        Task<AuthUser> Validate_Login(LoginAuthUserDTO model);
    }
}
