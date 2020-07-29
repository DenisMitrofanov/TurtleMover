using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWorld.BusinesLogic.Entities;

namespace TurtleWorld.BusinesLogic.Interfaces
{
    public interface IBoard
    {
        void ReseedMines(IEnumerable<Entities.Point> mines);

        bool ProbeMine(Entities.Point p);
        bool ProbeExit(Entities.Point p);
        Point ValidateAgainstBordes(Point pointToMoveTo);
    }
}
