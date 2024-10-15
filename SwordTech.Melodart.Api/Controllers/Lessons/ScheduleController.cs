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

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<ScheduleDto>>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Get()
    {
        var data = await _scheduleAppService.GetAll();

        return Success(data);
    }
}
