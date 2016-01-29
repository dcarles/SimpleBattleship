using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Battleships.Model;

namespace Battleships.Logic
{
    public enum LastPlay
    {
        Init,
        Miss,
        Hit,
        Sunk,
        AlreadyHit,
        Error
    }

    public class GameProcessor
    {
        private readonly Board _board;
        private readonly IUserInterface _userInterface;
        private LastPlay _lastPlay;


        public GameProcessor(IBoardFactory boardFactory, IUserInterface userInterface)
        {
            this._board = boardFactory.Get();
            this._userInterface = userInterface;
            _lastPlay = LastPlay.Init;
        }

        public void Play()
        {
            _userInterface.RenderMessage("Please input your shoot coordinates (x, y) in format [Letter][Number], e.g. B3");
            _userInterface.RenderMessage("Board go from A to " + Position.GetLetterFromX(_board.Width + 64) + " and from 0 to " + (_board.Height - 1));

            while (true)
            {
                var coordinates = _userInterface.GetUserInput();

                if (!AreValidCoordinates(coordinates))
                {
                    _lastPlay = LastPlay.Error;
                    _userInterface.RenderMessage(GetLastPlayMessage());
                    _userInterface.RenderMessage("Your coordinates are invalid. Please make sure is in the format [Letter][Number], e.g A7");
                    _userInterface.RenderMessage("Board go from A to " + Position.GetLetterFromX(_board.Width + 64) + " and from 0 to " +  (_board.Height - 1));
                    continue;
                }

                var shootPosition = new Position(coordinates);


                if (_board.IsCoordinateAlreadyUsed(shootPosition))
                {
                    _lastPlay = LastPlay.AlreadyHit;
                    _userInterface.RenderMessage(GetLastPlayMessage());
                    continue;
                }


                var cellStatus = _board.ProcessShipHit(shootPosition);

                switch (cellStatus)
                {
                    case CellStatus.Hit:
                        this._lastPlay = LastPlay.Hit;
                        break;
                    case CellStatus.Sunk:
                        this._lastPlay = LastPlay.Sunk;
                        break;
                    case CellStatus.Miss:
                        this._lastPlay = LastPlay.Miss;
                        break;
                    case CellStatus.Invalid:
                    default:
                        this._lastPlay = LastPlay.Error;
                        break;
                }

                _userInterface.RenderMessage(GetLastPlayMessage());


                if (_board.AreAllShipsSunk())
                {
                    ProcessGameEnd();
                    break;
                }


            }

        }
        
        private bool AreValidCoordinates(string coordinates)
        {
            int y;
            return coordinates.Length >= 2 && coordinates.Length <= 3 && char.IsLetter(coordinates[0]) &&
                Position.GetXFromLetter(coordinates[0]) >= 0 &&
                int.TryParse(coordinates.Substring(1), out y) &&
                _board.IsCoordinateInside(coordinates[0],y);
        }
        
        private string GetLastPlayMessage()
        {
            switch (_lastPlay)
            {
                case LastPlay.Hit:
                    return "You hit a ship!";
                case LastPlay.Miss:
                    return "You missed!";
                case LastPlay.AlreadyHit:
                    return "Sorry you already hit that cell";
                case LastPlay.Sunk:
                    return "You sunk a ship!";
                case LastPlay.Error:
                    return "Sorry There was an error: ";
                case LastPlay.Init:
                default:
                    return "";
            }
        }

        private void ProcessGameEnd()
        {
            _userInterface.RenderMessage("You won! You sunk all the ships!");
            _userInterface.RenderMessage("Press any key to exit");
            _userInterface.GetUserInput();
        }
    }
}
