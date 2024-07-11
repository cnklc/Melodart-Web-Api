using SwordTech.Melodart.Application.Contract.Users.Models;

namespace SwordTech.Melodart.Application.Contract.Users;

public interface IAuthAppService
{
    Task<UserDto> Login(LoginRequest loginRequest);
    Task ResetPassword(ResetPasswordRequest resetPasswordRequest);
    Task<bool> CheckPasswordCode(CheckPasswordCodeRequest checkPasswordCodeRequest);
    Task<bool> ChangePassword(ChangePasswordRequest changePasswordRequest);
}
