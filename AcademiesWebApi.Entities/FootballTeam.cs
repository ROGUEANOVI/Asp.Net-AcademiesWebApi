using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiesWebApi.Entities
{
    public class FootballTeam : Entity
    {
        public string Name { get; set; } 
        public double Score { get; set; }
        public string Manager { get; set; }

    }
}
