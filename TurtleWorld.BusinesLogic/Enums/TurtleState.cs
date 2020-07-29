using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleWorld.BusinesLogic.Enums
{
    internal enum TurtleState
    {
        // managed to find the exit
        Escaped = -1,

        Alive = 0,
        Dead,
        DeadFromMine,

        Zombie

    }
}
