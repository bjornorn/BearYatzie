using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearDiceGame
{
    class OldView
    {
        
        public static string WelcomeMessage = "Velkommen til BearYatzie";
        public static string Menu = "Trykk \"N\" for nytt spill\n" +
                                    "Trykk \"M\" for nytt minispill";
        public static string RollInfoText = "Trykk \"R\" for å trille terninger \n" +
                                            "Trykk Z X C V eller B for å fryse terning";

        public static void Welcome()
        {
            Console.WriteLine(WelcomeMessage);
            Console.WriteLine();
            Console.WriteLine(Menu);
            string input = Console.ReadLine();
            if (input == "n") new GameBearYatzie();
        }

        public static void UpdateView()
        {
            DrawDice();
            ScoreField();
        }

        public static void DrawDice()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 27);
            Console.WriteLine("Tilgjengelige terningkast: " + GameEngine.rollCounter);
            Console.WriteLine(RollInfoText);
            Console.SetCursorPosition(0, 20);
            var cursorcount = 0;

            foreach (var terning in Dice.DiceList)
            {
                if (terning._diceValue == 0)
                {
                    Dice.ChooseDice(terning._diceValue, cursorcount, 0);
                    Console.SetCursorPosition(cursorcount, 6);
                    Console.WriteLine(" Nr " + (terning.diceNr));
                    //Console.SetCursorPosition(cursorcount +1 , 7);
                    //Console.WriteLine("   ?");
                    cursorcount += 15;
                    continue;
                }

                if (terning.diceIsLocked == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Dice.ChooseDice(terning._diceValue, cursorcount, 0);
                    Console.SetCursorPosition(cursorcount, 6);
                    Console.WriteLine(" Nr " + (terning.diceNr));
                    //Console.SetCursorPosition(cursorcount + 1, 7);
                    //Console.WriteLine(terning._diceValue);
                    Console.ResetColor();
                    cursorcount += 15;
                    continue;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Dice.ChooseDice(terning._diceValue, cursorcount, 0);
                    Console.SetCursorPosition(cursorcount, 6);
                    Console.WriteLine(" Nr " + (terning.diceNr));
                    //Console.SetCursorPosition(cursorcount + 1, 7);
                    //Console.WriteLine(terning._diceValue);
                    Console.ResetColor();
                    cursorcount += 15;
                }
                Console.WriteLine();
            }
        }

        public static void ScoreField()
        {
            int counter = 1;

            Console.WriteLine();
            Console.WriteLine(String.Format("{0,-15}|{1,-10}|{2,-5}|", "", "Mulige Poeng", "Poeng"));


            for (var i = 0; i < PlayField.totalList.Count; i++)
            {
                var felt = PlayField.totalList[i];
                Console.Write("\r");
                Console.Write(String.Format("{0,-15}|", felt.name));


                if (felt.sum == null)
                {
                    if (i == PlayField.menuselect && GameBearYatzie.TurnOn == false)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write(String.Format("{0,12}", felt.potentialsum));
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.Write(String.Format("{0,12}", felt.potentialsum));
                    }
                }
                else
                {
                    if (i == PlayField.menuselect && GameBearYatzie.TurnOn == false)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGreen;
                        Console.Write(String.Format("{0,12}", felt.potentialsum));
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                    {
                        Console.Write(String.Format("{0,12}", "Ferdig"));
                    }
                }

                if (felt.sum != null)
                {
                    Console.Write(String.Format("|{0,5}|{1,4}", felt.sum, "\n"));
                }
                else
                {
                    Console.Write(String.Format("|{0,5}|{1,4}", "Tom", "\n"));
                }

                if (i == 5)
                {
                    //Console.WriteLine(String.Format("{0,-15}|{1,-12}|{2, 5}|", "Sum", "", GameEngine.smallsum));
                    Console.WriteLine(String.Format("{0,-15}|{1,-12}|{2, 5}|", "Bonus", "", "0"));
                }


            }
            //Console.WriteLine(String.Format("{0,-15}|{1,-12}|{2, 5}|", "Totalsum", "", GameEngine.smallsum));
            //Console.WriteLine("\n");



        }
    
}

}
