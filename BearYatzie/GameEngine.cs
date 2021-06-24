using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearDiceGame
{
    class GameEngine
    {
        public static int rollCounter = 3;
        public static int turns = 14;
        public static int smallsum = 0; 
      


        public static void TurnController(Player aktivspiller)
        {
            var input = Console.ReadKey();

                switch (input.Key) //Switch on Key enum
                {
                    case ConsoleKey.R:
                       ThrowAndCheck(aktivspiller);
                        break;
                    case ConsoleKey.D1:
                        Dice.LockDice(0);
                        break;
                    case ConsoleKey.D2:
                        Dice.LockDice(1);
                        break;
                    case ConsoleKey.D3:
                        Dice.LockDice(2);
                        break;
                    case ConsoleKey.D4:
                        Dice.LockDice(3);
                        break;
                    case ConsoleKey.D5:
                        Dice.LockDice(4);
                        break;
                }
        }
        public static void ThrowAndCheck(Player aktivspiller)
        {
            var beep = 100;
            rollCounter--;
            foreach (Dice terning in Dice.DiceList)
            {
                if (terning.diceIsLocked == false)
                {
                    terning.DiceRoll();
                    //Console.Beep(beep, 250);
                    //beep += 100;
                }
            }
            foreach (var felt in PlayField.totalList)
            {
                
                felt.potentialsum = felt.smallCalcPotential(felt.validvalue);
            }

            PlayField.bigCalcPotential(aktivspiller);

       
        }

        public static void PlacePoints2(int fieldNo, Player aktivspiller)
        {
            //sett poeng i felt
            aktivspiller.playerscore[fieldNo] = PlayField.totalList[fieldNo].potentialsum;
            //fjern nullable
            aktivspiller.playerscore[6] ??= 0;
            aktivspiller.playerscore[17] ??= 0;
            //sum
            if (fieldNo < 6)
            {
                aktivspiller.playerscore[6] = (aktivspiller.playerscore[6] + aktivspiller.playerscore[fieldNo]);
            }
            //bonus
            if (aktivspiller.playerscore[6] > 62)
            {
                aktivspiller.playerscore[7] = 50;
            }           
            //totalsum
            aktivspiller.playerscore[17] = aktivspiller.playerscore[17] + aktivspiller.playerscore[fieldNo];

        }

        public static void PlayFieldMenu(Player aktivspiller)

        {
            var input = Console.ReadKey();

            switch (input.Key) //Switch on Key enum
            {
                case ConsoleKey.DownArrow:
              
                    if (PlayField.menuselect < 17) { PlayField.menuselect++; }
                    else if (PlayField.menuselect >= 17) { PlayField.menuselect = 0;}
                    View.UpdateView(aktivspiller);
                    break;

                case ConsoleKey.UpArrow:
                    if (PlayField.menuselect <= 0) { PlayField.menuselect = 17; }
                    else if (PlayField.menuselect > 0) { PlayField.menuselect--; }
                    View.UpdateView(aktivspiller);
                    break;

                case ConsoleKey.Enter:
                    PlacePoints2(PlayField.menuselect, aktivspiller);
                    PlayField.fieldchooser = false;
                    View.UpdateView(aktivspiller);

                    break;
            }
        }

        public static void NewRound()
        {
            PlayField.fieldchooser = true;
         
            GameBearYatzie.turnCounter--;
            if (GameBearYatzie.turnCounter < 1)
            {
                EndGame();
            }
            else {
                 rollCounter = 3;
            
                foreach (var terning in Dice.DiceList)
                {
                    terning.diceIsLocked = false;
                    terning._diceValue = 0;
                }

                foreach (var field in PlayField.totalList)
                {
                    field.potentialsum = 0;
                }
                    GameBearYatzie.TurnOn = true;
            }
        }

        public static void EndGame()
        {
            Console.SetCursorPosition(0, 22);
            if (smallsum > 30)
            {
                Console.WriteLine(String.Format("{0, -60}", "Gratulerer, ganske bra"));
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 23);
                foreach (var felt in PlayField.totalList)
                {
                    felt.sum = null;
                    felt.avalibe = true;
                    felt.potentialsum = 0;
                }
                smallsum = 0;
                GameBearYatzie.turnCounter = 16;
                NewRound();
                
            }

            else if (smallsum < 31)
            {
                Console.WriteLine(String.Format("{0, -60}", "Dette var dårlig, du er et rasshøl!"));
                Console.Write(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 23);
                foreach (var felt in PlayField.smallList)
                {
                    felt.sum = null;
                    felt.avalibe = true;
                    felt.potentialsum = 0;
                }
                smallsum = 0;
                GameBearYatzie.turnCounter = 16;
                NewRound();

            }

        }
    }
}
