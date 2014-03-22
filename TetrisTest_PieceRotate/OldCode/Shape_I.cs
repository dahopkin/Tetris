using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Shape_I //: Shape
    {

         /*public Shape_I(Point location) : this(location, 0){}

         public Shape_I(Point location, BlockType blockType) : this(location, 0, blockType) { }
        
         public Shape_I(Point location, int rotationIndex) : this(location, rotationIndex, BlockType.Solid){}

         public Shape_I(Point location, int rotationIndex, BlockType blockType)
             : base(location, Color.SkyBlue, blockType)
         {
             PivotBlock = block2;
             this.rotationIndex = rotationIndex;
             ConstructShape(rotationIndex);
         }

         protected override void ConstructShape(int shapeIndex)
         {
            Block[] blocksToAssign = new Block[] { block1, block3, block4};
            Block[] blocksToGoFrom = new Block[] { PivotBlock, PivotBlock, block3};
            NewBlockLocations[] connections = new NewBlockLocations[] { 
                NewBlockLocations.Left,
                NewBlockLocations.Right,
                NewBlockLocations.Right
            };
            for (int i = 0; i < blocksToAssign.Length; i++)
            {
                blocksToAssign[i].Location = connections[i].RotatedCCWPoint(connections[i], blocksToGoFrom[i], shapeIndex);
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
            return new Shape_I(location, rotationIndex, blockType);
        }*/
    }
}
