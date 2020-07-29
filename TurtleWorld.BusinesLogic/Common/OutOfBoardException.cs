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
        public OutOfBoardException(Point p):base()
        {

        }
    }
}
