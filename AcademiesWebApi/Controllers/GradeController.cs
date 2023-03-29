using AcademiesWebApi.Application;
using AcademiesWebApi.DTOs.GradeDTOs;
using AcademiesWebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AcademiesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly ILogger<GradeController> _logger;
        private readonly IMapper _mapper;
        private IApplication<Grade> _grade;

        public GradeController(ILogger<GradeController> logger, IMapper mapper, IApplication<Grade> grade)
        {
            _logger = logger;
            _mapper = mapper;
            _grade = grade;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var grades = await _grade.GetAll();

            return Ok(grades);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al intentar obtener una nota con id: {id}");

                return BadRequest();
            }

            var entity = await _grade.GetById(id);
            if (entity == null)
            {
                _logger.LogInformation($"No se encontro ninguna nota con id: {id}");

                return NotFound();
            }

            return Ok(entity);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert([FromBody] AddGradeRequestDTO gradeDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Error al intetar agegar una nota");

                return BadRequest();
            }
            else
            {
                var entity = _mapper.Map<Grade>(gradeDTO);

                await _grade.Insert(entity);

                return Created("", entity);
            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] EditGradeRequestDTO gradelDTO)
        {
            if (!ModelState.IsValid || id != gradelDTO.GradeID)
            {
                _logger.LogError($"Error al intentar actualizar una nota con id: {id}");

                return BadRequest();
            }

            var grade = await _grade.GetById(id);

            if (grade == null)
            {
                _logger.LogInformation($"No se encontro ninguna nota con id: {id}");

                return NotFound();
            }

            grade = _mapper.Map<Grade>(gradelDTO);

            await _grade.Update(grade);

            return Ok(grade);
        }



        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al intentar eliminar una nota con id: {id}");

                return BadRequest();
            }

            var entity = await _grade.GetById(id);

            if (entity == null)
            {
                _logger.LogInformation($"No se encontro ninguna nota con id: {id}");

                return NotFound();
            }

            await _grade.Delete(id);

            return NoContent();
        }

    }
}
