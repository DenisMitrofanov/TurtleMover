using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleWorld.BusinesLogic.Common
{
    public delegate void TurtleStateEventHandler(object sender, TurtleEventArgs e);
    public class TurtleEventArgs : EventArgs
    {

    }
}
