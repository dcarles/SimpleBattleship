using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Model
{
    public enum ShipDirection
    {
        Horizontal = 0,
        Vertical
    }
    
    public abstract class Ship
    {
        public Position OriginPosition { get; set; }
        public virtual int Length
        {
            get { return 0; }
        }

        public ShipDirection Direction { get; private set; }
        private int _hits;
        
        protected Ship(ShipDirection direction)
        {
            Direction = direction;
        }

        public bool IsHit(Position position)
        {
            var x = OriginPosition.X;
            var y = OriginPosition.Y;

            for (var j = 0; j < Length; j++)
            {
                if (position.X == x && position.Y == y)
                {
                    return true;
                }

                if (Direction == ShipDirection.Horizontal)
                {
                    y++;
                }
                else
                {
                    x++;
                }
            }

            return false;
        }
        
        public void Hit()
        {
            _hits++;
        }

        public bool IsSunk()
        {
            return _hits == Length;
        }

    }


}
