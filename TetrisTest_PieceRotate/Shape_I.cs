using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Shape_I : Shape
    {

         public Shape_I(Point location) : this(location, 0){}

         public Shape_I(Point location, int rotationIndex)
            : base(location, Color.SkyBlue)
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
            //switch (shapeIndex)
            //{
            //    case 0:
            //        /*
                      
            //         1P34
            //         */
            //        block1.Location = PivotBlock.NewBlockLeft;
            //        block3.Location = PivotBlock.NewBlockRight;
            //        block4.Location = block3.NewBlockRight;
            //        break;
            //    case 1:
            //        /*
            //          4
            //          3
            //          P
            //          1
            //         */
            //        block1.Location = PivotBlock.NewBlockBottom;
            //        block3.Location = PivotBlock.NewBlockTop;
            //        block4.Location = block3.NewBlockTop;
            //        break;
            //    case 2:
            //        /*
            //         43P1
            //         */
            //        block1.Location = PivotBlock.NewBlockRight;
            //        block3.Location = PivotBlock.NewBlockLeft;
            //        block4.Location = block3.NewBlockLeft;
            //        break;
            //    case 3:
            //        /*
            //          1
            //          P
            //          3
            //          4
            //         */
            //        block1.Location = PivotBlock.NewBlockTop;
            //        block3.Location = PivotBlock.NewBlockBottom;
            //        block4.Location = block3.NewBlockBottom;
            //        break;
            //}

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
            return new Shape_I(location, rotationIndex);
        }
    }
}
