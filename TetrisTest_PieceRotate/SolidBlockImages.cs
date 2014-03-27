/*
 * The SolidBlockImages class makes a block use an array of images that are solid with a black border
 * for drawing onto the screen. This array of images will be passed on to whatver drawing method
 * renders them onto the screen.
 * 
 * It needs a block to manipulate for for its creation.
 * 
 * The class has a public method for building the block's images, which is really 
 * just using its private methods for building the images behind the scenes.
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
    class SolidBlockImages : IConstructStyle
    {
        public Bitmap[] Images;
        private Block block;

        /// <summary>
        /// Instantiates an instance of the SolidBlockImages class from a block.
        /// </summary>
        /// <param name="block">The block to use as a basis for constructing images.</param>
        public SolidBlockImages(Block block) {
            this.Images = new Bitmap[2];
            this.block = block;
        } // end constructor method SolidBlockImages

        /// <summary>
        /// This method builds the Images array within the SolidBlockImages class.
        /// </summary>
        public void ConstructBlock()
        {
            Images[0] = new Bitmap(block.BlockSize.Width, block.BlockSize.Height);
            Images[1] = new Bitmap(block.BlockSize.Width, block.BlockSize.Height);
            ConstructImage(Images[0], block.Color);
            ConstructImage(Images[1], Color.White);
        } // end method ConstructBlock

        /// <summary>
        /// This method builds an image consisting of a solid color block with a black border.
        /// </summary>
        /// <param name="image">The blank image to draw graphics onto.</param>
        /// <param name="color">The color to use in constructing the shape</param>
        private void ConstructImage(Bitmap image, Color color)
        {
            Pen borderPen = new Pen(Color.Black);
            Brush backColorBrush = new SolidBrush(color);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.FillRectangle(backColorBrush, 0, 0, image.Width, image.Height);
                g.DrawRectangle(borderPen, 0, 0, image.Width, image.Height);
            } // end using 
        } // end method ConstructImage

    }
}
