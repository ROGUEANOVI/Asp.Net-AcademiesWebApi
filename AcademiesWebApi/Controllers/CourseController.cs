using AcademiesWebApi.Application;
using AcademiesWebApi.DTOs.CourseDTOs;
using AcademiesWebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AcademiesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ILogger<CourseController> _logger;

        private readonly IMapper _mapper;

        private IApplication<Course> _course;

        public CourseController(ILogger<CourseController> logger, IMapper mapper, IApplication<Course> course)
        {
            _logger = logger;
            _mapper = mapper;
            _course = course;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var courses = await _course.GetAll();

            return Ok(courses);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al intentar obtener un curso con id: {id}");

                return BadRequest();
            }

            var entity = await _course.GetById(id);
            if (entity == null)
            {
                _logger.LogInformation($"No se encontro ningun course con id: {id}");

                return NotFound();
            }

            return Ok(entity);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Insert([FromBody] AddCourseRequestDTO courseDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Error al intetar agegar un course");

                return BadRequest();
            }
            else
            {
                var entity = _mapper.Map<Course>(courseDTO);

                await _course.Insert(entity);

                return Created("", entity);
            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] EditCourseRequestDTO courseDTO)
        {
            if (!ModelState.IsValid || id != courseDTO.CourseID)
            {
                _logger.LogError($"Error al intentar actualizar un curso con id: {id}");

                return BadRequest();
            }

            var course = await _course.GetById(id);

            if (course == null)
            {
                _logger.LogInformation($"No se encontro ningun curso con id: {id}");

                return NotFound();
            }

            course = _mapper.Map<Course>(courseDTO);

            await _course.Update(course);

            return Ok(course);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al intentar eliminar un curso con id: {id}");

                return BadRequest();
            }

            var entity = await _course.GetById(id);

            if (entity == null)
            {
                _logger.LogInformation($"No se encontro ningun curso con id: {id}");

                return NotFound();
            }

            await _course.Delete(id);

            return NoContent();
        }
    }
}
