using System;
using System.Security.Cryptography.X509Certificates;

namespace BearDiceGame
{
    public class GameBearYatzie 
    {
        public static bool GameOn = false;
        public static bool TurnOn = false;
        public static int turnCounter = 15;
        public static int players = 2;

        public GameBearYatzie()
        {
            GameOn = true;
            for (int i = 0; i < 5; i++) new Dice(i);
           
           

            int id = 1;
            foreach (var felt in PlayField.smallfields)
            {
                new PlayField(felt, id);
                id++;
            }
            foreach (var felt in PlayField.bigFields)
            {
                new PlayField(felt, id, false);
                id++;
            }

            Player Bjørn = new Player("Bjørn");
            Player Andre = new Player("Andre");


            TurnOn = true;
        }
        //public void UpdateView(Player aktivspiller)
        //{
        //    View.UpdateView(aktivspiller);
        //}
        public void PlayGame() {
            View.UpdateView(Player.PlayerList[0]);
            Console.CursorVisible = true;
            while (GameOn)
            {
                foreach (var aktivspiller in Player.PlayerList)
                {
                    while (TurnOn)
                    { GameEngine.TurnController(aktivspiller);
            
                      View.UpdateView(aktivspiller);
                      if (GameEngine.rollCounter < 1)
                      {
                          GameBearYatzie.TurnOn = false;
                      }
                    }

                    while (PlayField.fieldchooser == true) { GameEngine.PlayFieldMenu(aktivspiller); }

                    Console.WriteLine(aktivspiller.name + " Trykk en tast for å starte ny runde");
                    GameEngine.NewRound();
                }
            }
        }
    }
}