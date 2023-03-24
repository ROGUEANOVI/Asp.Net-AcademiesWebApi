using AcademiesWebApi.Application;
using AcademiesWebApi.DTOs;
using AcademiesWebApi.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcademiesWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FootballTeamController : ControllerBase
    {
        IApplication<FootballTeam> _football;
        
        public FootballTeamController(IApplication<FootballTeam> football)
        {
            _football = football;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //return Ok(new FootballTeam()
            //{
            //    Name = "Atl. Nacional",
            //    Score = 100
            //});
            return Ok(_football.GetAll());
        }


        [HttpPost]
        public IActionResult Post(FootballTeamDTO dto)
        {
            var f = new FootballTeam()
            {
                Name = dto.Name,
                Score = dto.Score,
                Manager = dto.Manager
            };

            return Ok(_football.Save(f));
        }

    }
}
