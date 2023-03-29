using AcademiesWebApi.Application;
using AcademiesWebApi.DTOs.TeacherDTOs;
using AcademiesWebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AcademiesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ILogger<SchoolController> _logger;

        private readonly IMapper _mapper;

        IApplication<Teacher> _teacher;

        public TeacherController(ILogger<SchoolController> logger, IMapper mapper, IApplication<Teacher> teacher)
        {
            _logger = logger;
            _mapper = mapper;
            _teacher = teacher;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var teachers = await _teacher.GetAll();

            return Ok(teachers);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al intentar obtener un maestro con id: {id}");

                return BadRequest();
            }

            var entity = await _teacher.GetById(id);
            if (entity == null)
            {
                _logger.LogInformation($"No se encontro ningun maestro con id: {id}");

                return NotFound();
            }

            return Ok(entity);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert([FromBody] AddTeacherRequestDTO teacherDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Error al intetar agegar un maestro");

                return BadRequest();
            }
            else
            {
                var entity = _mapper.Map<Teacher>(teacherDTO);

                await _teacher.Insert(entity);

                return Created("", entity);
            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] EditTeacherRequestDTO teacherDTO)
        {
            if (!ModelState.IsValid || id != teacherDTO.TeacherID)
            {
                _logger.LogError($"Error al intentar actualizar un maestro con id: {id}");

                return BadRequest();
            }

            var teacher = await _teacher.GetById(id);

            if (teacher == null)
            {
                _logger.LogInformation($"No se encontro ningun maestro con id: {id}");

                return NotFound();
            }

            teacher = _mapper.Map<Teacher>(teacherDTO);

            await _teacher.Update(teacher);

            return Ok(teacher);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al intentar eliminar un maestro con id: {id}");

                return BadRequest();
            }

            var entity = await _teacher.GetById(id);

            if (entity == null)
            {
                _logger.LogInformation($"No se encontro ningun maestro con id: {id}");

                return NotFound();
            }

            await _teacher.Delete(id);

            return NoContent();
        }
    }
}
