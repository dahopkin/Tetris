using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Shape_T //: Shape
    {
        /*public Shape_T(Point location) : this(location, 0) { }

        public Shape_T(Point location, BlockType blockType) : this(location, 0, blockType) { }

        public Shape_T(Point location, int rotationIndex) : this(location, rotationIndex, BlockType.Solid) { }

        public Shape_T(Point location, int rotationIndex, BlockType blockType)
            : base(location, Color.Purple, blockType)
        {
            PivotBlock = block3;
            this.rotationIndex = rotationIndex;
            blocksToAssign = new Block[] { block2, block1, block4 };
            blocksToGoFrom = new Block[] { PivotBlock, PivotBlock, PivotBlock };
            connections = new NewBlockLocations[] { 
                NewBlockLocations.Left,
                NewBlockLocations.Top,
                NewBlockLocations.Right
            };
            ConstructShape(rotationIndex);
        }

        protected override void ConstructShape(int shapeIndex)
        {

            for (int i = 0; i < blocksToAssign.Length; i++)
            {
                blocksToAssign[i].Location =
                    connections[i].RotatedCCWPoint(connections[i],
                    blocksToGoFrom[i], shapeIndex);
            }
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
            return new Shape_T(location, rotationIndex, blockType);
        }*/
    }
}
