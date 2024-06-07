using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiTesting.Classes;

namespace ApiTesting.Classes.PostData
{
    internal class DataHelper
    {
        PlayerDataList playerDataList = new PlayerDataList();
        public Player GetPlayerData(string name)
        {
            var playerData = playerDataList.AllPlayerData.FirstOrDefault(x => x.Name.Contains(name)) ?? throw new Exception("Player data cannot be found");
            {
                return playerData.Data;
            }
        }
    }
}
