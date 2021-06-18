using System;
using System.Collections.Generic;

namespace BearYatzie
{
    public class Dice
    {
        public int _diceValue  {get; set;}
        public bool diceIsLocked { get; set; }
        public string diceName { get; set; }
        public int diceNr { get; set; }
        public static List<Dice> DiceList = new List<Dice>();
        public static int DiceRollCounter = 0;


        public Dice(int i)
        {
           
            diceName = "DiceNo" + (i + 1);
            diceIsLocked = false;
            DiceList.Add(this);
            diceNr = i;


        }

        public int DiceRoll()
        {
           Random random = new Random();
           
           var diceroll = random.Next(1, 7);
           _diceValue = diceroll;
            return _diceValue;

        }

        public static void LockDice(int i)
        {

            DiceList[i].diceIsLocked = !DiceList[i].diceIsLocked;
            //return DiceList[i].diceIsLocked;

            //View.ChooseDice();
        }

        public static void ChooseDice(int i, int cursorX, int cursorY)
        {
            int caseSwitch = i;
            switch (caseSwitch)
            {
                case 0:
                    DiceArt.QuestionDice(cursorX, cursorY);
                    break;
                case 1:
                    DiceArt.OneDice(cursorX, cursorY);
                    break;
                case 2:
                    DiceArt.TwoDice(cursorX, cursorY);
                    break;
                case 3:
                    DiceArt.ThreeDice(cursorX, cursorY);
                    break;
                case 4:
                    DiceArt.FourDice(cursorX, cursorY);
                    break;
                case 5:
                    DiceArt.FiveDice(cursorX, cursorY);
                    break;
                case 6:
                    DiceArt.SixDice(cursorX, cursorY);
                    break;
            }
        }


        

    }
}