using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TurtleWorld.BusinesLogic.Enums;

namespace TurtleWorld.Utils.Helpers
{
    public static class SettingsReader
    {
        


        // Given a file containing the board size, starting point and direction, exit point and mines called“game-settings”
        // the structure of the file would be the following
        // line 1 : DX_int , DY_int         - Board size
        // line 2 : x_int  , y_int          - starting point
        // line 3 : North|East|South|West   - direction
        // line 4 : ex_int , ey_int         - exit point
        // line 5-N: mx_int , my_int         - a mine position


        public struct TurtleSetUpSettings
        {
            public (int DimX, int DimY) BoardSize;
            public (int X, int Y) StartingPoint;
            public Directions InitialOrientation;
            public (int X, int Y) ExitPoint;
            public IEnumerable<(int X, int Y)> Mines;
        }

 

        private static Lazy<Regex> coordinatesReg = new Lazy<Regex>( ()=>new Regex(@"(<X>\d+)\s+(<Y>\d+)", RegexOptions.Singleline | RegexOptions.Compiled));
        private static (int X, int Y) ReadPoint(string line)
        {
            var m = coordinatesReg.Value.Match(line);
            if (!m.Success)
                throw new ArgumentException($"Input line doesn't correpond to the expected format 'int_x, int_y', actual value is {line}");

            return (int.Parse(m.Groups["X"].Value), int.Parse(m.Groups["Y"].Value));
        }

        public static TurtleSetUpSettings ReadSettingsFromFile(string fileName)
        {
            using (StreamReader r = new StreamReader(fileName))
                return ReadSettings(r);
        }


        public static TurtleSetUpSettings ReadSettingsFromString(string content) => ReadSettings(new StringReader(content));
        public static TurtleSetUpSettings ReadSettings(TextReader stream)
        {
            TurtleSetUpSettings res = new TurtleSetUpSettings();
            string line;

            //board side 
            if (null == (line = stream.ReadLine()))
                throw new Exception("Stream reader is incomplete - board size line is missing");
            res.BoardSize = ReadPoint(line);

            //starting point
            if (null == (line = stream.ReadLine()))
                throw new Exception("Stream reader is incomplete - starting point line is missing");
            res.StartingPoint = ReadPoint(line);

            //direction
            if (null == (line = stream.ReadLine()))
                throw new Exception("Stream reader is incomplete - direction line is missing");
            res.InitialOrientation = (Directions)Enum.Parse(typeof(Directions), line, true); //it's ok to get an exception

            //exit point
            if (null == (line = stream.ReadLine()))
                throw new Exception("Stream reader is incomplete - exit point line is missing");
            res.ExitPoint = ReadPoint(line);

            //mines section
            res.Mines = ReadMines(stream);

            return res;
        }

        // laziness, although all mines are read eventually into memory in the BoardSetUp class anyway
        private static IEnumerable<(int X, int Y)> ReadMines(TextReader stream)
        {
            if (null == stream)
                throw new ArgumentNullException("stream");

            string line;

            while (null != (line = stream.ReadLine()))
            {
                yield return ReadPoint(line); ;
            }
        }



       
    }
}
