using System;

namespace BearDiceGame
{
    class Program
    {
        static void Main(string[] args)
        {

            //View.Welcome();
            //new Game();
            var newGame = new GameBearYatzie();
            newGame.PlayGame();
            //while (GameBearYatzie.GameOn)
            //{
            //    GameEngine.TurnController();
            //}

            //while (GameMiniDice.GameOn)
            //{
            //    View.ChooseDice();
            //    Console.WriteLine(View.ShowCounter());
            //    Console.WriteLine(View.ShowSum());
            //    var input = Console.ReadKey();
                
            //}

        }
    }
}
