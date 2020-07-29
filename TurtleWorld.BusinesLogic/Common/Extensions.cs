using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWorld.BusinesLogic.Enums;

namespace TurtleWorld.BusinesLogic.Common
{
    internal static class Extensions
    {
        internal static Directions TurnRight(this Directions d)
        {
            Directions res = Directions.Unknown;
            switch (d)
            {
                case Directions.North:
                    res = Directions.East;
                    break;
                case Directions.East:
                    res = Directions.South;
                    break;
                case Directions.South:
                    res = Directions.West;
                    break;
                case Directions.West:
                    res = Directions.North;
                    break;
                default:
                    throw new Exception($"Unsupported Direction value {d }");
            }
            return res;
        }
    }
}
