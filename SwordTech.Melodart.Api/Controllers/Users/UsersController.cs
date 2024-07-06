using Microsoft.AspNetCore.Mvc;
using SwordTech.Melodart.Api.Controllers.Base;
using SwordTech.Melodart.Api.Helpers;
using SwordTech.Melodart.Application.Contract.Users;

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
    [ProducesResponseType(typeof(List<UserDto>), 200)]
    public async Task<IActionResult> Get()
    {
        var data = await _userAppService.GetAll();

        return Success(data);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), 200)]
    public async Task<IActionResult> Get(Guid id)
    {
        var data = await _userAppService.GetById(id);

        // if (data == null) return NotFound<object>("yatch not found.", "User not found in database.");

        return Success(data);
    }

    [HttpPost]
    // [ValidateModel]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), 400)]
    [ProducesResponseType(typeof(ApiResponse<UserDto>), 500)]
    public async Task<IActionResult> Post([FromBody] UserCreateDto model)
    {
        try
        {
            var data = await _userAppService.Create(model);

            return Success(data);
        }
        catch (Exception ex)
        {
            return Error<object>("Something went wrong!", ex.Message, null);
        }
    }

    [HttpPut("{id}")]
    // [ValidateModel]
    public async Task<IActionResult> Put(Guid id, [FromForm] UserUpdateDto model)
    {
        try
        {
            var data = await _userAppService.Update(id, model);

            return Success(data);
        }
        catch (Exception ex)
        {
            return Error<object>("Something went wrong!", ex.Message, null);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _userAppService.Delete(id);

            return Success("Yacth deleted successfully.");
        }
        catch (Exception ex)
        {
            return Error<object>("Something went wrong!", ex.Message, null);
        }
    }

    [HttpGet("GetAuthorization")]
    public async Task<IActionResult> GetAuthorization()
    {
        var data = await _userAppService.GetAuthorization();
    
        return Success(data);
    }
}
