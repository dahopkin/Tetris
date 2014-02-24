using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Pile
    {
        /*Pile, like Shape, is a combination of blocks. Unlike Shape, it will change
         * over time as it absorbs new blocks into itself from shapes. */
        public List<Block> PileBlocks;

        public Pile() {
            PileBlocks = new List<Block>();
        }

        public void AddShapeToPile(Shape shapeToAdd) {
            foreach (Block block in shapeToAdd.ShapeBlocks)
                PileBlocks.Add(block);
        }
    }
}
