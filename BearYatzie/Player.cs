using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearDiceGame
{
    class Player
    {
        public int playerTurns { get; set; }
        public string name { get; set; }

        public static List<Player> PlayerList = new List<Player>();

        public Player(string name)
        {
            this.playerTurns = 3;
            this.name = name;
            PlayerList.Add(this);
        }
    }
    
}
