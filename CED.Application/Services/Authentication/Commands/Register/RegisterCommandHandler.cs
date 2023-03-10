using CED.Application.Common.Authentication;
using CED.Application.Common.Persistence;
using CED.Domain.Entities.User;
using MediatR;

namespace CED.Application.Services.Authentication.Commands.Register;

public class RegisterCommandHandler 
    : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {

        //Check if the user existed

        if (await _userRepository.GetUserByEmail(command.Email) is not null)
        {
            //  return new AuthenticationResult(false, "User has already existed");
            throw new Exception("User with an email has already existed");
        }
        var user = new User
        {
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = command.Password
        };

        await _userRepository.Insert(user);

        //Create jwt token
        var token = _jwtTokenGenerator.GenerateToken(
            user.Id,
            command.FirstName,
            command.LastName);


        return new AuthenticationResult(user, token);
    }
}

