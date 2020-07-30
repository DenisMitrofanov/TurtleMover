using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TurtleWorld.BusinesLogic.Common;
using TurtleWorld.BusinesLogic.Entities;
using TurtleWorld.BusinesLogic.Enums;

namespace TurtleWorld.UnitTests
{
    [TestClass]
    public class BoardTest
    {
        [TestMethod]

        [DataRow(12, 3, 4, 2, BoardModes.BouncingWalls)]
        [DataRow(5, 12233, 4, 2, BoardModes.BouncingWalls)]
        [DataRow(534, 1, 4, 0, BoardModes.BouncingWalls)]
        [DataRow(12, 3, 4, 2, BoardModes.DeadlyMoat)]
        [DataRow(12, 3, 4, 2, BoardModes.EarthMode)]
        public void SettingBoard_PositiveDimensions_Test(int dX, int dY, int exitX, int exitY, BoardModes mode)
        {
            BoardSetUp probe = new BoardSetUp(new Point(dX, dY), new Point[] { new Point(exitX, exitY) }, mode);
            Assert.AreEqual(exitX, probe.Exit.X);
            Assert.AreEqual(exitY, probe.Exit.Y);

            Assert.AreEqual(dX, probe.BottomRight.X);
            Assert.AreEqual(dY, probe.BottomRight.Y);

            Assert.AreEqual(mode, probe.BoardMode);
        }


        [TestMethod]
        [DataRow(12, 0)]
        [DataRow(0, 0)]
        [DataRow(12, -1)]
        [DataRow(-123, 0)]
        [DataRow(-9, -120)]
        [ExpectedException(typeof(ArgumentException), "Board's dimension must be greater than zero")]
        public void SettingBoard_Illegal_Dimensions_Test(int dX, int dY)
        {
            BoardSetUp probe = new BoardSetUp(new Point(dX, dY), new Point[] { new Point(0, 0) });

        }

        [TestMethod]
        [DataRow(2, 2, 4, 2)]
        [DataRow(1, 1, 4, 2)]
        [DataRow(1, 1, -2, 2)]
        [DataRow(1, 1, -2, -2)]
        [DataRow(1, 1, 0, -1)]
        [ExpectedException(typeof(OutOfBoardException), AllowDerivedTypes = true)]
        public void SettingBoard_ExistOutside_Test(int dX, int dY, int exitX, int exitY)
        {
            BoardSetUp probe = new BoardSetUp(new Point(dX, dY), new Point[] { new Point(exitX, exitY) });

        }


        private BoardSetUp Get10x10Board(BoardModes mode)
        {
            return new BoardSetUp(new Point(10 - 1, 10 - 1), new Point[] { new Point(0, 0) }, mode);
        }

        [TestMethod]
        [DataRow(1, 2, 1, 2)]
        [DataRow(0, 0, 0, 0)]
        [DataRow(9, 9, 9, 9)]

        [DataRow(-1, 5, 0, 5)]
        [DataRow(5, -1, 5, 0)]
        [DataRow(10, 5, 9, 5)]
        [DataRow(5, 10, 5, 9)]

        public void ValidateAgainstBordes_BouncingWalls_Test(int fromX, int fromY, int afterX, int afterY)
        {

            BoardSetUp board = Get10x10Board(BoardModes.BouncingWalls);
            Point calculatedPoint = board.ValidateAgainstBordes(new Point(fromX, fromY));
            Assert.AreEqual(afterX, calculatedPoint.X);
            Assert.AreEqual(afterY, calculatedPoint.Y);

        }

        [TestMethod]
        [DataRow(1, 2, 1, 2)]
        [DataRow(0, 0, 0, 0)]
        [DataRow(9, 9, 9, 9)]

        [DataRow(-1, 5, 9, 5)]
        [DataRow(5, -1, 5, 9)]
        [DataRow(10, 5, 0, 5)]
        [DataRow(5, 10, 5, 0)]

        public void ValidateAgainstBordes_EarthMode_Test(int fromX, int fromY, int afterX, int afterY)
        {
            BoardSetUp board = Get10x10Board(BoardModes.EarthMode);
            Point calculatedPoint = board.ValidateAgainstBordes(new Point(fromX, fromY));
            Assert.AreEqual(afterX, calculatedPoint.X);
            Assert.AreEqual(afterY, calculatedPoint.Y);

        }



        [TestMethod]
        [DataRow(1, 2, 1, 2)]
        [DataRow(0, 0, 0, 0)]
        [DataRow(9, 9, 9, 9)]

        public void ValidateAgainstBordes_DeadlyMoat_Inside_Test(int fromX, int fromY, int afterX, int afterY)
        {
            BoardSetUp board = Get10x10Board(BoardModes.DeadlyMoat);
            Point calculatedPoint = board.ValidateAgainstBordes(new Point(fromX, fromY));
            Assert.AreEqual(afterX, calculatedPoint.X);
            Assert.AreEqual(afterY, calculatedPoint.Y);

        }


        [TestMethod]
        [DataRow(-1, 5, 9, 5)]
        [DataRow(5, -1, 5, 9)]
        [DataRow(10, 5, 0, 5)]
        [DataRow(5, 10, 5, 0)]
        [ExpectedException(typeof(OutOfBoardException))]
        public void ValidateAgainstBordes_DeadlyMoat_Outside_Test(int fromX, int fromY, int afterX, int afterY)
        {
            BoardSetUp board = Get10x10Board(BoardModes.DeadlyMoat);
            Point calculatedPoint = board.ValidateAgainstBordes(new Point(fromX, fromY));
        }

        [TestMethod]
        
        public void MineSeeding_NoMines_Test()
        {
            BoardSetUp board = Get10x10Board(BoardModes.BouncingWalls);
            board.ReseedMines( new Point[] { });

            board.ReseedMines(new (int X, int Y)[] { });


        }

        [TestMethod]

        public void MineSeeding_GoodMines_Test()
        {
            BoardSetUp board = Get10x10Board(BoardModes.BouncingWalls);

            board.ReseedMines(new (int X, int Y)[] { (1, 1), (2, 2), (1, 2) });
        }


        [TestMethod]

        public void MineSeeding_GoodMinesDuplicates_Test()
        {
            BoardSetUp board = Get10x10Board(BoardModes.BouncingWalls);

            board.ReseedMines(new (int X, int Y)[] { (1, 1), (2, 2), (1, 2), (1, 1), (2, 2), (1, 2) });
        }

        [TestMethod]
        [ExpectedException(typeof(OutOfBoardException))]
        public void MineSeeding_OutsideOfBoard_Test()
        {
            BoardSetUp board = Get10x10Board(BoardModes.BouncingWalls);

            board.ReseedMines(new (int X, int Y)[] { (1, 1), (-1, 2), (1, 2), (1, 1), (2, 2), (1, 2) });
        }

        [TestMethod]
        public void Mine_ProbingRealMine_Test()
        {

            BoardSetUp board = Get10x10Board(BoardModes.BouncingWalls);

            board.ReseedMines(new (int X, int Y)[] { (1, 1), (1, 2), (1, 2), (1, 1), (2, 2), (1, 2) });
            Assert.IsTrue(board.ProbeMine(new Point(1, 1)));
            Assert.IsFalse(board.ProbeMine(new Point(9, 9)));
        }

        [TestMethod]
        public void Mine_ProbingMine_NoMines_Test()
        {

            BoardSetUp board = Get10x10Board(BoardModes.BouncingWalls);
            Assert.IsFalse(board.ProbeMine(new Point(1, 1)));
            Assert.IsFalse(board.ProbeMine(new Point(9, 9)));
        }

        [TestMethod]
        [ExpectedException(typeof(OutOfBoardException))]
        public void Mine_ProbingMine_OutsideBorder_Test()

        {

            BoardSetUp board = Get10x10Board(BoardModes.BouncingWalls);
            board.ProbeMine(new Point(-1, 1));

        }

        [TestMethod]
        public void MineSeeding_ProbingExis_Test()
        {

            BoardSetUp board = Get10x10Board(BoardModes.BouncingWalls);

            Assert.IsTrue(board.ProbeExit(new Point(0, 0)));
            Assert.IsFalse(board.ProbeExit(new Point(9, 9)));
        }

        [TestMethod]
        [ExpectedException(typeof(OutOfBoardException))]
        public void MineSeeding_ProbingExit_InvalidPoint_Test()
        {

            BoardSetUp board = Get10x10Board(BoardModes.BouncingWalls);

            board.ProbeExit(new Point(555, 0));
            
        }
    }
}
