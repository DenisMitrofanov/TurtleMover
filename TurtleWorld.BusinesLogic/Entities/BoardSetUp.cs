using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWorld.BusinesLogic.Interfaces;
using TurtleWorld.BusinesLogic.Enums;

namespace TurtleWorld.BusinesLogic.Entities
{
    internal class BoardSetUp : IBoard
    {
        public readonly BoardModes BorderMode;

        private readonly Point BottomRight; //defines dimensions of the board

        // TODO: how often do we alter mines? cosider using Array instead
        private List<Point> mines = new List<Point>();

        private List<Point> exits = new List<Point>();

        public BoardSetUp(Point dimensions, IEnumerable<Point> exits, BoardModes mode = BoardModes.BouncingWalls)
        {
            this.BorderMode = mode;
            this.BottomRight = dimensions;

            this.exits = new List<Point>(exits);
            this.exits.Sort();
        }

        /// <summary>
        /// Clean and plant new mines on the board. Throws an exception if a mine doesn't fit boundaries 
        /// don't mind duplicates, keep everything in memory
        /// </summary>
        /// <param name="mines"></param>
        public void ReseedMines(IEnumerable<Point> mines)
        {
            this.mines = new List<Point>(mines);
            this.mines.Sort();
        }

        /// <summary>
        /// Is the given point mine? the mines are presorted 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool ProbeMine(Point p) => (0 <= mines.BinarySearch(p));

        public bool ProbeExit(Point p) => (0 <= exits.BinarySearch(p));
        

        public Point ValidateAgainstBordes(Point pointToMoveTo)
        {
            throw new NotImplementedException();
        }
    }
}
