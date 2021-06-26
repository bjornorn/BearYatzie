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

        public static string RollInfoText = "Trykk \"R\" for å trille terninger. Trykk 1, 2, 3, 4 eller 5 for å holde terning";
        public static string PointInfoText2 = "Bruk piltastene for å velge hvor du vil plassere poeng, trykk \"Enter\" for å plassere";

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
            DrawDice(aktivspiller);
            ScoreField(aktivspiller);
        }

        public static void DrawDice(Player aktivspiller)
        {
            //Console.Clear();
            Console.SetCursorPosition(73, 4);
            Console.Write(aktivspiller.name + " sin tur     ");
            Console.SetCursorPosition(73, 1);
            //Console.WriteLine(String.Format("{0,6}{0,-12}", "",aktivspiller.name + " sin tur"));
            Console.Write("Terningkast: " + GameEngine.rollCounter);
            Console.SetCursorPosition(0, 27);
            var RollOrPlace = (GameBearYatzie.TurnOn == true) ? RollInfoText : PointInfoText2;
            Console.Write(String.Format("       {0,-20}", RollOrPlace + "        "));

            
            //Console.SetCursorPosition(50, 27);
            //Console.Write();





            Console.SetCursorPosition(0, 20);
            var cursorcount = 0;

            foreach (var terning in Dice.DiceList)
            {
                if (terning._diceValue == 0)
                {
                    
                    
                    Dice.ChooseDice(terning._diceValue, cursorcount, 0);
                    Console.SetCursorPosition(cursorcount, 5);
                    Console.WriteLine(" Nr " + (terning.diceNr + 1));
                    //Console.SetCursorPosition(cursorcount +1 , 7);
                    //Console.WriteLine("   ?");
                    cursorcount += 15;
                    continue;
                }

                if (terning.diceIsLocked == false)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Dice.ChooseDice(terning._diceValue, cursorcount, 0);
                    Console.SetCursorPosition(cursorcount, 5);
                    Console.WriteLine(" Nr " + (terning.diceNr + 1));
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
                    Console.SetCursorPosition(cursorcount, 5);
                    Console.WriteLine(" Nr " + (terning.diceNr + 1));
                    //Console.SetCursorPosition(cursorcount + 1, 7);
                    //Console.WriteLine(terning._diceValue);
                    Console.ResetColor();
                    cursorcount += 15;
                }

               
            }
        }

        public static void ScoreField(Player aktivspiller)
        {
            Console.WriteLine();
            Console.Write(String.Format("{0,-15}|", ""));
            foreach (var spiller in Player.PlayerList)
            {
                if (spiller == aktivspiller)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.Write(String.Format("{0,-8}", spiller.name));
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write("|");
                }
                else
                {
                    Console.Write(String.Format("{0,-8}|", spiller.name));
                }
                
            }

            for (var i = 0; i < PlayField.totalList.Count; i++) {
                
                Console.Write("\r\n");
                Console.Write(String.Format("{0,-15}|", PlayField.totalList[i].name));
                PlayerScoreView(i, aktivspiller);
            }

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
                            if (PlayField.totalList[i].potentialsum == 0)
                            {
                                Console.Write(String.Format("{0,8}|", PlayField.totalList[i].potentialsum));
                            }
                      
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write(String.Format("{0,8}", PlayField.totalList[i].potentialsum));
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("|");
                            }
                            
                        }
                        else if (spiller.playerscore[i] != null)
                        {
                        
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0,8}", spiller.playerscore[i]));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("|");
                        }

                    }
                    if (spiller != aktivspiller)
                    {

                        if (spiller.playerscore[i] == null)
                        {
                            Console.Write(String.Format("{0,8}|", "0"));
                        }
                        else if (spiller.playerscore[i] != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0,8}", spiller.playerscore[i]));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("|");
                        }

                    }
                }

                else if (GameBearYatzie.TurnOn == false)
                {
                    if (spiller == aktivspiller)
                    {
                        if (i == PlayField.menuselect && spiller.playerscore[i] == null)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(String.Format("{0,8}", PlayField.totalList[i].potentialsum));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("|");
                        }

                        else if (i == PlayField.menuselect && spiller.playerscore[i] != null)
                        {
                            Console.BackgroundColor = ConsoleColor.Gray;
                            Console.ForegroundColor = ConsoleColor.DarkYellow;
                            Console.Write(String.Format("{0,8}", spiller.playerscore[i]));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.Write("|");
                        }

                        else if (i != PlayField.menuselect && spiller.playerscore[i] == null)
                        {
                            if (PlayField.totalList[i].potentialsum == 0)
                            {
                                Console.Write(String.Format("{0,8}|", PlayField.totalList[i].potentialsum));
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write(String.Format("{0,8}", PlayField.totalList[i].potentialsum));
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.Write("|");
                            }

                        }

                        else if (i != PlayField.menuselect && spiller.playerscore[i] != null)
                        {
                          
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0,8}", spiller.playerscore[i]));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("|");

                        }

                     
                    }

                    if (spiller != aktivspiller)
                    {
                        if ((i == PlayField.menuselect) && (spiller.playerscore[i] == null))
                        {
                          
                            Console.Write(String.Format("{0,8}|", "0"));
                         
                        }

                        else if (i == PlayField.menuselect && spiller.playerscore[i] != null)
                        {
                            Console.Write(String.Format("{0,8}|", spiller.playerscore[i]));
                        }

                        else if (i != PlayField.menuselect && spiller.playerscore[i] == null)
                        {
                            Console.Write(String.Format("{0,8}|", "0"));
                        }

                        else if (i != PlayField.menuselect && spiller.playerscore[i] != null)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write(String.Format("{0,8}", spiller.playerscore[i]));
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.Write("|");
                        }
                    }
                    
                }
               
            }
        }
    }
}