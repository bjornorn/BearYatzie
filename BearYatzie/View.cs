﻿using System;
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
            int counter = 0;

            Console.WriteLine(aktivspiller.name + " sin tur");
            Console.WriteLine(String.Format("{0,-15}|{1,-5}|", "", "Poeng"));
           

            for (var i = 0; i < PlayField.totalList.Count; i++)
            {
                var felt = PlayField.totalList[i];
                Console.Write("\r");
                Console.Write(String.Format("{0,-15}|", felt.name));
            
            foreach (var spiller in Player.PlayerList)
                {
                    //Console.Write(spiller.name);
                    PlayerScoreView(felt, i, spiller, aktivspiller);
                    counter++;
                    if (counter == Player.PlayerList.Count)
                    {
                        Console.Write("\n");
                        counter = 0;

                    }
                    //Console.SetCursorPosition(6 * i,0);
                    //Console.Write("\n");
                }
                //PlayerScoreView(felt, i);
            }

            Console.WriteLine(String.Format("{0,-15}|{1,-12}|{2, 5}|", "Totalsum", "", GameEngine.smallsum));
        }

        private static void PlayerScoreView(PlayField felt, int i, Player spiller, Player aktivspiller)
        {
            if (GameBearYatzie.TurnOn == true)
            {
                if (spiller.playerscore[i].sum == null && spiller.name == aktivspiller.name)
                {
                    Console.Write(String.Format("{0,6}", aktivspiller.playerscore[i].potentialsum));
                }
                else if (spiller.playerscore[i].sum == null && spiller.name != aktivspiller.name)
                {
                    Console.Write(String.Format("{0,6}", spiller.playerscore[i].sum + "tom"));
                }
                else if (spiller.playerscore[i].sum != null && spiller.name == aktivspiller.name)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.Write(String.Format("{0,6}", aktivspiller.playerscore[i].potentialsum));
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                else if (spiller.playerscore[i].sum != null && spiller.name != aktivspiller.name)
                {

                    Console.Write(String.Format("{0,6}", spiller.playerscore[i].sum));

                }
                else { Console.Write(String.Format("{0,6}", spiller.playerscore[i].potentialsum)); }
                //Console.Write("\n");
            }

            if (GameBearYatzie.TurnOn == false)
            {
                if (i == PlayField.menuselect && spiller == aktivspiller)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(String.Format("{0,6}", felt.sum));
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                else if (i != PlayField.menuselect)
                {
                    //Console.BackgroundColor = ConsoleColor.Black;
                    //Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(String.Format("{0,6}", felt.potentialsum));
                    //Console.BackgroundColor = ConsoleColor.Black;
                    //Console.ForegroundColor = ConsoleColor.White;
                }

                else if (i == PlayField.menuselect)
                {
                    //Console.BackgroundColor = ConsoleColor.Green;
                    //Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(String.Format("{0,6}", felt.potentialsum));
                
                    //Console.ForegroundColor = ConsoleColor.White;
                    //Console.BackgroundColor = ConsoleColor.Black;
                }
         
            }

            
        }
        //if (i == 5)
        //{
        //    Console.WriteLine(String.Format("{0,-15}|{1,-6}|{2, 5}|", "Sum", "", GameEngine.smallsum));
        //    Console.WriteLine(String.Format("{0,-15}|{1,-6}|{2, 5}|", "Bonus", "", "0"));
        //}


    

    //Console.WriteLine("\n");



    
    }
}