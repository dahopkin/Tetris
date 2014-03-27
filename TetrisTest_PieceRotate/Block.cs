/*
 * The Block class represents an individual block contained within a Tetris shape.
 * 
 * It needs a location Point, a Color, and a BlockType for its creation, 
 * though there are some overloaded constructors that will default to a solid block. 
 * It utilizes a certain type of block design based on the BlockType. 
 * The BlockType decides which IBlockType to use, and the classes implementing 
 * the IBlockType interface handle all graphical elements of the block (from how
 * it's graphically constructed to how it's drawn on the screen). This is complicated, but
 * I did things this way to be able to have several different block appearances.
 * This way, it's possible to have solid blocks, hollow blocks, and even blocks that are
 * animated within themselves.
 * 
 * The class has public methods for: 
 * -Moving the block.
 * -Drawing the block on the screen.
 * -Controlling the flash effect of the block (which triggers when it's being cleared).
 * -Returning points along the edge of the Block's rectangular area.
 * -Returning points where new blocks can be created adjacent to the current block.
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
    public class Block
    {
        
        // an int to represent the length.
        private const int blockLength = 24;
        
        public Color Color;
        
        public IBlockType iBlockType;

        public BlockType BlockType;
        
        /// <summary>
        /// Gets the length of a block.
        /// </summary>
        public static int BlockLength { get { return blockLength; } }
        
        /// <summary>
        /// Gets the size of the block.
        /// </summary>
        public Size BlockSize { get { return new Size(BlockLength, BlockLength); } }
        
        /// <summary>
        /// Gets or sets the Location point of the block.
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Gets the rectangle representing the area of the block.
        /// </summary>
        public Rectangle Area
        {
            get { return new Rectangle(Location, BlockSize); }
        } // end property Area

       
        /// <summary>
        /// Instantiates an instance of the the Block class from a location point, a color, and a BlockType enum.
        /// </summary>
        /// <param name="location">The location of the block.</param>
        /// <param name="color">The color of the block.</param>
        /// <param name="blockType"></param>
        public Block(Point location, Color color, BlockType BlockType) {
            this.Location = location;
            this.Color = color;
            this.BlockType = BlockType;
            DecideBlockType(BlockType);
        } // end constructor method Block

        /// <summary>
        /// Instantiates an instance of the the Block class from a location point and a color.
        /// </summary>
        /// <param name="location">The location of the block.</param>
        /// <param name="color">The color of the block.</param>
        public Block(Point location, Color color) 
            : this(location, color, BlockType.Solid){} // end constructor method Block

        /// <summary>
        /// Instantiates a static gray instance of the the Block object from a location point.
        /// </summary>
        /// <param name="location">The locaton of the block.</param>
        public Block(Point location) 
            : this(location, Color.Gray) { } // end constructor method Block

        /// <summary>
        /// This method allocates the correct block type to use based on the BlockType enum.
        /// </summary>
        /// <param name="blockType"></param>
        public void DecideBlockType(BlockType blockType) {
            switch (blockType) { 
                case BlockType.Solid:
                    iBlockType = new SolidBlock(this);
                    break;
                case BlockType.Hollow:
                    iBlockType = new HollowBlock(this);
                    break;
                case BlockType.Fade:
                    iBlockType = new FadedBlock(this);
                    break;
                default:
                    break;
            }
        } // end method DecideBlockType

        #region Point Properties

        /// <summary>
        /// The starting location to create a new block to the left of the current one.
        /// </summary>
        public Point NewBlockLeft {
            get { return new Point((Location.X - blockLength), Location.Y); }
        } // end method NewBlockLeft

        /// <summary>
        /// The starting location to create a new block to the right of the current one.
        /// </summary>
        public Point NewBlockRight
        {
            get { return new Point(Area.Right, Location.Y); }
        } // end method NewBlockRight

        /// <summary>
        /// The starting location to create a new block on top of the current one.
        /// </summary>
        public Point NewBlockTop
        {
            get { return new Point(Location.X, (Location.Y - blockLength)); }
        } // end method NewBlockTop

        /// <summary>
        /// The starting location to create a new block on top and to the right of the current one.
        /// </summary>
        public Point NewBlockTopRight
        {
            get { return new Point((Location.X + blockLength), (Location.Y - blockLength)); }
        } // end method NewBlockTopRight

        /// <summary>
        /// The starting location to create a new block on top and to the left of the current one.
        /// </summary>
        public Point NewBlockTopLeft
        {
            get { return new Point((Location.X - blockLength), (Location.Y - blockLength)); }
        } // end method NewBlockTopLeft

        /// <summary>
        /// The starting location to create a new block on the bottom of the current one.
        /// </summary>
        public Point NewBlockBottom
        {
            get { return new Point(Location.X, Area.Bottom); }
        } // end method NewBlockBottom

        /// <summary>
        /// The starting location to create a new block on the bottom and to the right of the current one.
        /// </summary>
        public Point NewBlockBottomRight
        {
            get { return new Point((Location.X + blockLength), Area.Bottom); }
        } // end method NewBlockBottomRight

        /// <summary>
        /// The starting location to create a new block on the bottom and to the left of the current one.
        /// </summary>
        public Point NewBlockBottomLeft
        {
            get { return new Point((Location.X - blockLength), Area.Bottom); }
        } // end method NewBlockBottomLeft

        /// <summary>
        /// The location of the top right of the block.
        /// </summary>
        public Point TopRight
        {
            get { return new Point((Location.X + blockLength), Area.Top); }
        } // end method TopRight

        /// <summary>
        /// The location of the bottom right of the block.
        /// </summary>
        public Point BottomRight
        {
            get { return new Point((Location.X + blockLength), Area.Bottom); }
        } // end method BottomRight

        /// <summary>
        /// The location of the bottom left of the block.
        /// </summary>
        public Point BottomLeft
        {
            get { return new Point(Location.X, Area.Bottom); }
        } // end method BottomLeft

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
       
        /// <summary>
        /// Gets every point on the edge of the block.
        /// </summary>
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
        #endregion
        
        /// <summary>
        /// This method draws the block onto the screen.
        /// </summary>
        /// <param name="g">The graphics object to draw onto.</param>
        public void Draw(Graphics g)
        {
            iBlockType.Draw(g);
        } // end method Draw

        /// <summary>
        /// This method starts the block's flashing animation.
        /// </summary>
        public void StartFlashing() {
            iBlockType.StartFlashing();
        } 

        /// <summary>
        /// This method checks to see if the block is done flashing.
        /// </summary>
        /// <returns>A boolean indicating if the block is done flashing.</returns>
        public bool DoneFlashing()
        {
            return iBlockType.DoneFlashing();
        }

        /// <summary>
        /// This method checks to see if the block is flashing.
        /// </summary>
        /// <returns>A boolean indicating whether or not the block is flashing.</returns>
        public bool IsFlashing()
        {
            return iBlockType.IsFlashing();
        }

        /// <summary>
        /// This method moves the block left, right, or down.
        /// </summary>
        /// <param name="directionToMove">The direction in which to move.</param>
        /// <param name="distanceToMove">The distance to move.</param>
        public void Move(Direction directionToMove, int distanceToMove) {
            switch (directionToMove) { 
                case Direction.Left:
                    Location = new Point((Location.X - distanceToMove), Location.Y);
                    break;
                case Direction.Right:
                    Location = new Point((Location.X + distanceToMove), Location.Y);
                    break;
                case Direction.Down:
                    Location = new Point(Location.X, (Location.Y + distanceToMove));
                    break;
                case Direction.Up:
                    Location = new Point(Location.X, (Location.Y - distanceToMove));
                    break;
                default:
                    break;
            } // end switch 
        } // end method Move

    }
}
