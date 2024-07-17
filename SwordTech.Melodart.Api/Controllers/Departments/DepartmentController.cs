using Microsoft.AspNetCore.Mvc;
using SwordTech.Melodart.Api.Controllers.Base;
using SwordTech.Melodart.Api.Helpers;
using SwordTech.Melodart.Application.Contract.Departments;
using SwordTech.Melodart.Application.Contract.Departments.Models;

namespace SwordTech.Melodart.Api.Controllers.Departments;

public class DepartmentController : BaseApiController
{
    private readonly IDepartmentAppService _departmentAppService;

    public DepartmentController(IDepartmentAppService departmentAppService)
    {
        _departmentAppService = departmentAppService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<List<DepartmentDto>>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Get()
    {
        var data = await _departmentAppService.GetAll();

        return Success(data);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<DepartmentDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Get(Guid id)
    {
        var data = await _departmentAppService.GetById(id);

        return Success(data);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<DepartmentDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Post([FromBody] DepartmentCreatDto model)
    {
        var data = await _departmentAppService.Create(model);

        return Success(data);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<DepartmentDto>), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Put(Guid id, [FromBody] DepartmentUpdateDto model)
    {
        var data = await _departmentAppService.Update(id, model);

        return Success(data);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), 200)]
    [ProducesResponseType(typeof(ApiResponse), 400)]
    [ProducesResponseType(typeof(ApiResponse), 500)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _departmentAppService.Delete(id);

        return Success("Deleted successfully.");
    }
}
