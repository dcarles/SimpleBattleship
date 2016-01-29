using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Logic;
using Battleships.Model;

namespace Battleships
{
    public class ConsoleInterface : IUserInterface
    {

        public string GetUserInput()
        {
            var input = Console.ReadLine();
            return input;
        }

        public void RenderMessage(string message)
        {
            Console.WriteLine(message);
        }

    }
}
