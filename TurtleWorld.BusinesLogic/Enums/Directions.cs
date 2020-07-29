using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleWorld.BusinesLogic.Enums
{
    //[Flags] - we won't support NorthEasth for the time being
    public enum Directions : byte
    {
        Unknown = 255,
        North = 1,
        East  = 2,
        South = 4,
        West  = 8
    }
}
