using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public class Shape
    {
        
        /*This shape class will be the basis for all shapes
         (lists of blocks arranged into certain formations).
         However, the main goal right now is to get a rotate 
         algorithm working. The algorithm I found on youtube needs a pivot
         square to work.
         
         The rotate algorithm works, but now I need to move it.
         It needs to check for collision with another block, and
         we don't have another block in this class.
         * 
         * Maybe I should split processing in case of the move and rotate.
         * have the move/rotate return lists of points, and then have the grid
         * check the lists. if they're good, the block can move and performs the rest of
         * the move operation from within the class.
         * 
         */

        public List<Block> ShapeBlocks;
        //public Rectangle boundaries;
        public Block PivotBlock;
        //public BlockType blockType;
        public Point Location { get; set; }
        public Color Color;
        public Block block1;
        public Block block2;
        public Block block3;
        public Block block4;
        protected int rotationIndex = 0;
        // Turn the following 3 arrays into a class that takes
        // two blocks and a NewBlockLocation as parameters.
        public Block[] blocksToAssign;
        public Block[] blocksToGoFrom;
        public NewBlockLocations[] connections;
        public BlockType blockType;
        public ShapeName shapeName;
        protected ShapeConstructor shapeConstructor;

        /// <summary>
        /// Instantiates an instance of the Shape class from a location point, a color, and a BlockType enum.
        /// </summary>
        /// <param name="location">The location point where the shape is made.</param>
        /// <param name="color">The color of the shape.</param>
        /// <param name="blockType">The type of block.</param>
        //public Shape(Point location, Color color, int rotationIndex, BlockType blockType, ShapeName shapeName)
        public Shape(Point location, int rotationIndex, BlockType blockType, ShapeName shapeName)
        {
            //this.boundaries = boundaries;
            this.Location = location;
            //this.color = color;
            this.ShapeBlocks = new List<Block>();
            this.blockType = blockType;
            this.shapeName = shapeName;
            this.rotationIndex = rotationIndex;
            /*block1 = new Block(location, Color.Blue, blockType);
            block2 = new Block(location, Color.Blue, blockType);
            block3 = new Block(location, Color.Blue, blockType);
            block4 = new Block(location, Color.Blue, blockType);
            ShapeBlocks.Add(block1);
            ShapeBlocks.Add(block2);
            ShapeBlocks.Add(block3);
            ShapeBlocks.Add(block4);*/
            shapeConstructor = new ShapeConstructor(this);
            shapeConstructor.FormShape();
            ConstructShape(rotationIndex);
        }

        public Shape(Point location, BlockType blockType, ShapeName shapeName) : this(location, 0, blockType, shapeName){}


        /*public Shape(Point location, Color color, BlockType blockType)
        {
            //this.boundaries = boundaries;
            this.Location = location;
            this.Color = color;
            this.ShapeBlocks = new List<Block>();
            this.blockType = blockType;
            block1 = new Block(location, color, blockType);
            block2 = new Block(location, color, blockType);
            block3 = new Block(location, color, blockType);
            block4 = new Block(location, color, blockType);
            ShapeBlocks.Add(block1);
            ShapeBlocks.Add(block2);
            ShapeBlocks.Add(block3);
            ShapeBlocks.Add(block4);
        }*/

        /// <summary>
        /// Instantiates a solid block instance of the Shape class from a location point and a color.
        /// </summary>
        /// <param name="location">The location point where the shape is made.</param>
        /// <param name="color">The color of the shape.</param>
        //public Shape(Point location, Color color): this(location, color, BlockType.Solid) { }

        /// <summary>
        /// This method draws the Shape onto the screen.
        /// </summary>
        /// <param name="g">The graphics object to draw onto.</param>
        public void Draw(Graphics g) {
            foreach (Block block in ShapeBlocks)
                block.Draw(g);
        }

        //protected abstract void ConstructShape(int shapeIndex);

        /// <summary>
        /// This method copies the shape exactly 
        /// </summary>
        /// <returns>A copy of the shape.</returns>
        //public abstract Shape CopyShape();

        //public abstract Shape CopyShape(Point location);

        //public abstract Shape CopyShape(Point location, int rotationIndex);

        public Shape CopyShape(){
            return CopyShape(PivotBlock.Location, this.rotationIndex);
        }

        public Shape CopyShape(Point location){
            return CopyShape(location, 0);
        }

        public Shape CopyShape(Point location, int rotationIndex){
            //return new Shape(location, rotationIndex, blockType);
            return new Shape(location, rotationIndex, this.blockType, this.shapeName);
        }

        public virtual void RotateShape() {
            rotationIndex = (rotationIndex + 1) % 4;
            ConstructShape(rotationIndex);
        }

        protected virtual void ConstructShape(int shapeIndex)
        {
            for (int i = 0; i < blocksToAssign.Length; i++)
            {
                blocksToAssign[i].Location =
                    connections[i].RotatedCCWPoint(connections[i],
                    blocksToGoFrom[i], shapeIndex);
            }
        }

        /// <summary>
        /// Moves the shape a certain distance in the direction specified.
        /// </summary>
        /// <param name="direction">The direction to move.</param>
        /// <param name="distance">The distance to move.</param>
        public void Move(Direction direction, int distance) { 
            // the code to ensure WHETHER the shape can move
            // will have to be in the grid class.
            switch (direction) { 
                case (Direction.Down):
                    MoveShapeDown(distance);
                    break;
                case (Direction.Left):
                    MoveShapeLeft(distance);
                    break;
                case (Direction.Right):
                    MoveShapeRight(distance);
                    break;
                case (Direction.Up):
                    MoveShapeUp(distance);
                    break;
                default:
                    break;
            } // end switch

        }

        /// <summary>
        /// Moves the shape one block in the direction specified.
        /// </summary>
        /// <param name="direction">The direction to move.</param>
        public void Move(Direction direction)
        {
            Move(direction, Block.BlockLength);
        }

        /// <summary>
        /// This method moves the entire shape down.
        /// </summary>
        /// <param name="distance">The distance to move</param>
        public void MoveShapeDown(int distance){
            for (int i = 0; i < ShapeBlocks.Count; i++)
                ShapeBlocks[i].Move(Direction.Down, distance);
        }

        /// <summary>
        /// This method moves the entire shape left.
        /// </summary>
        /// <param name="distance">The distance to move</param>
        public void MoveShapeLeft(int distance)
        {
            for (int i = 0; i < ShapeBlocks.Count; i++)
                ShapeBlocks[i].Move(Direction.Left, distance);
        }

        /// <summary>
        /// This method moves the entire shape right.
        /// </summary>
        /// <param name="distance">The distance to move</param>
        public void MoveShapeRight(int distance)
        {
            for (int i = 0; i < ShapeBlocks.Count; i++)
                ShapeBlocks[i].Move(Direction.Right, distance);
        }

        /// <summary>
        /// This method moves the entire shape up.
        /// </summary>
        /// <param name="distance">The distance to move</param>
        public void MoveShapeUp(int distance)
        {
            for (int i = 0; i < ShapeBlocks.Count; i++)
                ShapeBlocks[i].Move(Direction.Up, distance);
        }

    }
}
