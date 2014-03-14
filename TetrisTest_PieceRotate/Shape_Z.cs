﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Shape_Z : Shape
    {
       
        public Shape_Z(Point location) : this(location, 0){}

        public Shape_Z(Point location, int rotationIndex)
            : base(location, Color.Red)
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

        /*protected override void ConstructShape(int shapeIndex)
        {
            switch (shapeIndex)
            {
                case 0:
                    block1.Location = PivotBlock.NewBlockLeft;
                    block3.Location = PivotBlock.NewBlockBottom;
                    block4.Location = PivotBlock.NewBlockBottomRight;
                    break;
                case 1:
                    block1.Location = PivotBlock.NewBlockBottom;
                    block3.Location = PivotBlock.NewBlockRight;
                    block4.Location = PivotBlock.NewBlockTopRight;
                    break;
                case 2:
                    block1.Location = PivotBlock.NewBlockRight;
                    block3.Location = PivotBlock.NewBlockTop;
                    block4.Location = PivotBlock.NewBlockTopLeft;
                    break;
                case 3:
                    block1.Location = PivotBlock.NewBlockTop;
                    block3.Location = PivotBlock.NewBlockLeft;
                    block4.Location = PivotBlock.NewBlockTopRight;
                    break;
            }

        }*/

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
            return new Shape_Z(location, rotationIndex);
        }
    }
}
