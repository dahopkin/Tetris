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

        /// <summary>
        /// Instantiates an instance of the Pile class.
        /// </summary>
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

        /// <summary>
        /// This method adds a shape to the pile.
        /// </summary>
        /// <param name="shapeToAdd">The shape to add to the pile.</param>
        public void AddShapeToPile(Shape shapeToAdd) {
            foreach (Block block in shapeToAdd.ShapeBlocks)
                PileBlocks.Add(block);
            //shapeToAdd = null;
        }

        /// <summary>
        /// This method removes a list of blocks from the pile.
        /// </summary>
        /// <param name="blockToRemove">The list of blocks to remove from the pile.</param>
        public void RemoveBlocksFromPile(List<Block> blocksToRemove)
        {
            foreach (Block block in blocksToRemove)
                RemoveBlockFromPile(block);
        }

        /// <summary>
        /// This method removes a block from the pile.
        /// </summary>
        /// <param name="blockToRemove">The block to remove from the pile.</param>
        public void RemoveBlockFromPile(Block blockToRemove)
        {
            if (PileBlocks.Contains(blockToRemove))
                PileBlocks.Remove(blockToRemove);
        }

        /// <summary>
        /// This method draws the pile of blocks onto the screen.
        /// </summary>
        /// <param name="g">The graphics object to draw onto.</param>
        public void Draw(Graphics g) {
            foreach (Block block in PileBlocks)
                block.Draw(g);
        }
    }
}
