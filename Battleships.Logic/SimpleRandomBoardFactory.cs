using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Model;

namespace Battleships.Logic
{
    public class SimpleRandomBoardFactory: IBoardFactory
    {
        private readonly Dictionary<Type, int> _shipsType;
        private readonly Random _random;


        public SimpleRandomBoardFactory()
        {
            _shipsType = new Dictionary<Type, int>
            {
                { typeof(Battleship), 1},
                { typeof(Destructor), 2}
            };

            _random = new Random();
        }


        public Board Get()
        {
            var board = new Board(10, 10);
            this.AddShips(board);
            return board;
        }
        
        private void AddShips(Board board)
        {
            var shipFactory = new ShipFactory();

            foreach (var shipType in this._shipsType)
            {
                for (var i = 0; i < shipType.Value; i++)
                {
                    var direction = this.GetRandomShipDirection();
                    var ship = shipFactory.Get(shipType.Key.Name, direction);
                    ship.OriginPosition = this.GetRandomShipPosition(direction, ship.Length, board.Width, board.Height);

                    while (board.ShipsOverlap(ship))
                    {
                        ship.OriginPosition = this.GetRandomShipPosition(direction, ship.Length, board.Width, board.Height);
                    }
                  
                    board.PlaceShip(ship);
                }
            }
        }

        private ShipDirection GetRandomShipDirection()
        {
            return _random.Next(0, 2) == 0 ? ShipDirection.Vertical : ShipDirection.Horizontal;
        }

        private Position GetRandomShipPosition(ShipDirection direction, int shipLength, int rowsCount, int colsCount)
        {
            int x;
            int y;

            if (direction == ShipDirection.Horizontal)
            {
                x = _random.Next(0, rowsCount);
                y = _random.Next(0, colsCount - shipLength);
            }
            else
            {
                x = _random.Next(0, rowsCount - shipLength);
                y = _random.Next(0, colsCount);
            }

            return new Position(x, y);
        }

    }
}
