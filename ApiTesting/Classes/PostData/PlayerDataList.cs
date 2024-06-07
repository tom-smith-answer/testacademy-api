using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiTesting.Classes;

namespace ApiTesting.Classes.PostData
{
    public class PlayerDataList
    {
        public PlayerDataList()
        {
        }

        public List<PlayerData> AllPlayerData = new List<PlayerData>
        {
            new PlayerData("Post_Harvey", new Player
        {
            Id = 0,
            FirstName = "Harvey",
            MiddleName = "The Power",
            LastName = "Sembhy",
            DateOfBirth = "1066-06-06T14:30:23.234Z",
            SquadNumber = 69,
            Position = "Everywhere",
            AbbrPosition = "EW",
            Team = "Ninja Warriors",
            League = "Answer under 1100s",
            Starting11 = false,
        })
        };
    }
}