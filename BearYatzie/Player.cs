using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearDiceGame
{
    public class Player
    {
        public int playerTurns { get; set; }
        public string name { get; set; }

        //public List<PlayerField> playerScore = new List<PlayField>();
        //public List<PlayField> playerscore = new List<PlayField>();
        public List<int?> playerscore = new List<int?>();

        public static List<Player> PlayerList = new List<Player>();
        

        public Player(string name)
        {
            this.playerTurns = 3;
            this.name = name;
            
            foreach (var field in PlayField.totalList)
            {
                playerscore.Add(field.sum);
            }
            PlayerList.Add(this);
        }
    }
    
}
