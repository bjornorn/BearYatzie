using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BearDiceGame
{
    class GameEngine
    {
        public static int rollCounter = 3;
        public static int antallspillere = 1;
        
        //public static int turns = 0;
     
       


        public static void TurnController(Player aktivspiller)
        {
            Console.SetCursorPosition(0, 27);
            var input = Console.ReadKey();

                switch (input.Key) //Switch on Key enum
                {
                    case ConsoleKey.R:
                        Console.SetCursorPosition(0, 27);
                        Console.Write(" ");
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
                aktivspiller.playerscore[6] += PlayField.totalList[fieldNo].potentialsum; ;
            }
            
            //bonus
            if (aktivspiller.playerscore[6] > 62 && aktivspiller.playergotbonus == false )
            {
                aktivspiller.playerscore[7] = 50;
                aktivspiller.playerscore[17] = 50 + aktivspiller.playerscore[17];
                aktivspiller.playergotbonus = true;

            }
            //totalsum

            aktivspiller.playerscore[17] += PlayField.totalList[fieldNo].potentialsum;
            
        }

        public static void PlayFieldMenu(Player aktivspiller)

        {

            if (aktivspiller.playerscore[PlayField.menuselect] != null)
            {
                for (var index = 0; index < aktivspiller.playerscore.Count - 1; index++)
                {
                    var felt = aktivspiller.playerscore[index];
                    if (felt == null)
                    {
                        PlayField.menuselect = index;
                    break;
                    }

                }
        }
        View.UpdateView(aktivspiller);
            //View.ScoreField(aktivspiller);
            
            Console.SetCursorPosition(0, 27);
            var input = Console.ReadKey();
           
            switch (input.Key) //Switch on Key enum
            {
                  
                case ConsoleKey.DownArrow:
                    do
                    {
                        PlayField.menuselect++;
                        if (PlayField.menuselect > 16) { PlayField.menuselect = 0; }
                        
                    } while (aktivspiller.playerscore[PlayField.menuselect] != null);
                    break;

                case ConsoleKey.UpArrow:
                    do
                    {
                        PlayField.menuselect--;
                        if (PlayField.menuselect < 0) { PlayField.menuselect = 16; }

                    } while (aktivspiller.playerscore[PlayField.menuselect] != null);
                    break;
                case ConsoleKey.Enter:
                    PlacePoints2(PlayField.menuselect, aktivspiller);
                    PlayField.fieldchooser = false;
                    //View.UpdateView(aktivspiller);
                    break;
                default:
                    Console.SetCursorPosition(0, 27);
                    Console.Write(" ");
                    PlayFieldMenu(aktivspiller);
                    break;
                    
            }
        }

        public static void NextPlayer(Player aktivspiller)
        {
            
            PlayField.fieldchooser = true;
            GameBearYatzie.turnCounter--;
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
                

                if (GameBearYatzie.turnCounter <= 0)
                {
                    EndGame(aktivspiller);
                }

                GameBearYatzie.TurnOn = true;
        }

        public static void EndGame(Player aktivspiller)
        {
            GameBearYatzie.GameOn = false;


            Console.SetCursorPosition(7, 27);
            Console.ForegroundColor = ConsoleColor.Red;
            //Console.WriteLine(winnerList[0] + " vant med " + winnerscore + " poeng");
            
            Console.Write("        Spillet er over, trykk \"N\" for nytt spill                                    ");
            Console.ForegroundColor = ConsoleColor.White;
            


            foreach (var player in Player.PlayerList)
            {
                for (var index = 0; index < player.playerscore.Count; index++)
                {


                    if (index == 6 || index == 7 || index == 17)
                    {
                        player.playerscore[index] = 0;
                    }
                    else
                    {
                        player.playerscore[index] = null;
                    }
                }
            }

            GameBearYatzie.turnCounter = (Player.PlayerList.Count * 15);
            EndMenu(aktivspiller);

        }

        public static void EndMenu(Player aktivspiller)
        {
            Console.SetCursorPosition(0, 27);
            var input = Console.ReadKey();
           
            switch (input.Key) //Switch on Key enum
            {

                case ConsoleKey.N:
                    NextRound();
                    break;
                default:
                    Console.SetCursorPosition(0, 27);
                    Console.Write(" ");
                    EndMenu(aktivspiller);
                    break;
            }
        }

        public static void NextRound()
        {
            GameBearYatzie.GameOn = true;
            
            View.UpdateView(Player.PlayerList[0]);
        }

        public static void createPlayers(int antallspillere)
        {
            for (int i = 0; i < antallspillere; i++)
            {
                Console.SetCursorPosition(20, 12);
                Console.WriteLine("Skriv inn navn på spiller " + (i + 1));
                Console.SetCursorPosition(20, 13);
                string playername = Console.ReadLine();
                if (playername.Length > 8)
                {
                    playername = playername.Substring(0, 8);
                }
                else if (playername.Length < 1)
                {
                    playername = "Spiller" + (i + 1);
                }
                new Player(playername);
                Console.SetCursorPosition(20, 13);
                Console.WriteLine("                          ");

            }
            Console.Clear();
      
        }
    }
}
