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

                Player Bjørn = new Player("Bjørn");
                Player Andre = new Player("Andre");
            }
            TurnOn = true;
        }
        public void UpdateView()
        {
            View.UpdateView();
        }
        public void PlayGame() {
            UpdateView();
            Console.CursorVisible = true;
            while (GameOn)
            {
                //PlayField.bigCalcPotential();
              
                    foreach (var player in Player.PlayerList)
                    {
                    while (TurnOn)
                    {

                        GameEngine.TurnController();
            
                        UpdateView();
                    }
                }
                //Console.WriteLine("Hvor vil du plassere poengene dine?\n" +
                //                  "Skriv minst 2 av bokstavene på plassering");
                //var input = Console.ReadLine();
                //GameEngine.PlacePoints(input);

                //ny "velger"
                while (PlayField.fieldchooser == true) { GameEngine.PlayFieldMenu(); }

               
                Console.WriteLine("Trykk en tast for å starte ny runde");
                GameEngine.NewRound();
            }
        }
    }
}