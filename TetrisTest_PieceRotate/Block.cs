using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace Tetris
{
    class Block
    {
        /*
         * This class will be for a basic Tetris block. This block is intended
         * to be the basic unit behind all shapes. It should have:
         * Fields:
         * -A location property(Point).
         * -A backing rectangle for Area.(Rectangle).
         * -A default block size.(Size)
         * Methods/Properties:
         * -Property returning a point representing the top right.
         * -Property returning a point representing the lower left.
         * -Property returning a point representing the top left plus block length.
         * -Property returning a point representing the lower left.
         
         */
        // an int to represent the length.
        private const int blockLength = 20;
        public int BlockLength { get { return blockLength; } }
        // a size to represent the size. Will be 
        // filled by blocklength for length and width.
        private Size blockSize;
        // Location of the block. Upper left corner.
        public Point Location { get; set; }

        // The rectangle
        public Rectangle Area
        {
            get { return new Rectangle(Location, blockSize); }
        } // end property Area

        /// <summary>
        /// The starting location to create a new block to the left of the current one.
        /// </summary>
        public Point NewBlockLeft {
            get { return new Point((Location.X - blockLength), Location.Y); }
        }

        /// <summary>
        /// The starting location to create a new block to the right of the current one.
        /// </summary>

        public Point NewBlockRight
        {
            get { return new Point(Area.Right, Location.Y); }
        }

        /// <summary>
        /// The starting location to create a new block on top of the current one.
        /// </summary>

        public Point NewBlockTop
        {
            get { return new Point(Location.X, (Location.Y - blockLength)); }
        }

        /// <summary>
        /// The starting location to create a new block on top and to the right of the current one.
        /// </summary>
        public Point NewBlockTopRight
        {
            get { return new Point((Location.X + blockLength), (Location.Y - blockLength)); }
        }

        /// <summary>
        /// The starting location to create a new block on top and to the left of the current one.
        /// </summary>
        public Point NewBlockTopLeft
        {
            get { return new Point((Location.X - blockLength), (Location.Y - blockLength)); }
        }

        /// <summary>
        /// The starting location to create a new block on the bottom of the current one.
        /// </summary>
        public Point NewBlockBottom
        {
            get { return new Point(Location.X, Area.Bottom); }
        }

        /// <summary>
        /// The starting location to create a new block on the bottom and to the right of the current one.
        /// </summary>
        public Point NewBlockBottomRight
        {
            get { return new Point((Location.X + blockLength), Area.Bottom); }
        }

        /// <summary>
        /// The starting location to create a new block on the bottom and to the left of the current one.
        /// </summary>
        public Point NewBlockBottomLeft
        {
            get { return new Point((Location.X - blockLength), Area.Bottom); }
        }

        public Point TopRight
        {
            get { return new Point((Location.X + blockLength), Area.Top); }
        }

        public Point BottomRight
        {
            get { return new Point((Location.X + blockLength), Area.Bottom); }
        }

        public Point BottomLeft
        {
            get { return new Point(Location.X, Area.Bottom); }
        }


        public Color color;
        
        public Block(Point location, Color color) {
            this.Location = location;
            blockSize = new Size(blockLength, blockLength);
            this.color = color;
        } // end property Block

        // This is for when you want to just use a phantom block to test things.
        public Block(Point location): this(location, Color.Transparent)
        {
            
        } // end property Block

        public void Draw(Graphics g){
            Pen borderPen = new Pen(Color.Black);
            Brush backColorBrush = new SolidBrush(color);
            g.FillRectangle(backColorBrush, Area);
            g.DrawRectangle(borderPen,Area);
        } // end method Draw

    }
}
