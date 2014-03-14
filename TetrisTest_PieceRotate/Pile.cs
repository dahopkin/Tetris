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

        public Block this[Point point] {
            get {
                for (int i = 0; i < PileBlocks.Count(); i++)
                    if (PileBlocks[i].Location == point) return PileBlocks[i];
                return null;
            }
        }

        public void AddShapeToPile(Shape shapeToAdd) {
            foreach (Block block in shapeToAdd.ShapeBlocks)
                PileBlocks.Add(block);
            //shapeToAdd = null;
        }

        public void RemoveBlocksFromPile(List<Block> blocksToRemove)
        {
            foreach (Block block in blocksToRemove)
                RemoveBlockFromPile(block);
        }

        public void RemoveBlockFromPile(Block blockToRemove)
        {
            if (PileBlocks.Contains(blockToRemove))
                PileBlocks.Remove(blockToRemove);
        }

        public void Draw(Graphics g) {
            foreach (Block block in PileBlocks)
                block.Draw(g);
        }
    }
}
