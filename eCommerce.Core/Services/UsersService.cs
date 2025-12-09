using AutoMapper;
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
    private readonly IMapper _mapper;
    public UsersService(IUsersRepository usersRepository, IMapper mapper)
    {
        _usersRepository = usersRepository;
        _mapper = mapper;
    }
    public async Task<AuthenticationResponse?> LogIn(LoginRequest loginRequest)
    {
        ApplicationUser? user = await _usersRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);
        if (user == null)
        {
            return null;
        }
        //return new AuthenticationResponse(

        //     user.UserID,
        //     user.Email,
        //     user.PersonName,
        //   user.Gender,
        //     "",
        //   Success: true
        //);
        return _mapper.Map<AuthenticationResponse>(user) with
        { Success = true, Token = "token"};
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        //ApplicationUser user = new ApplicationUser
        //{
        //    PersonName = registerRequest.PersonName,
        //    Email = registerRequest.Email,
        //    Password = registerRequest.Password,
        //    Gender = registerRequest.Gender.ToString(),
        //};
         ApplicationUser user = _mapper.Map<ApplicationUser>(registerRequest);
        ApplicationUser ? registerUser = await _usersRepository.AddUser(user);
        if (registerUser == null) 
            {
                return null;
            }
        //return new AuthenticationResponse(registerUser.UserID, registerUser.Email, 
        //    registerUser.PersonName,
        //    registerUser.Gender,"",Success: true);
        return _mapper.Map<AuthenticationResponse>(registerUser) with { Success = true, Token = "token"};

    }
}

