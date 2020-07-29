using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWorld.BusinesLogic.Enums;

namespace TurtleWorld.BusinesLogic.Entities
{
    // TODO cosnider using a structure 


    /// <summary>
    /// X 0 ---->
    /// Y 0
    ///     |
    ///     |
    ///     |
    ///     Y
    /// </summary>
    public class Point : Tuple<int, int>
    {
        public Point(int x, int y):base(x, y)
        {

        }
        public int X
        {
            get => this.Item1; 
        }

        public int Y
        {
            get => this.Item2;
        }

        public override string ToString()
        {
            return $"{X}/{Y}";
        }

        internal Point MoveTo(Directions d)
        {
            Point res;// = (Point) this.MemberwiseClone();
            switch (d)
            {
                case Directions.North:
                    res = new Point(X, Y -1);
                    break;
                case Directions.East:
                    res = new Point(X + 1, Y);
                    break;
                case Directions.South:
                    res = new Point(X, Y + 1);
                    break;
                case Directions.West:
                    res = new Point(X - 1, Y);
                    break;
                default:
                    throw new Exception($"Unsupported Direction value {d }");
            }
            return res;
        }
    }
}
