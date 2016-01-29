using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Model
{
    public class Battleship : Ship
    {
        public Battleship(ShipDirection direction): base(direction){}

        public override int Length
        {
            get
            {
                return 5;
            }
        }

    }
}
