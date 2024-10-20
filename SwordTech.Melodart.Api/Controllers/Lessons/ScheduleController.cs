using Microsoft.AspNetCore.Mvc;
using SwordTech.Melodart.Api.Controllers.Base;
using SwordTech.Melodart.Api.Helpers;
using SwordTech.Melodart.Application.Contract.Lessons;
using SwordTech.Melodart.Application.Contract.Lessons.Models;

namespace SwordTech.Melodart.Api.Controllers.Lessons;

public class ScheduleController : BaseApiController
{
    private readonly IScheduleAppService _scheduleAppService;

    public ScheduleController(IScheduleAppService scheduleAppService)
    {
        _scheduleAppService = scheduleAppService;
    }
    
    [HttpGet("get-today")]
    [ProducesResponseType(typeof(ApiResponse<List<ScheduleDto>>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> GetToday()
    {
        var data = await _scheduleAppService.GetToday();

        return Success(data);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<ScheduleDto>>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Get()
    {
        var data = await _scheduleAppService.GetAll();

        return Success(data);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ScheduleDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Get(Guid id)
    {
        var data = await _scheduleAppService.GetById(id);

        return Success(data);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ScheduleDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Post([FromBody] ScheduleCreateDto model)
    {
        var data = await _scheduleAppService.Create(model);

        return Success(data);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ScheduleDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Put(Guid id, [FromBody] ScheduleUpdateDto model)
    {
        var data = await _scheduleAppService.Update(id, model);

        return Success(data);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _scheduleAppService.Delete(id);

        return Success("Deleted successfully.");
    }
}
