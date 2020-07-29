using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleWorld.BusinesLogic.Enums
{
    public enum BoardModes
    {
        BouncingWalls = 1,// staying at the current position
        EarthMode, // passing the zero longitude/latitude means transferring to the opposite edge for the board
        DeadlyMoat // crossing borders means end of the journey
    }
}
