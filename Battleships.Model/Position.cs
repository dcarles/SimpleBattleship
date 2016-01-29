using System;

namespace Battleships.Model
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }


        public Position(string coordinates)
        {
            this.X = GetXFromLetter(coordinates[0]);
            this.Y = int.Parse(coordinates.Substring(1));
        }

        public Position(int row, int col)
        {
            this.X = row;
            this.Y = col;
        }

        public static int GetXFromLetter(char rowChar)
        {
            return char.ToUpper(rowChar) - 65;//index == 0
        }

        public static char GetLetterFromX(int x)
        {
            return (char)x;
        }
    }
}