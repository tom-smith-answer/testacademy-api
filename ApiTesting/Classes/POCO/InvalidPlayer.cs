using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiTesting.Classes.POCO
{
    internal class InvalidPlayer
    {
        public long Id { get; set; }

        public int? FirstName { get; set; }

        public int? MiddleName { get; set; }

        public int? LastName { get; set; }

        public int? DateOfBirth { get; set; }

        public string? SquadNumber { get; set; }

        public int? Position { get; set; }

        public int? AbbrPosition { get; set; }

        public int? Team { get; set; }

        public int? League { get; set; }

        public string? Starting11 { get; set; }
    }
}
