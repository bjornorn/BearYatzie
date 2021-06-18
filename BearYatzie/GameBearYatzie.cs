using System;
using System.Security.Cryptography.X509Certificates;

namespace BearDiceGame
{
    public class GameBearYatzie : View
    {
        public static bool GameOn = false;
        public static bool TurnOn = false;
        
        public static int turnCounter = 6;

        public void UpdateView()
        {
            base.UpdateView();
        }


        public GameBearYatzie()
        {
            GameOn = true;
            for (int i = 0; i < 5; i++) new Dice(i);
            int id = 1;
            foreach (var felt in BoardField.smallfields)
            {
                new BoardField(felt, id);
                id++;
            }
           
            TurnOn = true;

        }
        
        public void PlayGame() {
            UpdateView();
            while (GameOn)
            {
               
                while (TurnOn)
                {
                    GameEngine.TurnController();
                    UpdateView();
                } 






                Console.WriteLine("Hvor vil du plassere poengene dine? \nSkriv minst 2 av bokstavene på plassering");
                var input = Console.ReadLine();
                GameEngine.PlacePoints(input);
                UpdateView();
                Console.WriteLine("Trykk en tast for å starte ny runde");
                //Console.ReadLine();
                GameEngine.NewRound();
                

            }
        }



    }

}