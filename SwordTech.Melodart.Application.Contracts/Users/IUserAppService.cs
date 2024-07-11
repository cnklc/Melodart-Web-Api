using SwordTech.Melodart.Application.Contract.Base;
using SwordTech.Melodart.Application.Contract.Users.Models;
using SwordTech.Melodart.Domain.User;

namespace SwordTech.Melodart.Application.Contract.Users;

public interface IUserAppService : IAppService<AppUser, UserDto, UserDto, UserCreateDto, UserUpdateDto>
{
    Task<List<AuthorizationDto>> GetAuthorization();
}
