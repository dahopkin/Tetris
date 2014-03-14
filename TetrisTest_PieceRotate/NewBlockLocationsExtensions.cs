using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public static class NewBlockLocationsExtensions
    {
        // The idea for this length function comes from stack overflow as well.
        public static int Length() { return Enum.GetNames(typeof(NewBlockLocations)).Length; }

        public static Point ReturnPoint(NewBlockLocations self, Block block)
        {
            Point pointToReturn = new Point(0,0);
            switch (self) { 
                case NewBlockLocations.Right:
                    pointToReturn = block.NewBlockRight;
                    break;
                case NewBlockLocations.Top_Right:
                    pointToReturn = block.NewBlockTopRight;
                    break;
                case NewBlockLocations.Top:
                    pointToReturn = block.NewBlockTop;
                    break;
                case NewBlockLocations.Top_Left:
                    pointToReturn = block.NewBlockTopLeft;
                    break;
                case NewBlockLocations.Left:
                    pointToReturn = block.NewBlockLeft;
                    break;
                case NewBlockLocations.Bottom_Left:
                    pointToReturn = block.NewBlockBottomLeft;
                    break;
                case NewBlockLocations.Bottom:
                    pointToReturn = block.NewBlockBottom;
                    break;
                case NewBlockLocations.Bottom_Right:
                    pointToReturn = block.NewBlockBottomRight;
                    break;
            } // end switch 
            return pointToReturn;
        }

        public static Point RotatedCCWPoint(this NewBlockLocations self, NewBlockLocations originalLocation,
            Block block, int rotationIndex)
        {
            int currentNewBlockPointIndex = (int)originalLocation;
            int rotatedCCWNewBlockPointIndex = (currentNewBlockPointIndex + (2 * rotationIndex)) % 8;
            NewBlockLocations rotatedCCWLocation = (NewBlockLocations)rotatedCCWNewBlockPointIndex;
            Point rotatedCCWPoint = ReturnPoint(rotatedCCWLocation, block);
            return rotatedCCWPoint;
        }

        public static Point RotatedCWPoint(this NewBlockLocations self, NewBlockLocations originalLocation,
            Block block, int rotationIndex)
        {
            int currentNewBlockPointIndex = (int)originalLocation;
            int rotatedCWNewBlockPointIndex = currentNewBlockPointIndex - (2 * rotationIndex);
            if (rotatedCWNewBlockPointIndex < 0) rotatedCWNewBlockPointIndex += 8;
            NewBlockLocations rotatedCWLocation = (NewBlockLocations)rotatedCWNewBlockPointIndex;
            Point rotatedCWPoint = ReturnPoint(rotatedCWLocation, block);
            return rotatedCWPoint;
        }
    }
}
