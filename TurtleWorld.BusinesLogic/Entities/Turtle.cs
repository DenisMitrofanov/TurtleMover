using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using TurtleWorld.BusinesLogic.Common;
using TurtleWorld.BusinesLogic.Enums;
using TurtleWorld.BusinesLogic.Interfaces;

namespace TurtleWorld.BusinesLogic.Entities
{
    internal class Turtle : ITurtle
    {
        

        private IBoard mineBoard;

        #region Turtle state

        Directions orientation;
        Point currentPosition;
        TurtleState state = TurtleState.Alive;


        public override string ToString()
        {
            return $"T is {State} facing {Orientation} at pos ({CurrentPosition}) on board ({mineBoard}) ";
        }

        public override int GetHashCode()
        {
            return new { orientation, currentPosition, state, mineBoard }.GetHashCode();
        }

        TurtleState State
        {
            get => this.state;
            set
            {
                this.state = value;
                OnTurtleState(newState: value);
            }
        }

        Directions Orientation
        {
            get => this.orientation;
            set
            {
                this.orientation = value;
                OnTurtleState(newOrientation: value);
            }
        }

        Point CurrentPosition
        {
            get => this.currentPosition;
            set
            {
                this.currentPosition = value;
                OnTurtleState(newPos: value);
            }
        }

        /// <summary>
        /// To handle atomic change of two state attributes
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="state"></param>
        void SetPositionAndState(Point pos, TurtleState state)
        {
            this.currentPosition = pos;
            this.state = state;
            OnTurtleState(newPos: pos, newState: state);
        }

        public event TurtleStateEventHandler TurtleStateChanged;

        void OnTurtleState(Directions? newOrientation = null, Point newPos = null, TurtleState? newState = null)
                         => TurtleStateChanged?.Invoke(this, new TurtleEventArgs());

        #endregion


        public Turtle(IBoard board) => this.mineBoard = board;


        /// <summary>
        /// Rotate right 90 degrees 
        /// </summary>
        public void Rotate()
        {
            ValidateCurrentState();
            orientation = orientation.TurnRight();
        }
        //orientation = (orientation ^ 2) & 15;
        
        public void Move()
        {
            ValidateCurrentState();
            Point pointToMoveTo = currentPosition.MoveTo(orientation);

            try
            {
                // depends on the board's setting the current possition on the edge of the board may change
                pointToMoveTo = mineBoard.ValidateAgainstBordes(pointToMoveTo);
            }
            catch(OutOfBoardException ex)
            {
                SetPositionAndState(pointToMoveTo, TurtleState.Dead);
                return;
            }

            // checking mines first, before exit 
            if (mineBoard.ProbeMine(pointToMoveTo))
                SetPositionAndState(pointToMoveTo, TurtleState.DeadFromMine);// Should we allow zombie mode here?
            else if (mineBoard.ProbeExit(pointToMoveTo))
                SetPositionAndState(pointToMoveTo, TurtleState.Escaped);
            else
                this.CurrentPosition = pointToMoveTo;
        }

        private void ValidateCurrentState()
        {
            if (TurtleState.Dead == this.State
                || TurtleState.DeadFromMine == this.State)
                throw new Exception("The turtle is dead already and cannot move!");

            if (TurtleState.Escaped == this.State)
                throw new Exception("The turtle has escaped the board and is not available");

            if (TurtleState.Alive != this.State
                && TurtleState.Zombie != this.State)
                throw new Exception("The turtle is in unnatural state");
        }
    }
}
