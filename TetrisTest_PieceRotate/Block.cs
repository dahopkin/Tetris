using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace Tetris
{
    public class Block
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
        private const int blockLength = 24;
        private Bitmap image;
        private Bitmap[] images;
        public static int BlockLength { get { return blockLength; } }
        // a size to represent the size. Will be 
        // filled by blocklength for length and width.
        private Size blockSize;
        // Location of the block. Upper left corner.
        public Point Location { get; set; }
        private bool isClearing;
        public bool IsClearing {
            get { return isClearing; } 
            set{
                isClearing = value;
                flickerStartTime = DateTime.Now;
            } 
        }
        public bool HasCleared { get; set; }
        private int flickerIndex = 0;
        private DateTime flickerStartTime;
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

        /// <summary>
        /// Gets the point at the center of the object's area.
        /// </summary>
        public Point Middle
        {
            get
            {
                return new Point((Area.Left + Area.Width / 2), (Area.Top + Area.Height / 2));
            } // end get 
        } // end property Middle

        public Color color;
       
        public List<Point> AllCollisionPoints
        {
            get
            {
                List<Point> allCollisionPoints = new List<Point>();
                for (int i = Area.Left; i <= Area.Width; i++)
                {
                    allCollisionPoints.Add(new Point(i, Area.Top));
                    allCollisionPoints.Add(new Point(i, Area.Bottom));
                }
                for (int i = Area.Top; i <= Area.Bottom; i++)
                {
                    allCollisionPoints.Add(new Point(Area.Left, i));
                    allCollisionPoints.Add(new Point(Area.Right, i));
                }
                return allCollisionPoints;
            } // end get 
        } // end property AllCollisionPoints

        /// <summary>
        /// Gets a list of points representing all new block points of the area, organized into
        /// counter-clockwise order.
        /// </summary>
        public List<Point> CCWBlockPoints
        {
            get
            {
                return new List<Point>(){
                    this.NewBlockRight,
                    this.NewBlockTopRight,
                    this.NewBlockTop,
                    this.NewBlockTopLeft,
                    this.NewBlockLeft,
                    this.NewBlockBottomLeft,
                    this.NewBlockBottom,
                    this.NewBlockBottomRight,
                };
            } // end get 
        } // end property CCWBlockPoints

        public Block(Point location, Color color)
        {
            this.Location = location;
            blockSize = new Size(blockLength, blockLength);
            this.color = color;
            this.isClearing = false;
            this.HasCleared = false;
            images = new Bitmap[2];
            ConstructImages();
            image = images[0];
        } 
        // This is for when you want to just use a phantom block to test things.
        public Block(Point location): this(location, Color.Gray)
        {
            
        } // end property Block

        private void ConstructImage(Bitmap image, Color color)
        {
            Pen borderPen = new Pen(Color.Black);
            Brush backColorBrush = new SolidBrush(color);
            //Color fadedColor = Color.FromArgb(127, color);
            //Pen borderPen = new Pen(color);
            //Brush backColorBrush = new SolidBrush(fadedColor);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.FillRectangle(backColorBrush, 0,0,image.Width, image.Height);
                g.DrawRectangle(borderPen, 0,0, image.Width, image.Height);
            }
        } // end property Block

        private void ConstructImages()
        {
            images[0] = new Bitmap(blockSize.Width, blockSize.Height);
            images[1] = new Bitmap(blockSize.Width, blockSize.Height);
            ConstructImage(images[0], color);
            ConstructImage(images[1], Color.White); 

        } // end property Block

       

        //public void Draw(Graphics g){
        //    Pen borderPen = new Pen(Color.Black);
        //    Brush backColorBrush = new SolidBrush(color);
        //    if (!IsClearing)
        //    {
        //        g.FillRectangle(backColorBrush, Area);
        //        g.DrawRectangle(borderPen, Area);
        //    }
        //    else {
        //        DateTime flickerCurrentTime = DateTime.Now;
        //        TimeSpan duration = flickerCurrentTime - flickerStartTime;
        //        if (duration.Milliseconds < 500)
        //        {
        //            flickerIndex = (flickerIndex + 1) % 2;
        //            if (flickerIndex == 1)
        //            {
        //                g.FillRectangle(Brushes.White, Area);
        //                g.DrawRectangle(borderPen, Area);
        //            }
        //            else
        //            {
        //                g.FillRectangle(backColorBrush, Area);
        //                g.DrawRectangle(borderPen, Area);
        //            }
        //        }
        //        else
        //            HasCleared = true;
        //    }
        //} // end method Draw

        public void Draw(Graphics g)
        {
            /*
             image = invaderImages[animationCell];
            g.DrawImageUnscaled(image, Location);
             */
            if (!IsClearing)
            {
                image = images[0];
                g.DrawImageUnscaled(image, Location);
            }
            else
            {
                DateTime flickerCurrentTime = DateTime.Now;
                TimeSpan duration = flickerCurrentTime - flickerStartTime;
                if (duration.Milliseconds < 500)
                {
                        flickerIndex = (flickerIndex + 1) % 2;
                        image = images[flickerIndex];
                        g.DrawImageUnscaled(image, Location);
                }
                else
                    HasCleared = true;
            }
        } // end method Draw

    }
}
