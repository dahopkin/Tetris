/*
 * The DrawFromMultiImageArray class renders a block that has an array of more
 * than two images, animating every image except the last on a repeat loop. The last
 * image is the white block which will show when flashing. The class also controls the flashing
 * of the block when it must be removed from the game.
 * 
 * It needs an image array and a block for its creation. 
 * 
 * The class has public methods for:
 * -Drawing the block onto the screen.
 * -Making the block start flashing, and checking to see if it's still flashing
 * or has stopped flashing.
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
    class DrawFromMultiImageArray : IDrawStyle
    {
        private Bitmap image;
        private Bitmap[] images;
        private Block block;
        public TimedEffect BlockFlashing;
        private int flashIndex = 0;
        private int animationCounterIndex = 0;
        private int animationMaxIndex;
        private bool flipAnimation;

        /// <summary>
        /// Instantiates an instance of the DrawFromMultiImageArray class from an imaage array and a block.
        /// </summary>
        /// <param name="images">The Image array to fill.</param>
        /// <param name="block">The block to use as a reference for drawing images</param>
        public DrawFromMultiImageArray(Bitmap[] images, Block block)
        {
            this.block = block;
            this.images = images;
            this.animationMaxIndex = this.images.Count() - 2;
            this.image = images[animationCounterIndex];
            BlockFlashing = new TimedEffect(500, TimeUnit.Milliseconds);
            this.flipAnimation = false;
        } // end constructor method DrawFromMultiImageArray

        /// <summary>
        /// This method makes the block start flashing.
        /// </summary>
        public void StartFlashing(){BlockFlashing.Start();}
        
        /// <summary>
        /// This method checks to see if the block is done flashing.
        /// </summary>
        /// <returns>A boolean indicating whether the block is done flashing.</returns>
        public bool DoneFlashing() { return BlockFlashing.Expired(); }

        /// <summary>
        /// This method checks to see if the block is flashing.
        /// </summary>
        /// <returns>A boolean indicating whether the block is flashing.</returns>
        public bool IsFlashing() { return BlockFlashing.Active; }

        /// <summary>
        /// This method draws the image onto the screen, using the right picture in the image array.
        /// If the flash animation is active, it will flip between a white block image and a "regular"
        /// image in the array.
        /// </summary>
        /// <param name="g">The graphics object to draw onto.</param>
        public void Draw(Graphics g)
        {
            if (!BlockFlashing.Active)
            {
                image = images[animationCounterIndex];
                IterateAnimation();
                g.DrawImageUnscaled(image, block.Location);
            }
            else
            {
                if (!DoneFlashing())
                {
                    flashIndex = (flashIndex + 1) % 2;
                    image = (flashIndex == 0 ? images[flashIndex] : images[images.Count()-1]);
                    g.DrawImageUnscaled(image, block.Location);
                }
            }
        } // end method Draw

        /// <summary>
        /// This method keeps the animation going.
        /// </summary>
        private void IterateAnimation() {
            if (!flipAnimation) animationCounterIndex++;
            else animationCounterIndex--;

            if (animationCounterIndex == 0) flipAnimation = false;
            else if (animationCounterIndex == animationMaxIndex) flipAnimation = true;
        } // end method IterateAnimation
    }
}
