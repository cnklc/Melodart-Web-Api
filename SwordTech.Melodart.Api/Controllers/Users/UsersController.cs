using Microsoft.AspNetCore.Mvc;
using SwordTech.Melodart.Api.Controllers.Base;
using SwordTech.Melodart.Api.Helpers;
using SwordTech.Melodart.Application.Contract.Users;
using SwordTech.Melodart.Application.Contract.Users.Models;

namespace SwordTech.Melodart.Api.Controllers.Users;

// [Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserAppService _userAppService;

    public UsersController(IUserAppService userAppService)
    {
        _userAppService = userAppService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<UserDto>>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Get()
    {
        var data = await _userAppService.GetAll();

        return Success(data);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Get(Guid id)
    {
        var data = await _userAppService.GetById(id);

        return Success(data);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Post([FromBody] UserCreateDto model)
    {
        var data = await _userAppService.Create(model);

        return Success(data);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Put(Guid id, [FromForm] UserUpdateDto model)
    {
        var data = await _userAppService.Update(id, model);

        return Success(data);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _userAppService.Delete(id);

        return Success("Yacth deleted successfully.");
    }

    [HttpGet("GetAuthorization")]
    [ProducesResponseType(typeof(ApiResponse<List<AuthorizationDto>>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> GetAuthorization()
    {
        var data = await _userAppService.GetAuthorization();

        return Success(data);
    }
}
