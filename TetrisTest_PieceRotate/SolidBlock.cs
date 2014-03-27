/*
 * The SolidBlock class renders a block that is a solid color with a black border. 
 * It uses an IConstructStyle Interface using object (SolidBlockImages) for building 
 * the block's images, and a IDrawStyle Interface using object (DrawFrom2ImageArray)
 * for drawing them onto the screen.
 * 
 * It only needs a block for its constructor. 
 * 
 * The class has public methods for:
 * -Drawing the block (delegated to the IDrawStyle)
 * -Starting a block's flash animation and checking on the status of that animation
 * (i.e. is it still going, or has it expired). This is
 * also delegated to the IDrawStyle.
 * -Constructing the block(delegated to the IConstructStyle)
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
    public class SolidBlock : IBlockType
    {
        public IConstructStyle constructStyle;
        public IDrawStyle drawStyle;
        private Block block;

        /// <summary>
        /// Instantiates an instance of the SolidBlock class from a reference block.
        /// </summary>
        /// <param name="block">The block to use as a reference</param>
        public SolidBlock(Block block) {
            this.block = block;
            this.constructStyle = new SolidBlockImages(block);
            constructStyle.ConstructBlock();
            this.drawStyle = new DrawFrom2ImageArray(((SolidBlockImages)constructStyle).Images, block);
        } // end constructor method SolidBlock

        /// <summary>
        /// This method draws the solid block onto the screen.
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
