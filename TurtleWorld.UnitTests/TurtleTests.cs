using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleWorld.BusinesLogic.Common;
using TurtleWorld.BusinesLogic.Entities;
using TurtleWorld.BusinesLogic.Enums;

namespace TurtleWorld.UnitTests
{
    [TestClass]
    public class TurtleTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SettingTurtle_MissingBoard_Test()
        {
            Turtle t = new Turtle(null, new Point(0, 0));
            
        }

        private BoardSetUp Standard10x10GeneratedBoard
        {
            get => new BoardSetUp(new Point(10 - 1, 10 - 1), new Point[] { new Point(0, 0) });
        }

        [TestMethod]
        [ExpectedException(typeof(OutOfBoardException))]
        public void SettingTurtle_StartingPointOutside_Test()
        {
            Turtle t = new Turtle(Standard10x10GeneratedBoard, new Point(-1, 0));

        }

        [TestMethod]
        public void SettingTurtle_NonDefaultDirection_Test()
        {
            Turtle t = new Turtle(Standard10x10GeneratedBoard, new Point(1, 0), BusinesLogic.Enums.Directions.West);
            Assert.AreEqual(BusinesLogic.Enums.Directions.West, t.Orientation);

        }


        [TestMethod]
        [DataRow(5, 5, 5, 4, BusinesLogic.Enums.Directions.North)]
        [DataRow(5, 5, 6, 5, BusinesLogic.Enums.Directions.East)]
        [DataRow(5, 5, 5, 6, BusinesLogic.Enums.Directions.South)]
        [DataRow(5, 5, 4, 5, BusinesLogic.Enums.Directions.West)]
        public void SettingTurtle_Movement_Test(int fromX, int fromY, int afterX, int afterY, Directions initialDirection)
        {
            Turtle t = new Turtle(Standard10x10GeneratedBoard, new Point(fromX, fromY), initialDirection);
            t.Move();

            Assert.AreEqual(initialDirection, t.Orientation);
            Assert.AreEqual(t.CurrentPosition.X, afterX);
            Assert.AreEqual(t.CurrentPosition.Y, afterY);

        }

        [TestMethod]
        [DataRow(5, 5, 6, 5, BusinesLogic.Enums.Directions.North, Directions.East)]
        [DataRow(5, 5, 5, 6, BusinesLogic.Enums.Directions.East, Directions.South)]
        [DataRow(5, 5, 4, 5, BusinesLogic.Enums.Directions.South, Directions.West)]
        [DataRow(5, 5, 5, 4, BusinesLogic.Enums.Directions.West, Directions.North)]
        
        public void SettingTurtle_Movement_TurnAndMove_Test(int fromX, int fromY, int afterX, int afterY, 
            Directions initialDirection, Directions afterDirection)
        {
            Turtle t = new Turtle(Standard10x10GeneratedBoard, new Point(fromX, fromY), initialDirection);
            t.Rotate();
            t.Move();

            Assert.AreEqual(afterDirection, t.Orientation);
            Assert.AreEqual(t.CurrentPosition.X, afterX);
            Assert.AreEqual(t.CurrentPosition.Y, afterY);

        }
    }
}
