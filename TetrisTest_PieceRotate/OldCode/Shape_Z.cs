using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Shape_Z //: Shape
    {

       /* public Shape_Z(Point location) : this(location, 0){}

        public Shape_Z(Point location, BlockType blockType) : this(location, 0, blockType) { }

        public Shape_Z(Point location, int rotationIndex) : this(location, rotationIndex, BlockType.Solid) { }

        public Shape_Z(Point location, int rotationIndex, BlockType blockType)
            : base(location, Color.Red, blockType)
        {
            PivotBlock = block2;
            this.rotationIndex = rotationIndex;
            blocksToAssign = new Block[] { block1, block3, block4 };
            blocksToGoFrom = new Block[] { PivotBlock, PivotBlock, PivotBlock };
            connections = new NewBlockLocations[] { 
                NewBlockLocations.Left,
                NewBlockLocations.Bottom,
                NewBlockLocations.Bottom_Right
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
            return new Shape_Z(location, rotationIndex, blockType);
        }*/
    }
}
