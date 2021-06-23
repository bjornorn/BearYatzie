using System;
using System.Linq;
using System.Threading;

namespace BearDiceGame
{
    public class View
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

        public static void UpdateView(Player aktivspiller)
        {
            DrawDice();
            ScoreField(aktivspiller);
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

        public static void ScoreField(Player aktivspiller)
        {
            

            Console.WriteLine(aktivspiller.name + " sin tur");
            Console.WriteLine(String.Format("{0,-15}|{1,-5}|", "", "Poeng"));

            //foreach (var felt in PlayField.totalList)
            for (var i = 0; i < PlayField.totalList.Count; i++)
            {
                
                Console.Write("\r\n");
                Console.Write(String.Format("{0,-15}|", PlayField.totalList[i].name));

                PlayerScoreView(i, aktivspiller);
       
            }

            Console.WriteLine(String.Format("{0,-15}|{1,-12}|{2, 5}|", "Totalsum", "", GameEngine.smallsum));
        }

        private static void PlayerScoreView(int i, Player aktivspiller)
        {
            int counter = 0;
            foreach (var spiller in Player.PlayerList)
            {
                if (counter == Player.PlayerList.Count)
                {
                    Console.Write("\n");
                    counter = 0;
                }
                counter++;
              
                if (GameBearYatzie.TurnOn == true)
                {
                    if (spiller == aktivspiller)
                    {

                        if (spiller.playerscore[i] == null)
                        {
                            Console.Write(String.Format("{0,6}", PlayField.totalList[i].potentialsum));
                        }
                        else if (spiller.playerscore[i] != null)
                        {
                            Console.Write(String.Format("{0,6}", spiller.playerscore[i]));
                        }

                    }
                    if (spiller != aktivspiller)
                    {

                        if (spiller.playerscore[i] == null)
                        {
                            Console.Write(String.Format("{0,6}", "0"));
                        }
                        else if (spiller.playerscore[i] != null)
                        {
                            Console.Write(String.Format("{0,6}", spiller.playerscore[i]));
                        }

                    }
                }

                else if (GameBearYatzie.TurnOn == false)
                {
                    if (spiller == aktivspiller)
                    {
                        if (i == PlayField.menuselect && spiller.playerscore[i] == null)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(String.Format("{0,6}", PlayField.totalList[i].potentialsum));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;
                        
                        }

                        else if (i == PlayField.menuselect && spiller.playerscore[i] != null)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write(String.Format("{0,6}", spiller.playerscore[i]));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;

                        }

                        else if (i != PlayField.menuselect && spiller.playerscore[i] == null)
                        {
                            //Console.BackgroundColor = ConsoleColor.Black;
                            //Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0,6}", PlayField.totalList[i].potentialsum));
                            //Console.BackgroundColor = ConsoleColor.Black;
                            //Console.ForegroundColor = ConsoleColor.White;

                        }

                        else if (i != PlayField.menuselect && spiller.playerscore[i] != null)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0,6}", spiller.playerscore[i]));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;
                        }

                     
                    }

                    if (spiller != aktivspiller)
                    {
                        if ((i == PlayField.menuselect) && (spiller.playerscore[i] == null))
                        {
                            //Console.BackgroundColor = ConsoleColor.White;
                            //Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0,6}", "0"));
                            //Console.ForegroundColor = ConsoleColor.White;
                            //Console.BackgroundColor = ConsoleColor.Black;
                        }

                        else if (i == PlayField.menuselect && spiller.playerscore[i] != null)
                        {
                            //Console.BackgroundColor = ConsoleColor.Gray;
                            //Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0,6}", spiller.playerscore[i]));
                            //Console.ForegroundColor = ConsoleColor.White;
                            //Console.BackgroundColor = ConsoleColor.Black;
                        }

                        else if (i != PlayField.menuselect && spiller.playerscore[i] == null)
                        {
                            //Console.BackgroundColor = ConsoleColor.Black;
                            //Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0,6}", "0"));
                            //Console.BackgroundColor = ConsoleColor.Black;
                            //Console.ForegroundColor = ConsoleColor.White;
                        }

                        else if (i != PlayField.menuselect && spiller.playerscore[i] != null)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0,6}", spiller.playerscore[i]));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                    }
                    
                }
               
            }
        }
    }
}