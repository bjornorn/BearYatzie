using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace BearDiceGame
{
    public class PlayField
    {
        public bool avalibe { get; set; }
        public int? sum { get; set; }
        public string name { get; set; }
        public string shortname { get; set; }
        public bool smallfield { get; set; }
        public int potentialsum { get; set; }
        public int validvalue { get; set; }


        public static string[] smallfields = { "Enere", "Toere", "Treere", "Firere", "Femmere", "Seksere" };
     
        public static string[] bigFields = { "Sum", "Bonus", "1 Par", "2 Par", "3 like", "4 like", "Liten Straight", "Stor Straight", "Hus", "Sjanse", "Yatzy", "Totalsum" };
    
        public static int menuselect { get; set; }
        public static bool fieldchooser = true;
        public static List<PlayField> smallList = new List<PlayField>();
        public static List<PlayField> bigList = new List<PlayField>();
        public static List<PlayField> totalList = new List<PlayField>();
        public static string[] specialfields = {"Sum", "Bonus", "Totalsum"};

        public PlayField(string name, int i)
        {
            this.name = name;
            shortname = name.Substring(0, 2);
            validvalue = i;
            this.avalibe = true;
            this.potentialsum = 0;
            this.smallfield = true;
            smallList.Add(this);
            totalList.Add(this);
            sum = null;
         
            
        }
        public PlayField(string name, int i, bool smallfield)
        {
            this.name = name;
            shortname = name.Substring(0, 2);
            validvalue = i;
            this.avalibe = true;
            this.potentialsum = 0;
            this.smallfield = smallfield;
            bigList.Add(this);
            totalList.Add(this);
            sum = null;
            if (specialfields.Contains(name))
            {
                sum = 0;
            }
        }



        public int smallCalcPotential(int i)
        {
            potentialsum = 0;
            foreach (var dice in Dice.DiceList)
            {
                if (dice._diceValue == i) potentialsum += dice._diceValue;
            }
            return potentialsum;
        }

        public static void bigCalcPotential(Player aktivspiller)
        {
            foreach (var storfelt in PlayField.bigList)
            {
                storfelt.potentialsum = 0;
            }
            int[] terningArray = {0, 0, 0, 0, 0, 0};
            bool foundOnePair = false;
            int twopairvalue = 0;
            int twoPairCounter = 0;
            bool found3equal = false;
            bool found4equal = false;
            int LStraightCount = 0;
            int SStraightCount = 0;
            
            int HouseSum = 0;
            int ChanseSum = 0;
            int YatzieSum = 0;




            foreach (var terning in Dice.DiceList)
            {
                int i = terning._diceValue - 1;
                terningArray[i]++;
            }

            for (int i = terningArray.Length; i > 0; i--)
            {
                int terningNummer = i - 1;
                int terningverdi = i;
              

                //1 Par
                if (foundOnePair == false && (terningArray[terningNummer]) > 1)
                {
                    PlayField.totalList[8].potentialsum = terningverdi * 2;
                    foundOnePair = true;
                }
                //2 Par
                if (twoPairCounter < 2 && (terningArray[terningNummer]) > 1)
                {
                    twopairvalue += (terningverdi * 2);
                    twoPairCounter++;
                    //terningArray[terningAntall] -= 2;
                    if (terningArray[terningNummer] > 1 && (terningArray[terningNummer]) > 3)
                    {
                        twopairvalue += (terningverdi * 2);
                        twoPairCounter++;
                    }
                    if (twoPairCounter > 1)
                    {
                        PlayField.totalList[9].potentialsum = twopairvalue;
                    }
                }
                //3 Like
                if ((found3equal == false) && (terningArray[terningNummer] > 2))
                {
                    PlayField.totalList[10].potentialsum = terningverdi * 3;
                    found3equal = true;
                }
                //4 Like
                if ((found4equal == false) && (terningArray[terningNummer] > 3))
                {
                    PlayField.totalList[11].potentialsum = terningverdi * 4;
                    found4equal = true;
                }
             
                //Liten straight

                    if (terningArray[terningNummer] == 1 && terningverdi < 6)
                    {
                        LStraightCount++;
                    }

                    if (LStraightCount > 4)
                    {
                        PlayField.totalList[12].potentialsum = 15;
                    }
                //Stor straight
            
                    if (terningArray[terningNummer] == 1 && terningverdi > 1)
                    {
                        SStraightCount++;
                    }

                    if (SStraightCount > 4)
                    {
                        PlayField.totalList[13].potentialsum = 20;
                    }

                //Hus
                if (terningArray[terningNummer] > 2)
                {
                    if (terningArray[terningNummer] > 4)
                    {
                        PlayField.totalList[14].potentialsum = (terningverdi * 5);
                    }
                    else {

                        int house3Counter = 0;
                    int house2Counter = 0;
                    //int trippel = terningNummer;
                    house3Counter = (terningverdi * 3);
                    for (var index = 0; index < terningArray.Length; index++)
                    {
                        var terningnummer = terningArray[index];
                        if (terningArray[index] > 1 && index != terningNummer)
                        {
                            int terningsverdi2 = index + 1;
                            house2Counter = (terningsverdi2 * 2);
                            PlayField.totalList[14].potentialsum = house3Counter + house2Counter;
                            break;
                        }
                    }
                    }
                }
                //Sjanse
                PlayField.totalList[15].potentialsum += (terningArray[terningNummer] * terningverdi);
                //Yatzie
                if (terningArray[terningNummer] > 4)
                {
                    PlayField.totalList[16].potentialsum = (terningverdi * 5) + 50;
                }
            }
        }
    }
}
