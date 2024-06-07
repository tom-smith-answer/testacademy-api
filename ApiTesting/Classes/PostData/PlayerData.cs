using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApiTesting.Classes;
using System.Threading.Tasks;

namespace ApiTesting.Classes.PostData
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
