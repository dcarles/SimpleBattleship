using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Logic;

namespace Battleships
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var simpleRandomBoardFactory = new SimpleRandomBoardFactory();
            var consoleInterface = new ConsoleInterface();
            var game = new GameProcessor(simpleRandomBoardFactory, consoleInterface);
            game.Play();
            Environment.Exit(0);
        }
    }
}
