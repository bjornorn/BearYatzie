namespace BearYatzie
{
    public class GameMiniDice
    {
        public static bool GameOn = false;
        public static int rollCounter = 0;

        public GameMiniDice()
        {
            GameOn = true;
            for (int i = 0; i < 5; i++) new Dice(i);
        }

    }
}