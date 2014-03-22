using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class FadedBlockImages : IConstructStyle
    {
        public Bitmap[] Images;
        private Block block;

        /// <summary>
        /// Instantiates an instance of the FadedBlockImages class from a block.
        /// </summary>
        /// <param name="block">The block to use as a basis for constructing images.</param>
        public FadedBlockImages(Block block)
        {
            this.Images = new Bitmap[5];
            this.block = block;
        }

        /// <summary>
        /// This method builds the Images array within the HollowBlockImages class.
        /// </summary>
        public void ConstructBlock()
        {
            /*Images[0] = new Bitmap(block.BlockSize.Width, block.BlockSize.Height);
            Images[1] = new Bitmap(block.BlockSize.Width, block.BlockSize.Height);*/
            for (int i = 0; i < Images.Count(); i++)
                Images[i] = new Bitmap(block.BlockSize.Width, block.BlockSize.Height);
            
            ConstructImages(Images);
        } // end method ConstructBlock

        /// <summary>
        /// This method builds an image consisting of a semi-transparent color block with a full color border.
        /// </summary>
        /// <param name="image">The blank image to draw graphics onto.</param>
        /// <param name="backColor">The color to use in constructing the shape</param>
        private void ConstructImage(Bitmap image, Color backColor, Color borderColor, int fadeAmount)
        {
            Pen borderPen = new Pen(Color.FromArgb(fadeAmount, borderColor));
            Brush backColorBrush = new SolidBrush(Color.FromArgb(fadeAmount, backColor));
            using (Graphics g = Graphics.FromImage(image))
            {
                g.FillRectangle(backColorBrush, 0, 0, image.Width, image.Height);
                g.DrawRectangle(borderPen, 0, 0, image.Width, image.Height);
            } // end using 
        } // end method ConstructImage

        /// <summary>
        /// This method builds an image consisting of a semi-transparent color block with a full color border.
        /// </summary>
        /// <param name="image">The blank image to draw graphics onto.</param>
        /// <param name="color">The color to use in constructing the shape</param>
        private void ConstructImages(Bitmap[] images)
        {
            int fader = 32;
            for (int i = 0; i < images.Count() - 2; i++)
            {
                ConstructImage(images[i], block.Color, Color.Black, (255 - (fader*(i+1))));
            }

            ConstructImage(images[images.Count() - 1], Color.White, Color.Black, 255);
        } // end method ConstructImage

    }
}
