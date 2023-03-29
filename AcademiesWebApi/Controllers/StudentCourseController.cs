using AcademiesWebApi.Application;
using AcademiesWebApi.DTOs.StudentCourseDTOs;
using AcademiesWebApi.DTOs.StudentDTOs;
using AcademiesWebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcademiesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCourseController : ControllerBase
    {
        private readonly ILogger<StudentCourseController> _logger;
        private readonly IMapper _mapper;
        private IApplication<StudentCourse> _studentCourse;

        public StudentCourseController(ILogger<StudentCourseController> logger, IMapper mapper, IApplication<StudentCourse> studentCourse)
        {
            _logger = logger;
            _mapper = mapper;
            _studentCourse = studentCourse;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var studentsCourses = await _studentCourse.GetAll();

            return Ok(studentsCourses);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdStudent(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al intentar obtener un estudiante con id: {id} y sus cursos");

                return BadRequest();
            }

            var entity = await _studentCourse.GetById(id);
            if (entity == null)
            {
                _logger.LogInformation($"No se encontro ningun estudiante con id: {id} y sus cursos");

                return NotFound();
            }

            return Ok(entity);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert([FromBody] AddStudentCourseRequestDTO studentCourseDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Error al intetar agegar un estudiante con su curso asignado");

                return BadRequest();
            }
            else
            {
                var entity = _mapper.Map<StudentCourse>(studentCourseDTO);

                await _studentCourse.Insert(entity);

                return Created("", entity);
            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] EditStudentCourseRequestDTO studentCourseDTO)
        {
            if (!ModelState.IsValid || id != studentCourseDTO.StudentCourseID)
            {
                _logger.LogError($"Error al intentar actualizar un estudiante con id: {id} y su curso");

                return BadRequest();
            }

            var studentCourse = await _studentCourse.GetById(id);

            if (studentCourse == null)
            {
                _logger.LogInformation($"No se encontro ningun estudiante con id: {id} y sus curso");

                return NotFound();
            }

            studentCourse = _mapper.Map<StudentCourse>(studentCourseDTO);

            await _studentCourse.Update(studentCourse);

            return Ok(studentCourse);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al intentar eliminar un crso con id: {id} y su curso asignado");

                return BadRequest();
            }

            var entity = await _studentCourse.GetById(id);

            if (entity == null)
            {
                _logger.LogInformation($"No se encontro ningun estudiante con id: {id} y su curso asignado");

                return NotFound();
            }

            await _studentCourse.Delete(id);

            return NoContent();
        }
    }
}
