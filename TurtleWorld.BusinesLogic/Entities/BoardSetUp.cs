using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWorld.BusinesLogic.Interfaces;
using TurtleWorld.BusinesLogic.Enums;
using TurtleWorld.BusinesLogic.Common;

namespace TurtleWorld.BusinesLogic.Entities
{
    internal class BoardSetUp : IBoard
    {
        public readonly BoardModes BoardMode;

        private readonly Point BottomRight; //defines dimensions of the board

        // TODO: how often do we alter mines? cosider using Array instead
        private List<Point> mines = new List<Point>();

        private List<Point> exits = new List<Point>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dimensions">the furthest point in the board, note that axes start with zero index, so for an N-dimension 
        /// this param will be N-1 </param>
        /// <param name="exits">can be zero to </param>
        /// <param name="mode"></param>
        public BoardSetUp(Point dimensions, IEnumerable<Point> exits, BoardModes mode = BoardModes.BouncingWalls)
        {
            this.BoardMode = mode;

            if (0 > dimensions.Y || 0 > dimensions.X)
                throw new ArgumentException("Board's dimension must be greater than zero", "dimensions");

            this.BottomRight = dimensions;

            this.exits = new List<Point>(exits);
            this.exits.ForEach(p => CheckIfIsInside(p));
            this.exits.Sort();
        }

        public override string ToString()
        {
            return $"B of ({BottomRight.X + 1}x{BottomRight.Y + 1}) exit(s) in ({exits.FirstOrDefault()}) with {mines.Count} mine(s)";
        }

        /// <summary>
        /// Clean and plant new mines on the board. Throws an exception if a mine doesn't fit boundaries 
        /// don't mind duplicates, keep everything in memory
        /// </summary>
        /// <param name="mines"></param>
        public void ReseedMines(IEnumerable<Point> mines)
        {
            this.mines = new List<Point>(mines);
            this.mines.ForEach(p => CheckIfIsInside(p));
            this.mines.Sort();
        }

        public void ReseedMines(IEnumerable<(int X, int Y)> mines)
        {
            this.mines = mines.Select(m => CheckIfIsInside(new Point(m) ) ).ToList<Point>();
            this.mines.Sort();
        }

        /// <summary>
        /// Is the given point mine? the mines are presorted 
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool ProbeMine(Point p) => (0 <= mines.BinarySearch(p));

        public bool ProbeExit(Point p) => (0 <= exits.BinarySearch(p));

        /// <summary>
        /// only handles the very edge of the board, every location far away from the board's borders is considered deadly
        /// </summary>
        /// <param name="pointToMoveTo"></param>
        /// <returns></returns>
        public Point ValidateAgainstBordes(Point pointToMoveTo)
        {
            Point res = pointToMoveTo;
            if (BoardModes.BouncingWalls == this.BoardMode)
            {
                if (-1 == pointToMoveTo.X)
                    res = new Point(0, pointToMoveTo.Y);

                if (this.BottomRight.X + 1 == pointToMoveTo.X)
                    res = new Point(this.BottomRight.X, pointToMoveTo.Y);

                if (-1 == pointToMoveTo.Y)
                    res = new Point(pointToMoveTo.X, 0);

                if (this.BottomRight.Y + 1 == pointToMoveTo.Y)
                    res = new Point(pointToMoveTo.X, this.BottomRight.Y);

            }

            // merry-go-round mode
            if (BoardModes.EarthMode == this.BoardMode)
            {
                if (-1 == pointToMoveTo.X)
                    res = new Point(this.BottomRight.X, pointToMoveTo.Y);

                if (this.BottomRight.X + 1 == pointToMoveTo.X)
                    res = new Point(0, pointToMoveTo.Y);

                if (-1 == pointToMoveTo.Y)
                    res = new Point(pointToMoveTo.X, this.BottomRight.Y);

                if (this.BottomRight.Y + 1 == pointToMoveTo.Y)
                    res = new Point(pointToMoveTo.X, 0);
            }

            // outside of legitimate borders check
            CheckIfIsInside(pointToMoveTo);

            return res;
        }

        public Point CheckIfIsInside(Point point)
        {
            if (0 > point.X || this.BottomRight.X < point.X
                || 0 > point.Y || this.BottomRight.Y < point.Y
                )
                throw new OutOfBoardException(point, this.BottomRight);

            return point;
        }
    }
}
