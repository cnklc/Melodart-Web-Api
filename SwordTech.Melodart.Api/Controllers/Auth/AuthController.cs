using Microsoft.AspNetCore.Mvc;
using SwordTech.Melodart.Api.Controllers.Base;
using SwordTech.Melodart.Api.Helpers;
using SwordTech.Melodart.Application.Contract.Users;
using SwordTech.Melodart.Application.Contract.Users.Models;

namespace SwordTech.Melodart.Api.Controllers.Auth;

public class AuthController : BaseApiController
{
    private readonly IAuthAppService _authAppService;

    public AuthController(IAuthAppService authAppService)
    {
        _authAppService = authAppService;
    }

    [HttpPost("Login")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var data = await _authAppService.Login(loginRequest);

        return Success(data);
    }

    [HttpPost("ResetPassword")]
    [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest resetPasswordRequest)
    {
        await _authAppService.ResetPassword(resetPasswordRequest);

        return Success(true);
    }

    [HttpPost("CheckPassword")]
    [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> ChechPasswordCode(CheckPasswordCodeRequest checkPasswordCodeRequest)
    {
        var data = await _authAppService.CheckPasswordCode(checkPasswordCodeRequest);

        return Success(data);
    }

    [HttpPost("ChangePassword")]
    [ProducesResponseType(typeof(ApiResponse<bool>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest changePasswordRequest)
    {
        var data = await _authAppService.ChangePassword(changePasswordRequest);

        return Success(data);
    }
}
