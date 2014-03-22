﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public class FaderBlock : IBlockType
    {
        public IConstructStyle constructStyle;
        public IDrawStyle drawStyle;
        private Block block;

        /// <summary>
        /// Instantiates an instance of the HollowBlock class from a reference block.
        /// </summary>
        /// <param name="block">The block to use as a reference</param>
        public FaderBlock(Block block)
        {
            this.block = block;
            this.constructStyle = new FadedBlockImages(block);
            constructStyle.ConstructBlock();
            this.drawStyle = new DrawFromMultiImageArray(((FadedBlockImages)constructStyle).Images, block);
        } // end constructor method HollowBlock

        /// <summary>
        /// This method draws the hollow block onto the screen.
        /// </summary>
        /// <param name="g">The graphics object to draw onto.</param>
        public void Draw(Graphics g) { drawStyle.Draw(g); }

        /// <summary>
        /// This method makes the block start flashing.
        /// </summary>
        public void StartFlashing() { drawStyle.StartFlashing(); }

        /// <summary>
        /// This method checks to see if the block is flashing.
        /// </summary>
        /// <returns>A boolean indicating whether the block is flashing.</returns>
        public bool IsFlashing() { return drawStyle.IsFlashing(); }

        /// <summary>
        /// This method checks to see if the block is done flashing.
        /// </summary>
        /// <returns>A boolean indicating whether the block is done flashing.</returns>
        public bool DoneFlashing() { return drawStyle.DoneFlashing(); }

        /// <summary>
        /// This method creates the block according to whatever the constructStyle is.
        /// </summary>
        public void ConstructBlock() { constructStyle.ConstructBlock(); }
    }
}