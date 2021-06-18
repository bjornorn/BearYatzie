using System;

namespace BearDiceGame
{
    public class DiceArt
    {
        //public static string[] ThreeDiceA = { " -----  ", "|O----| ", "|--O--| ", "|----O| ", " -----  " };
        //public static string[] FourDiceA = { " -----  ", "|O---O| ", "|-----| ", "|O---O| ", " -----  " };


        public static void OneDice(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("┌-----┐  ");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("|     | ");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("|  O  | ");
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("|     | ");
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("└-----┘ ");
        }
        public static void TwoDice(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("┌-----┐  ");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("|O    | ");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("|     | ");
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("|    O| ");
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("└-----┘ ");
        }
        public static void ThreeDice(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("┌-----┐  ");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("|O    | ");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("|  O  | ");
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("|    O| ");
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("└-----┘ ");
        }
        public static void FourDice(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("┌-----┐  ");
            Console.SetCursorPosition(x, y+1);
            Console.WriteLine("|O   O| ");
            Console.SetCursorPosition(x, y+2);
            Console.WriteLine("|     | ");
            Console.SetCursorPosition(x, y+3);
            Console.WriteLine("|O   O| ");
            Console.SetCursorPosition(x, y+4);
            Console.WriteLine("└-----┘ ");
        }

        public static void FiveDice(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("┌-----┐  ");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("|O   O| ");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("|  O  | ");
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("|O   O| ");
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("└-----┘ ");
        }

        public static void SixDice(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("┌-----┐ ");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("|O   O| ");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("|O   O| ");
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("|O   O| ");
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("└-----┘ ");
        }
        public static void QuestionDice(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("┌-----┐ ");
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("|? ? ?| ");
            Console.SetCursorPosition(x, y + 2);
            Console.WriteLine("|? ? ?| ");
            Console.SetCursorPosition(x, y + 3);
            Console.WriteLine("|? ? ?| ");
            Console.SetCursorPosition(x, y + 4);
            Console.WriteLine("└-----┘ ");
        }
    }
}