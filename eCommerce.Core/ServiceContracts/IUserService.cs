using eCommerce.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.ServiceContracts
{
    /// <summary>
    /// Contract for user service that contains use cases for users
    /// </summary>
    public interface IUserService
    {
        Task<AuthenticationResponse?> LogIn (LoginRequest loginRequest);
        Task<AuthenticationResponse?> Register (RegisterRequest registerRequest);
    }
}
