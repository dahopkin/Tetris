/*
 * The Shape class is the basis for all shapes (I, J, L, O, S, T, Z) seen within the game.  
 * It needs a location, an integer (for rotated position), a BlockType, and a ShapeName to be created.
 * However, it has alternate constructors.
 * The class has public methods for moving, copying, and rotating the shape.
 * The class has private methods for creating the shape depending on the ShapeName and BlockType.
 * It delegates all shape creation to the ShapeConstructor class. Without that class, the shape
 * wouldn't be formed.
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
    public class Shape
    {
        
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
            ArrangeShape(rotationIndex);
        } // end constructor method Shape

        public Shape(Point location, BlockType blockType, ShapeName shapeName)
            : this(location, 0, blockType, shapeName) { }  // end constructor method Shape

        /// <summary>
        /// This method draws the Shape onto the screen.
        /// </summary>
        /// <param name="g">The graphics object to draw onto.</param>
        public void Draw(Graphics g) {
            foreach (Block block in ShapeBlocks)
                block.Draw(g);
        } // end method Draw

        /// <summary>
        /// This method copies a shape at the same position and location as the original.
        /// </summary>
        /// <returns>A copied shape in the same position and location as the original.</returns>
        public Shape CopyShape(){
            return CopyShape(PivotBlock.Location, this.rotationIndex);
        } // end method CopyShape

        /// <summary>
        /// This method copies a shape to another with a given location and the starting shape position.
        /// </summary>
        /// <param name="location">The location to place the copied shape.</param>
        /// <returns>A copied shape at a given location and the original shape position.</returns>
        public Shape CopyShape(Point location){
            return CopyShape(location, 0);
        } // end method CopyShape

        /// <summary>
        /// This method copies a shape to another with a given location and a given shape position.
        /// </summary>
        /// <param name="location">The location to place the copied shape.</param>
        /// <param name="rotationIndex">The position the shape is supposed to have.</param>
        /// <returns>A copied shape in the suggested location and position.</returns>
        public Shape CopyShape(Point location, int rotationIndex){
            //return new Shape(location, rotationIndex, blockType);
            return new Shape(location, rotationIndex, this.blockType, this.shapeName);
        } // end method CopyShape

        /// <summary>
        /// This method rotates a shape 90 degrees CounterClockWise.
        /// </summary>
        public virtual void RotateShape() {
            /* Rotation is actually handled in the NewBlockLocationsExtensions
             * class. The ConstructShape method automatically takes care of this
             * as a result.
             */
            rotationIndex = (rotationIndex + 1) % 4;
            ArrangeShape(rotationIndex);
        } // end method RotateShape

        /// <summary>
        /// This method arranges the shape's blocks into a certain formation, depending on
        /// the position number
        /// </summary>
        /// <param name="positionNumber">The position you want the shape to have.</param>
        protected virtual void ArrangeShape(int positionNumber)
        {
            for (int i = 0; i < blocksToAssign.Length; i++)
            {
                blocksToAssign[i].Location =
                    connections[i].RotatedCCWPoint(connections[i],
                    blocksToGoFrom[i], positionNumber);
            }
        } // end method ConstructShape

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

        } // end method Move

        /// <summary>
        /// Moves the shape one block in the direction specified.
        /// </summary>
        /// <param name="direction">The direction to move.</param>
        public void Move(Direction direction)
        {
            Move(direction, Block.BlockLength);
        } // end method Move

        /// <summary>
        /// This method moves the entire shape down.
        /// </summary>
        /// <param name="distance">The distance to move</param>
        public void MoveShapeDown(int distance){
            for (int i = 0; i < ShapeBlocks.Count; i++)
                ShapeBlocks[i].Move(Direction.Down, distance);
        } // end method MoveShapeDown

        /// <summary>
        /// This method moves the entire shape left.
        /// </summary>
        /// <param name="distance">The distance to move</param>
        public void MoveShapeLeft(int distance)
        {
            for (int i = 0; i < ShapeBlocks.Count; i++)
                ShapeBlocks[i].Move(Direction.Left, distance);
        } // end method MoveShapeLeft

        /// <summary>
        /// This method moves the entire shape right.
        /// </summary>
        /// <param name="distance">The distance to move</param>
        public void MoveShapeRight(int distance)
        {
            for (int i = 0; i < ShapeBlocks.Count; i++)
                ShapeBlocks[i].Move(Direction.Right, distance);
        } // end method MoveShapeRight

        /// <summary>
        /// This method moves the entire shape up.
        /// </summary>
        /// <param name="distance">The distance to move</param>
        public void MoveShapeUp(int distance)
        {
            for (int i = 0; i < ShapeBlocks.Count; i++)
                ShapeBlocks[i].Move(Direction.Up, distance);
        } // end method MoveShapeUp

    }
}
