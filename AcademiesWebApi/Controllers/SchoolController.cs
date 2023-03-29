using AcademiesWebApi.Application;
using AcademiesWebApi.DTOs.SchoolDTOs;
using AcademiesWebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AcademiesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolController : ControllerBase
    {
        private readonly ILogger<SchoolController> _logger;

        private readonly IMapper _mapper;

        IApplication<School> _school;

        public SchoolController(ILogger<SchoolController> logger, IMapper mapper, IApplication<School> school)
        {
            _logger = logger;
            _mapper = mapper;
            _school = school;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var schools = await _school.GetAll();  

            return Ok(schools);
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al intentar obtener una escuela con id: {id}");
               
                return BadRequest();
            }

            var entity = await _school.GetById(id);
            if (entity == null)
            {
                _logger.LogInformation($"No se encontro ninguna escuela con id: {id}");
             
                return NotFound();
            }

            return Ok(entity);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Insert([FromBody] AddSchoolRequestDTO schoolDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Error al intetar agegar una escuela");

                return BadRequest();
            }
            else
            {
                var entity = _mapper.Map<School>(schoolDTO);

                await _school.Insert(entity);

                return Created("", entity);
            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update( int id,[FromBody] EditSchoolRequestDTO schoolDTO)
        {
            if (!ModelState.IsValid  || id != schoolDTO.SchoolID)
            {
                _logger.LogError($"Error al intentar actualizar una escuela con id: {id}");

                return BadRequest();
            }

            var school = await _school.GetById(id);

            if (school == null)
            {
                _logger.LogInformation($"No se encontro ninguna escuela con id: {id}");

                return NotFound();
            }

            school = _mapper.Map<School>(schoolDTO);

            await _school.Update(school);

            return Ok(school);
        }



        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                _logger.LogError($"Error al intentar eliminar una escuela con id: {id}");

                return BadRequest();
            }

            var entity = await _school.GetById(id);

            if (entity == null)
            {
                _logger.LogInformation($"No se encontro ninguna escuela con id: {id}");

                return NotFound();
            }

            await _school.Delete(id);

            return NoContent();
        }
    }
}
