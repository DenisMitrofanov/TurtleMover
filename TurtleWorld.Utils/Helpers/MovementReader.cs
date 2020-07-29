using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleWorld.Utils.Helpers
{
    /// <summary>
    /// as of the spec "a file containing one or more move sequences called “moves”"
    /// and "Turtle actions can be either a ​move​ (m) one tile forward or ​rotate​ (r) 90 degrees to the right"
    /// the file is a text file
    /// each line contains one and only one movement
    /// either M/Move or r/Rotate, case insensitive
    /// </summary>
    public static class MovementReader
    {
        private static string[] MoveNames = new string[] { "m", "move" };
        private static string[] RotateNames = new string[] { "r", "rotate", "turn" };
        public static IEnumerable<MovementSteps> ReadMovements(TextReader stream)
        {
            if (null == stream)
                throw new ArgumentNullException("stream");

            string line;

            while (null != (line = stream.ReadLine()))
            {
                yield return ParseMovement(line.Trim());
            }
        }

        private static MovementSteps ParseMovement(string line)
        {
            //the Names arrays are tiny, contains will do perfectly well in terms of performance
            if (MoveNames.Contains(line, StringComparer.InvariantCultureIgnoreCase))
                return MovementSteps.Move;
            else if (RotateNames.Contains(line, StringComparer.InvariantCultureIgnoreCase))
                return MovementSteps.Rotate;

            throw new ArgumentException($"cannot recognize move, expected m/r, actual is {line}", line);
        }

        public static IEnumerable<MovementSteps> ReadMovementsFromFile(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
                return ReadMovements(r);
        }

        public static IEnumerable<MovementSteps> ReadMovementsFromString(string content) => ReadMovements( new StringReader(content));
        
    }

    public enum MovementSteps
    {
        Move,
        Rotate
    }
}
