using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships.Model
{

    public enum CellStatus
    {
        Empty = 0,
        Ship = 1,
        Hit = 2,
        Miss = 3,
        Sunk = 4,
        Invalid = 6
    }

    public class Board
    {
        public readonly int Height;
        public readonly int Width;
        private readonly int[,] _cells;
        private List<Ship> Ships { get; set; }


        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            this._cells = new int[Width, Height];
            this.Ships = new List<Ship>();
        }

        public void PlaceShip(Ship ship)
        {
            var shipX = ship.OriginPosition.X;
            var shipY = ship.OriginPosition.Y;

            for (var i = 0; i < ship.Length; i++)
            {
                this.SetCell(shipX, shipY, CellStatus.Ship);

                if (ship.Direction == ShipDirection.Vertical)
                {
                    shipX++;
                }
                else
                {
                    shipY++;
                }
            }

            Ships.Add(ship);
        }

        private CellStatus GetCell(Position position)
        {
            return (CellStatus)this._cells[position.X, position.Y];
        }

        private CellStatus GetCell(int row, int col)
        {
            return (CellStatus)this._cells[row, col];
        }

        private void SetCell(int x, int y, CellStatus value)
        {
            this._cells[x, y] = (int)value;
        }

        private bool IsCoordinateInside(int x, int y)
        {
            return IsInRange(x, this.Width) && IsInRange(y, this.Height);
        }

        private static bool IsInRange(int value, int max)
        {
            return value >= 0 && value < max;
        }

        public bool ShipsOverlap(Ship ship)
        {
            var shipX = ship.OriginPosition.X;
            var shipY = ship.OriginPosition.Y;

            for (var i = 0; i < ship.Length; i++)
            {
                if (this.GetCell(shipX, shipY) == CellStatus.Ship)
                {
                    return true;
                }

                if (ship.Direction == ShipDirection.Vertical)
                {
                    shipX++;
                }
                else
                {
                    shipY++;
                }
            }

            return false;
        }

        public bool IsCoordinateAlreadyUsed(Position shotPosition)
        {
            var cell = this.GetCell(shotPosition);
            return cell == CellStatus.Hit || cell == CellStatus.Miss;
        }

        public CellStatus ProcessShipHit(Position shotPosition)
        {
            if (IsCoordinateAlreadyUsed(shotPosition)) return CellStatus.Invalid;

            var affectedShip = Ships.FirstOrDefault(ship => ship.IsHit(shotPosition));

            if (affectedShip == null)
            {
                this.SetCell(shotPosition.X, shotPosition.Y, CellStatus.Miss);
                return CellStatus.Miss;
            }

            affectedShip.Hit();

            if (affectedShip.IsSunk())
            {
                this.SetCell(shotPosition.X, shotPosition.Y, CellStatus.Hit);
                return CellStatus.Sunk;
            }
            else
            {
                this.SetCell(shotPosition.X, shotPosition.Y, CellStatus.Hit);
                return CellStatus.Hit;
            }

        }

        public bool AreAllShipsSunk()
        {
            return this.Ships.All(t => t.IsSunk());
        }

        public bool IsCoordinateInside(char c, int y)
        {
            var x = Position.GetXFromLetter(c);
            return IsCoordinateInside(x, y);
        }

    }
}
