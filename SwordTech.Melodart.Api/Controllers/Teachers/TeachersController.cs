using Microsoft.AspNetCore.Mvc;
using SwordTech.Melodart.Api.Controllers.Base;
using SwordTech.Melodart.Api.Helpers; 
using SwordTech.Melodart.Application.Contract.Teachers;
using SwordTech.Melodart.Application.Contract.Teachers.Models;

namespace SwordTech.Melodart.Api.Controllers.Teachers
{
    public class TeachersController : BaseApiController
    {
        private readonly ITeacherAppService _teacherAppService;

        public TeachersController(ITeacherAppService teacherAppService)
        {
            _teacherAppService = teacherAppService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<TeacherDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        public async Task<IActionResult> Get()
        {
            var data = await _teacherAppService.GetAll();

            return Success(data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TeacherDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _teacherAppService.GetById(id);

            return Success(data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<TeacherDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        public async Task<IActionResult> Post([FromBody] TeacherCreateDto model)
        {
            var data = await _teacherAppService.Create(model);

            return Success(data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TeacherDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        public async Task<IActionResult> Put(Guid id, [FromBody] TeacherUpdateDto model)
        {
            var data = await _teacherAppService.Update(id, model);

            return Success(data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TeacherDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _teacherAppService.Delete(id);

            return Success("Teacher deleted successfully.");
        }
    }
}
