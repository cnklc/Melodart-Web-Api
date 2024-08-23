using Microsoft.AspNetCore.Mvc;
using SwordTech.Melodart.Api.Controllers.Base;
using SwordTech.Melodart.Api.Helpers;
using SwordTech.Melodart.Application.Contract.Students;
using SwordTech.Melodart.Application.Contract.Students.Models;

namespace SwordTech.Melodart.Api.Controllers.Students;

public class StudentsController : BaseApiController
{
    private readonly IStudentAppService _studentAppService;

    public StudentsController(IStudentAppService studentAppService)
    {
        _studentAppService = studentAppService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<StudentDto>>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Get()
    {
        var data = await _studentAppService.GetAll();

        return Success(data);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<StudentDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Get(Guid id)
    {
        var data = await _studentAppService.GetById(id);

        return Success(data);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<StudentDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Post([FromBody] StudentCreateDto model)
    {
        var data = await _studentAppService.Create(model);

        return Success(data);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<StudentDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Put(Guid id, [FromBody] StudentUpdateDto model)
    {
        var data = await _studentAppService.Update(id, model);

        return Success(data);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _studentAppService.Delete(id);

        return Success("Deleted successfully.");
    }
}
