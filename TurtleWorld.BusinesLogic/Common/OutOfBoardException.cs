using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWorld.BusinesLogic.Entities;

namespace TurtleWorld.BusinesLogic.Common
{
    internal class OutOfBoardException : Exception
    {
        public OutOfBoardException(Point p, Point dimensions):base( $"Point {p} doesn't belong to board of ({dimensions.X}x{dimensions.Y})")
        {

        }
    }
}
