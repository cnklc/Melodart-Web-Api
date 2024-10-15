using Microsoft.AspNetCore.Mvc;
using SwordTech.Melodart.Api.Controllers.Base;
using SwordTech.Melodart.Api.Helpers;
using SwordTech.Melodart.Application.Contract.Lessons;
using SwordTech.Melodart.Application.Contract.Lessons.Models;

namespace SwordTech.Melodart.Api.Controllers.Lessons
{

    public class LessonsController : BaseApiController
    {
        private readonly ILessonAppService _lessonAppService;

        public LessonsController(ILessonAppService lessonAppService)
        {
            _lessonAppService = lessonAppService;
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<LessonDto>>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        public async Task<IActionResult> Get()
        {
            var data = await _lessonAppService.GetAll();

            return Success(data);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<LessonDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        public async Task<IActionResult> Get(Guid id)
        {
            var data = await _lessonAppService.GetById(id);

            return Success(data);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<LessonDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        public async Task<IActionResult> Post([FromBody] LessonCreateDto model)
        {
            var data = await _lessonAppService.Create(model);

            return Success(data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<LessonDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        public async Task<IActionResult> Put(Guid id, [FromBody] LessonUpdateDto model)
        {
            var data = await _lessonAppService.Update(id, model);

            return Success(data);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(ApiResponse), 400)]
        [ProducesResponseType(typeof(ApiResponse), 500)]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _lessonAppService.Delete(id);

            return Success("Deleted successfully.");
        }
    }
}
