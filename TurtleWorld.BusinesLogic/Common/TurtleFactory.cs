using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWorld.BusinesLogic.Entities;
using TurtleWorld.BusinesLogic.Enums;
using TurtleWorld.BusinesLogic.Interfaces;

namespace TurtleWorld.BusinesLogic.Common
{
    public static class TurtleFactory
    {
        public static ITurtle InitiateTurtleOnBoard((int DimX, int DimY) boardSize, 
                (int X, int Y) startingPoint, (int X, int Y) exit,
                IEnumerable<(int X, int Y)> mines,
            BoardModes boardMode = BoardModes.BouncingWalls,
            Directions direction = Directions.North)
        {
            IBoard board = new BoardSetUp(new Point(boardSize.DimX - 1, boardSize.DimY - 1),
                    new[] { new Point(exit) }, mode: boardMode);

            board.ReseedMines(mines);

            Turtle turtle = new Turtle(board, new Point(startingPoint), direction);

            return turtle;
        }
    }
}
