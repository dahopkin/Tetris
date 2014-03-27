/*
 * The Pile class represents a group of blocks on screen that are no longer moving.
 * 
 * The class has public methods for:
 * -Adding shapes and removing blocks from the pile.
 * -Drawing the pile onto the screen.
 * 
 * (c) Copyright 2014 Daniel Hopkins. All Rights Reserved.
 * E-mail: dahopkin2@gmail.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Pile
    {
        public List<Block> PileBlocks;

        /// <summary>
        /// Instantiates an instance of the Pile class.
        /// </summary>
        public Pile() {
            PileBlocks = new List<Block>();
        } // end constructor method Pile

        /// <summary>
        /// This indexer returns a block in the pile if it's located at a certain point,
        /// or null if it's not.
        /// </summary>
        /// <param name="point">The point to check for a block's presence.</param>
        /// <returns>A block located at a certain point, or null.</returns>
        public Block this[Point point] {
            get {
                for (int i = 0; i < PileBlocks.Count(); i++)
                    if (PileBlocks[i].Location == point) return PileBlocks[i];
                return null;
            } // end get
        } // end indexer

        /// <summary>
        /// This method adds a shape to the pile.
        /// </summary>
        /// <param name="shapeToAdd">The shape to add to the pile.</param>
        public void AddShapeToPile(Shape shapeToAdd) {
            foreach (Block block in shapeToAdd.ShapeBlocks)
                PileBlocks.Add(block);
            //shapeToAdd = null;
        } // end method AddShapeToPile

        /// <summary>
        /// This method removes a list of blocks from the pile.
        /// </summary>
        /// <param name="blockToRemove">The list of blocks to remove from the pile.</param>
        public void RemoveBlocksFromPile(List<Block> blocksToRemove)
        {
            foreach (Block block in blocksToRemove)
                RemoveBlockFromPile(block);
        } // end method RemoveBlocksFromPile

        /// <summary>
        /// This method removes a block from the pile.
        /// </summary>
        /// <param name="blockToRemove">The block to remove from the pile.</param>
        public void RemoveBlockFromPile(Block blockToRemove)
        {
            if (PileBlocks.Contains(blockToRemove))
                PileBlocks.Remove(blockToRemove);
        } // end method RemoveBlockFromPile

        /// <summary>
        /// This method draws the pile of blocks onto the screen.
        /// </summary>
        /// <param name="g">The graphics object to draw onto.</param>
        public void Draw(Graphics g) {
            foreach (Block block in PileBlocks)
                block.Draw(g);
        } // end method Draw
    }
}
