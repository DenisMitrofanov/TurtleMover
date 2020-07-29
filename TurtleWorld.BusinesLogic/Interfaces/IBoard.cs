using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWorld.BusinesLogic.Entities;

namespace TurtleWorld.BusinesLogic.Interfaces
{
    internal interface IBoard
    {
        void ReseedMines(IEnumerable<Entities.Point> mines);
        void ReseedMines(IEnumerable<(int X, int Y)> mines);


        bool ProbeMine(Entities.Point p);
        bool ProbeExit(Entities.Point p);

        /// <summary>
        /// Validate and amend(!!!) point depends on the board settings
        /// </summary>
        /// <param name="pointToMoveTo"></param>
        /// <returns></returns>
        Point ValidateAgainstBordes(Point pointToMoveTo);

        /// <summary>
        /// simple validation that the given point is inside the board
        /// </summary>
        /// <param name="pointToMoveTo"></param>
        /// <returns></returns>
        Point CheckIfIsInside(Point point);
    }
        
}
