using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Model
{
    public class Destructor: Ship
    {
        public Destructor(ShipDirection direction): base(direction){ }

        public override int Length
        {
            get
            {
                return 4;
            }
        }
    }
}
