using AcademiesWebApi.Application;
using AcademiesWebApi.DTOs.StudentDTOs;
using AcademiesWebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AcademiesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;

        private readonly IMapper _mapper;

        private IApplication<Student> _student;

        public StudentController(ILogger<StudentController> logger, IMapper mapper, IApplication<Student> student)
        {
            _logger = logger;
            _mapper = mapper;
            _student = student; 
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var students = await _student.GetAll();

            return Ok(students);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al intentar obtener un estudiante con id: {id}");

                return BadRequest();
            }

            var entity = await _student.GetById(id);
            if (entity == null)
            {
                _logger.LogInformation($"No se encontro ningun estudiante con id: {id}");

                return NotFound();
            }

            return Ok(entity);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert([FromBody] AddStudentRequestDTO studentDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Error al intetar agegar un estudiante");

                return BadRequest();
            }
            else
            {
                var entity = _mapper.Map<Student>(studentDTO);

                await _student.Insert(entity);

                return Created("", entity);
            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] EditStudentRequestDTO studentDTO)
        {
            if (!ModelState.IsValid || id != studentDTO.StudentID)
            {
                _logger.LogError($"Error al intentar actualizar un estudiante con id: {id}");

                return BadRequest();
            }

            var student = await _student.GetById(id);

            if (student == null)
            {
                _logger.LogInformation($"No se encontro ningun estudiante con id: {id}");

                return NotFound();
            }

            student = _mapper.Map<Student>(studentDTO);

            await _student.Update(student);

            return Ok(student);
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al intentar eliminar un estudiante con id: {id}");

                return BadRequest();
            }

            var entity = await _student.GetById(id);

            if (entity == null)
            {
                _logger.LogInformation($"No se encontro ningun estudiante con id: {id}");

                return NotFound();
            }

            await _student.Delete(id);

            return NoContent();
        }
    }
}
