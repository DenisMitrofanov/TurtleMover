using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWorld.BusinesLogic.Common;
using TurtleWorld.BusinesLogic.Interfaces;
using TurtleWorld.Utils.Helpers;

namespace ConsoleApp1
{
    class Program
    {
        static int Main(string[] args)
        {
            // Test if input arguments were supplied.
            if (args.Length != 2 || args.Contains("?"))
            {
                Console.WriteLine("Wrong Number of arguments. Usage: TurtleChallenge <game-settings> <moves>");
                return 1;
            }

            string settingsFileName = args[0];//"game-settings.txt"
            string movesFileName = args[1];//"moves.txt"

            var settings = SettingsReader.ReadSettingsFromFile(settingsFileName);
            var movements = MovementReader.ReadMovementsFromFile(movesFileName);

            var turtle = TurtleFactory.InitiateTurtleOnBoard(boardSize: settings.BoardSize,
                    startingPoint: settings.StartingPoint,
                    exit: settings.ExitPoint,
                    mines: settings.Mines,
                    boardMode: TurtleWorld.BusinesLogic.Enums.BoardModes.BouncingWalls,
                    direction: settings.InitialOrientation);
            int sequence = 1;
            //turtle.TurtleStateChanged += (obj, events) =>
            //{

            //    Console.WriteLine($"Sequence {sequence++} : {obj}");
            //};

            if (null != movements)
                foreach (var m in movements)
                {
                    DoAction(turtle, m);
                    Console.WriteLine($"Sequence {sequence++}: {m} : {turtle}");
                }

            return 0;
        }

        private static void DoAction(ITurtle turtle, MovementSteps m)
        {
            switch (m)
            {
                case MovementSteps.Move:
                    turtle.Move();
                    break;
                case MovementSteps.Rotate:
                    turtle.Rotate();
                    break;
                default:
                    throw new ArgumentException($"Unsupported movement type {m}");
            }
        }
    }
}
