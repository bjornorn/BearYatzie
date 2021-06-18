using System;
using System.Threading;

namespace BearYatzie
{
    public class View
    {
        public static string WelcomeMessage = "Velkommen til BearYatzie";
        public static string Menu = "Trykk \"N\" for nytt spill\n" +
                                    "Trykk \"M\" for nytt minispill";
        public static string RollInfoText = "Trykk \"R\" for å trille terninger \n" +
                                            "Trykk Z X C V eller B for å spare terning";



        public static void Welcome()
        {
            Console.WriteLine(WelcomeMessage);
            Console.WriteLine();
            Console.WriteLine(Menu);
            string input = Console.ReadLine();
           if (input == "n") new GameBearYatzie();
           else if (input == "m") new GameMiniDice();
        }

        public void UpdateView()
        {
            DrawDice();
            DrawBoard();
        }

        public void DrawDice()
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

        public void DrawBoard()
        {
            Console.WriteLine();
            Console.WriteLine(String.Format("{0,-10}|{1,-10}| {2,-5} |", "", "Mulige Poeng", "Poeng"));
            //Console.WriteLine();
            foreach (var felt in BoardField.totalList)
            {
                Console.Write("\r");
                Console.Write(String.Format("{0,-10}|", felt.name));

                if (felt.sum == null)
                {
                    Console.Write(String.Format("{0,5}       |",felt.potentialsum));
                }
                else 
                {
                    Console.Write(String.Format("  {0,2}    |", "Ferdig"));
                }

                
                if (felt.sum != null)
                {
                    Console.Write(String.Format("{0,4}   | {1,4}", felt.sum, "\n"));
                   
                }
                else
                {
                    Console.Write(String.Format("{0,4}   | {1,4}", "Tom", "\n"));
                }
            }

            Console.WriteLine("\n");
            Console.WriteLine("Total Sum: " + GameEngine.smallsum);
            //foreach (var field in BoardField.avalibeList)
            //{
            //    Console.WriteLine(field.normalfield);
            //}

            //Console.WriteLine(String.Format("{0,-18} | {1,-10}", Board.sumstring, Board.sumstring));
            //Console.WriteLine(Board.Bonus);
            //foreach (var felt in Board.felter2)
            //{
            //    Console.WriteLine(felt);
            //}
            //Console.WriteLine(Board.totalsum);

        }
        public static string ShowCounter()
        {
            string counter = $"Antall terningkast: {GameMiniDice.rollCounter}";
            return counter;
        }
        public static string ShowSum()
        {
            var totalvalue = 0;
            foreach (var terning in Dice.DiceList)
            {

                totalvalue += terning._diceValue;
            }
            string counter = $"Total sumstring: {totalvalue}";
            return counter;
        }


    }
}