using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWorld.BusinesLogic.Common;
using TurtleWorld.BusinesLogic.Enums;

namespace TurtleWorld.BusinesLogic.Interfaces
{
    public interface ITurtle
    {

        event TurtleStateEventHandler TurtleStateChanged;

        //Change state
        void Rotate();
        void Move();
    }


}
