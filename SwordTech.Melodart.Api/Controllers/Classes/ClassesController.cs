using Microsoft.AspNetCore.Mvc;
using SwordTech.Melodart.Api.Controllers.Base;
using SwordTech.Melodart.Api.Helpers;
using SwordTech.Melodart.Application.Contract.Classes;
using SwordTech.Melodart.Application.Contract.Classes.Models;

namespace SwordTech.Melodart.Api.Controllers.Classes;

public class ClassesController : BaseApiController
{
    private readonly IClassAppService _classAppService;

    public ClassesController(IClassAppService classAppService)
    {
        _classAppService = classAppService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<ClassDto>>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Get()
    {
        var data = await _classAppService.GetAll();

        return Success(data);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ClassDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Get(Guid id)
    {
        var data = await _classAppService.GetById(id);

        return Success(data);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ClassDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Post([FromBody] ClassCreateDto model)
    {
        var data = await _classAppService.Create(model);

        return Success(data);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ClassDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Put(Guid id, [FromBody] ClassUpdateDto model)
    {
        var data = await _classAppService.Update(id, model);

        return Success(data);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _classAppService.Delete(id);

        return Success("Deleted successfully.");
    }
}
