/*
 * The NewBlockLocationsExtensions class extends the NewBlockLocations enum, so that the Block class's
 * NewBlock[whatever direction] points can be returned depending on what the NewBlockLocations enum is.
 * It can also return rotated points.
 * The class has public methods for returning points (rotated or not).
 * 
 * (c) Copyright 2014 Daniel Hopkins. All Rights Reserved.
 * E-mail: dahopkin2@gmail.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public static class NewBlockLocationsExtensions
    {
        /// <summary>
        /// This method returns a point you can use to create a new block, based on a NewBlockLocations enum.
        /// </summary>
        /// <param name="self">A reference to the enum. Will not be used by the user.</param>
        /// <param name="block">The block to use as a reference.</param>
        /// <returns>Returns a point for creating a new block.</returns>
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
        } // end method ReturnPoint

        /// <summary>
        /// This method returns a point rotated 90 degrees counter-clockwise from an original point.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="originalLocation">The NewBlockLocations variable representing the original location.</param>
        /// <param name="block">The block to rotate around.</param>
        /// <param name="rotationIndex">The rotation index to use as a guide.</param>
        /// <returns>A rotated point.</returns>
        public static Point RotatedCCWPoint(this NewBlockLocations self, NewBlockLocations originalLocation,
            Block block, int rotationIndex)
        {
            int currentNewBlockPointIndex = (int)originalLocation;
            int rotatedCCWNewBlockPointIndex = (currentNewBlockPointIndex + (2 * rotationIndex)) % 8;
            NewBlockLocations rotatedCCWLocation = (NewBlockLocations)rotatedCCWNewBlockPointIndex;
            Point rotatedCCWPoint = ReturnPoint(rotatedCCWLocation, block);
            return rotatedCCWPoint;
        } // end method RotatedCCWPoint

        /// <summary>
        /// This method returns a point rotated 90 degrees clockwise from an original point.
        /// </summary>
        /// <param name="self"></param>
        /// <param name="originalLocation">The NewBlockLocations variable representing the original location.</param>
        /// <param name="block">The block to rotate around.</param>
        /// <param name="rotationIndex">The rotation index to use as a guide.</param>
        /// <returns>A rotated point.</returns>
        public static Point RotatedCWPoint(this NewBlockLocations self, NewBlockLocations originalLocation,
            Block block, int rotationIndex)
        {
            int currentNewBlockPointIndex = (int)originalLocation;
            int rotatedCWNewBlockPointIndex = currentNewBlockPointIndex - (2 * rotationIndex);
            if (rotatedCWNewBlockPointIndex < 0) rotatedCWNewBlockPointIndex += 8;
            NewBlockLocations rotatedCWLocation = (NewBlockLocations)rotatedCWNewBlockPointIndex;
            Point rotatedCWPoint = ReturnPoint(rotatedCWLocation, block);
            return rotatedCWPoint;
        } // end method RotatedCWPoint
    }
}
