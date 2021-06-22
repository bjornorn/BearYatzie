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
      


        public static void TurnController()
        {
            var input = Console.ReadKey();

                switch (input.Key) //Switch on Key enum
                {
                    case ConsoleKey.R:
                       ThrowAndCheck();
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
        public static void ThrowAndCheck()
        {
            var beep = 100;
            rollCounter--;
            foreach (Dice terning in Dice.DiceList)
            {
                if (terning.diceIsLocked == false)
                {
                    terning.DiceRoll();
                    Console.Beep(beep, 250);
                    beep += 100;
                }
            }
            foreach (var felt in PlayField.smallList)
            {
                felt.smallCalcPotential(felt.validvalue);
            }

            PlayField.bigCalcPotential();

            if (rollCounter < 1)
            {
                GameBearYatzie.TurnOn = false;
            }
        }

        public static void PlacePoints2(int fieldNo)
        {
            PlayField.totalList[fieldNo].sum = PlayField.totalList[fieldNo].potentialsum;
        }
        public static void PlacePoints(string input)
        {
            input = input.Length >= 2 ? input : "xx";
            input.ToCharArray();
            input = (char.ToUpper(input[0]) + input.Substring(1));
            input.ToString();

            string shortinput = input.Length >= 2 ? input.Substring(0, 2) : "xx";
            int feil = 0;
            int skrivefeil = 0;
            int localsum = 0;
            int i = 0;
                foreach (var felt in PlayField.smallList)
                {
                    
                    if ((felt.name == input) || (felt.shortname == shortinput))
                    {
                        i = felt.validvalue;
                    }
                    else { feil++; }
                }

                foreach (var felt in PlayField.smallList)
                {
                    if (felt.shortname != shortinput) { skrivefeil++; }
                    if (skrivefeil >= PlayField.smallList.Count)
                    {
                    Console.SetCursorPosition(0, 22);
                    Console.WriteLine(String.Format("{0, -60}", "Feil skrevet, skriv inn på nytt"));
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, 23);
                    string nytt = Console.ReadLine();
                    PlacePoints(nytt);
                    break;
                    }
                }

                foreach (var terning in Dice.DiceList)
                {
                    if (terning._diceValue == i) { localsum += terning._diceValue; }
                }

                foreach (var felt in PlayField.smallList)
                {
                    if (felt.validvalue == i && felt.avalibe == true)
                    {
                        felt.sum = localsum;
                        GameEngine.smallsum += localsum;
                    }
                    else if (felt.validvalue == i && felt.avalibe == false)
                    {
                        Console.SetCursorPosition(0, 22);
                        Console.WriteLine(String.Format("{0, -60}", "Feltet er brukt fra før, skriv inn annet felt"));
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, 23);
                        string gammel = Console.ReadLine();
                        PlacePoints(gammel);
                        break;
                    }
                }
                foreach (var felt in PlayField.smallList)
                {
                    if ((felt.name == input) || (felt.shortname == shortinput)) { felt.avalibe = false; }
                }
        }

        public static void PlayFieldMenu()

        {
            var input = Console.ReadKey();

            switch (input.Key) //Switch on Key enum
            {
                case ConsoleKey.DownArrow:
              
                    if (PlayField.menuselect < 14) { PlayField.menuselect++; }
                    else if (PlayField.menuselect >= 14) { PlayField.menuselect = 0;}
                    View.UpdateView();
                    break;

                case ConsoleKey.UpArrow:
                    if (PlayField.menuselect <= 0) { PlayField.menuselect = 14; }
                    else if (PlayField.menuselect > 0) { PlayField.menuselect--; }
                    View.UpdateView();
                    break;

                case ConsoleKey.Enter:
                 
                    PlacePoints2(PlayField.menuselect);
                    PlayField.fieldchooser = false;
                    View.UpdateView();
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
