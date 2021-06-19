using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearDiceGame
{
    class PlayField
    {
        public bool avalibe { get; set; }
        public int? sum { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public bool normalfield { get; set; }
        public int potentialsum { get; set; }
        public int validvalue { get; set; }

        public static string[] smallfields = { "Enere", "Toere", "Treere", "Firere", "Femmere", "Seksere" };
        public static string sumstring = "Sum";
        public static string Bonus = "Bonus";
        public static string[] felter2 = { "1 Par", "2 Par", "3 like", "4 like", "Liten Straight", "Stor Straight", "Hus", "Sjanse", "Yatzy" };
        public static string totalsum = "Totalsum";
        public static int menuselect = 0;

        public static List<PlayField> avalibeList = new List<PlayField>();
        public static List<PlayField> unavalibeList = new List<PlayField>();
        public static List<PlayField> totalList = new List<PlayField>();

        public PlayField(string name, int i)
        {
            this.name = name;
            shortname = name.Substring(0, 2);
            validvalue = i;
            this.avalibe = true;
            //this.sumstring = 0;

            this.potentialsum = 0;
            this.normalfield = normalfield;
            avalibeList.Add(this);
            totalList.Add(this);

        }

        public int calcPotential(int i)
        {
            //i = i + 1;
            potentialsum = 0;
            foreach (var dice in Dice.DiceList)
            {
                if (dice._diceValue == i) potentialsum += dice._diceValue;
            }
            return potentialsum;
        }
    }
    
}
