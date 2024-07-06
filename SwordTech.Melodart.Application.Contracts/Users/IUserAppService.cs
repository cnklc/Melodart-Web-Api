using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Domain.User;

namespace SwordTech.Melodart.Application.Contract.Users;

public interface IUserAppService : IAppService<AppUser, UserDto, UserDto, UserCreateDto, UserUpdateDto>
{
    Task<UserDto> Login(LoginDto login);

    Task<bool> ResetPassword(string email);

    Task<bool> ChangePassword(ChangePasswordDto input);
    Task<List<Authorization>> GetAuthorization();
}
