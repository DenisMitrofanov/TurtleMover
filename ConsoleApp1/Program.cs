using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWorld.Utils.Helpers;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = SettingsReader.ReadSettingsFromFile("game-settings.txt");
            var movements = MovementReader.ReadMovementsFromFile("moves.txt");
        }
    }
}
