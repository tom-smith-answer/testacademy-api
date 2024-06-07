using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTesting.Classes
{
    public class Player
    {
        public long Id { get; set; }

        public string? FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? DateOfBirth { get; set; }

        public int SquadNumber { get; set; }

        public string? Position { get; set; }

        public string? AbbrPosition { get; set; }

        public string? Team { get; set; }

        public string? League { get; set; }

        public bool Starting11 { get; set; }

    }
}
