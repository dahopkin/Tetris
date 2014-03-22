using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Shape_J //: Shape
    {

        /*public Shape_J(Point location) : this(location, 0) { }

        public Shape_J(Point location, BlockType blockType) : this(location, 0, blockType) { }

        public Shape_J(Point location, int rotationIndex) : this(location, rotationIndex, BlockType.Solid) { }

        public Shape_J(Point location, int rotationIndex, BlockType blockType)
            : base(location, Color.Blue, blockType)
        {
            PivotBlock = block2;
            this.rotationIndex = rotationIndex;
            blocksToAssign = new Block[] { block1, block3, block4 };
            blocksToGoFrom = new Block[] { PivotBlock, PivotBlock, block3 };
            connections = new NewBlockLocations[] { 
                NewBlockLocations.Top,
                NewBlockLocations.Right,
                NewBlockLocations.Right
            };
            ConstructShape(rotationIndex);
        }


        public override Shape CopyShape()
        {
            return CopyShape(PivotBlock.Location, this.rotationIndex);
        }

        public override Shape CopyShape(Point location)
        {
            return CopyShape(location, 0);
        }

        public override Shape CopyShape(Point location, int rotationIndex)
        {
            return new Shape_J(location, rotationIndex, blockType);
        }*/
    }
}
