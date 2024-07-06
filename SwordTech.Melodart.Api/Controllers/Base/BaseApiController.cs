using Microsoft.AspNetCore.Mvc;
using SwordTech.Melodart.Api.Helpers;

namespace SwordTech.Melodart.Api.Controllers.Base;

[ApiController]
// [Authorize]
[Route("api/[controller]")]
public class BaseApiController : Controller
{
    [NonAction]
    protected IActionResult Success<T>(T data, string message = "", string internalMessage = null)
    {
        return Success(new ApiResponse<T>
        {
            Success = true,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult Success<T>(ApiResponse<T> data)
    {
        return Ok(data);
    }

    [NonAction]
    protected IActionResult Created<T>(T data, string message, string internalMessage = null)
    {
        return Created(new ApiResponse<T>
        {
            Success = true,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult Created<T>(ApiResponse<T> data)
    {
        return StatusCode(201, data);
    }

    [NonAction]
    protected IActionResult NoContent<T>(T data, string message, string internalMessage = null)
    {
        return NoContent(new ApiResponse<T>
        {
            Success = true,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult NoContent<T>(ApiResponse<T> data)
    {
        return StatusCode(204, data);
    }

    [NonAction]
    protected IActionResult BadRequest<T>(T data, string message, string internalMessage = null)
    {
        return BadRequest(new ApiResponse<T>
        {
            Success = false,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult BadRequest<T>(ApiResponse<T> data)
    {
        return StatusCode(400, data);
    }

    [NonAction]
    protected IActionResult Unauthorized<T>(T data, string message, string internalMessage = null)
    {
        return Unauthorized(new ApiResponse<T>
        {
            Success = false,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult Unauthorized<T>(ApiResponse<T> data)
    {
        return StatusCode(401, data);
    }

    [NonAction]
    protected IActionResult Forbidden<T>(T data, string message, string internalMessage = null)
    {
        return Forbidden(new ApiResponse<T>
        {
            Success = false,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult Forbidden<T>(ApiResponse<T> data)
    {
        return StatusCode(403, data);
    }

    [NonAction]
    protected IActionResult NotFound<T>(T data, string message, string internalMessage = null)
    {
        return NotFound(new ApiResponse<T>
        {
            Success = false,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult NotFound<T>(ApiResponse<T> data)
    {
        return StatusCode(404, data);
    }

    [NonAction]
    protected IActionResult Error<T>(T data, string message, string internalMessage = null)
    {
        return Error(new ApiResponse<T>
        {
            Success = false,
            Message = message,
            InternalMessage = internalMessage,
            Data = data
        });
    }

    [NonAction]
    protected IActionResult Error<T>(ApiResponse<T> data)
    {
        return StatusCode(500, data);
    }
}
