using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiTesting.Classes.POCO;

namespace ApiTesting.Classes.TestData
{
    public class PlayerData
    {
        public string Name;
        public Player Data;

        public PlayerData(string name, Player data)
        {
            Name = name;
            Data = data;
        }
    }
}
