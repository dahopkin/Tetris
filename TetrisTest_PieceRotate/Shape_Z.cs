﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Shape_Z : Shape
    {
        public Shape_Z(Point location) : base(location, Color.Red){
            ConstructShape();
        }
        private void ConstructShape()
        {

            Block block1 = new Block(Location, color);
            Block block2 = new Block(block1.NewBlockBottom, color);
            Block block3 = new Block(block2.NewBlockRight, color);
            Block block4 = new Block(block3.NewBlockBottom, color);
            pivotBlock = block2;
            ShapeBlocks.Add(block1);
            ShapeBlocks.Add(block2);
            ShapeBlocks.Add(block3);
            ShapeBlocks.Add(block4);
        }
    }
}
