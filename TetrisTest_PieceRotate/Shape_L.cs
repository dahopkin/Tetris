﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Shape_L : Shape
    {
        public Shape_L(Point location) : base(location, Color.Orange){
            ConstructShape();
        }
        private void ConstructShape()
        {

            Block block1 = new Block(Location, color);
            Block block2 = new Block(block1.NewBlockBottom, color);
            Block block3 = new Block(block2.NewBlockLeft, color);
            Block block4 = new Block(block3.NewBlockLeft, color);
            pivotBlock = block2;
            ShapeBlocks.Add(block1);
            ShapeBlocks.Add(block2);
            ShapeBlocks.Add(block3);
            ShapeBlocks.Add(block4);
        }
    }
}
