using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Shape_O //: Shape
    {
        /*public Shape_O(Point location) : this(location, 0) { }

        public Shape_O(Point location, BlockType blockType) : this(location, 0, blockType) { }

        public Shape_O(Point location, int rotationIndex) : this(location, rotationIndex, BlockType.Solid) { }

        public Shape_O(Point location, int rotationIndex, BlockType blockType)
            : base(location, Color.Yellow, blockType)
        {
            PivotBlock = block2;
            this.rotationIndex = rotationIndex;
            blocksToAssign = new Block[] { block1, block3, block4 };
            blocksToGoFrom = new Block[] { PivotBlock, PivotBlock, PivotBlock };
            connections = new NewBlockLocations[] { 
                NewBlockLocations.Left,
                NewBlockLocations.Bottom_Left,
                NewBlockLocations.Bottom
            };
            ConstructShape(rotationIndex);
        }

        protected override void ConstructShape(int shapeIndex)
        {
            switch (shapeIndex)
            {
                case 0:
                    block1.Location = PivotBlock.NewBlockLeft;
                    block3.Location = PivotBlock.NewBlockBottomLeft;
                    block4.Location = PivotBlock.NewBlockBottom;
                    break;
            }

        }

        public override void RotateShape()
        {
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
            return new Shape_O(location, rotationIndex, blockType);
        }*/
    }
}
