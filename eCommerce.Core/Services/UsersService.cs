using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Services;

internal class UsersService : IUserService
{
    private readonly IUsersRepository _usersRepository;
    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }
    public async Task<AuthenticationResponse?> LogIn(LoginRequest loginRequest)
    {
        ApplicationUser? user = await _usersRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);
        if (user == null)
        {
            return null;
        }
        return new AuthenticationResponse(

             user.UserID,
             user.Email,
             user.PersonName,
           user.Gender,
             "",
           Success: true
        );
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        ApplicationUser user = new ApplicationUser {
            PersonName = registerRequest.PersonName,
            Email = registerRequest.Email,
           Password = registerRequest.Password,
            Gender = registerRequest.Gender.ToString(),
        };
        ApplicationUser? registerUser = await _usersRepository.AddUser(user);
        if (registerUser != null) 
            {
                return null;
            }
        return new AuthenticationResponse(registerUser.UserID, registerUser.Email, 
            registerUser.PersonName,
            registerUser.Gender,"",Success: true);

    }
}

