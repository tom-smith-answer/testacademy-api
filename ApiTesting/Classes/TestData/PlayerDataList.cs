using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiTesting.Classes.POCO;

namespace ApiTesting.Classes.TestData
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
        }),
            new PlayerData("Post_Duncan", new Player
        {
            Id = 0,
            FirstName = "Duncan",
            MiddleName = "The Darkness",
            LastName = "Denio",
            DateOfBirth = "2015-06-06T14:30:23.234Z",
            SquadNumber = 1,
            Position = "The shadows",
            AbbrPosition = "SH",
            Team = "JSON and the Argonauts",
            League = "Answer under 11s",
            Starting11 = true,
        }),
            new PlayerData("Post_Incomplete", new Player
            {
                Id=0,
                FirstName="Duncan",
                LastName="Denio",
                Starting11=true
            }),
            new PlayerData("Post_Invalid", new Player
        {
            Id = 0,
            FirstName = "",
            MiddleName = "",
            LastName = "*****",
            DateOfBirth = "1066-06-06T14:30:23.234Z",
            SquadNumber = 00,
            Position = "<><><>",
            AbbrPosition = "<>",
            Team = "######",
            League = "@@@@@@@",
            Starting11 = false,
        })
        };
    }
}