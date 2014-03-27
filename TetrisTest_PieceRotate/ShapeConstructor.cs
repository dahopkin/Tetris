/*
 * The ShapeConstructor class forms the blocks in the Shape class (when a shape is created)
 * into one of the shapes seen within the game (I, J, L, O, S, T, Z), depending on the shape's ShapeName.
 * It needs a shape to manipulate for its creation.
 * Based on the shape's ShapeName, the class will assign a color and a block formation.
 * The class has a public method for forming the shape.
 * The class has private methods for assigning color and Shape formation.
 * The class is rigid in creation, and a color cannot be assigned to a shape by a client/user.
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
    public class ShapeConstructor
    {
        Shape shape;
        
        /// <summary>
        /// Instantiates an instance of the ShapeConstructor class from a shape.
        /// </summary>
        /// <param name="shape">The Shape to manipulate.</param>
        public ShapeConstructor(Shape shape)
        {
            this.shape = shape;
            DecideColor();
            this.shape.block1 = new Block(this.shape.Location, this.shape.Color, this.shape.blockType);
            this.shape.block2 = new Block(this.shape.Location, this.shape.Color, this.shape.blockType);
            this.shape.block3 = new Block(this.shape.Location, this.shape.Color, this.shape.blockType);
            this.shape.block4 = new Block(this.shape.Location, this.shape.Color, this.shape.blockType);
            this.shape.ShapeBlocks.Add(this.shape.block1);
            this.shape.ShapeBlocks.Add(this.shape.block2);
            this.shape.ShapeBlocks.Add(this.shape.block3);
            this.shape.ShapeBlocks.Add(this.shape.block4);
        } // end constructor method ShapeConstructor

        /// <summary>
        /// This method decides what color the shape will have, according to the
        /// shape's ShapeName.
        /// </summary>
        private void DecideColor()
        {
            switch (shape.shapeName)
            {
                case ShapeName.I:
                    shape.Color = Color.SkyBlue;
                    break;
                case ShapeName.J:
                    shape.Color = Color.Blue;
                    break;
                case ShapeName.L:
                    shape.Color = Color.Orange;
                    break;
                case ShapeName.O:
                    shape.Color = Color.Yellow;
                    break;
                case ShapeName.S:
                    shape.Color = Color.Green;
                    break;
                case ShapeName.T:
                    shape.Color = Color.Purple;
                    break;
                case ShapeName.Z:
                    shape.Color = Color.Red;
                    break;
            } // end switch 
        } // end method DecideColor

        /// <summary>
        /// This method arranges the shape's blocks into a formation, depending on the shape's 
        /// ShapeName.
        /// </summary>
        public void FormShape() {
            switch (shape.shapeName) { 
                case ShapeName.I:
                    ConstructIShape();
                    break;
                case ShapeName.J:
                    ConstructJShape();
                    break;
                case ShapeName.L:
                    ConstructLShape();
                    break;
                case ShapeName.O:
                    ConstructOShape();
                    break;
                case ShapeName.S:
                    ConstructSShape();
                    break;
                case ShapeName.T:
                    ConstructTShape();
                    break;
                case ShapeName.Z:
                    ConstructZShape();
                    break;
            } // end switch 
        } // end method FormShape

        /// <summary>
        /// This method forms the shape's blocks into a I-Shape.
        /// </summary>
        private void ConstructIShape()
        {
            /*foreach (Block block in shape.ShapeBlocks)
                block.Color = Color.SkyBlue;*/

            shape.PivotBlock = shape.block2;
            shape.blocksToAssign = new Block[] { shape.block1, shape.block3, shape.block4 };
            shape.blocksToGoFrom = new Block[] { shape.PivotBlock, shape.PivotBlock, shape.block3 };
            shape.connections = new NewBlockLocations[] { 
                NewBlockLocations.Left,
                NewBlockLocations.Right,
                NewBlockLocations.Right
            };
        } // end method ConstructIShape

        /// <summary>
        /// This method forms the shape's blocks into a J-Shape.
        /// </summary>
        private void ConstructJShape()
        {
            /*
            foreach (Block block in shape.ShapeBlocks)
                block.Color = Color.Orange;*/
            shape.PivotBlock = shape.block2;
            shape.blocksToAssign = new Block[] { shape.block1, shape.block3, shape.block4 };
            shape.blocksToGoFrom = new Block[] { shape.PivotBlock, shape.PivotBlock, shape.block3 };
            shape.connections = new NewBlockLocations[] { 
                NewBlockLocations.Top,
                NewBlockLocations.Right,
                NewBlockLocations.Right
            };
        } // end method ConstructJShape

        /// <summary>
        /// This method forms the shape's blocks into a L-Shape.
        /// </summary>
        private void ConstructLShape()
        {
            /*
            foreach (Block block in shape.ShapeBlocks)
                block.Color = Color.Blue;*/
            shape.PivotBlock = shape.block2;
            shape.blocksToAssign = new Block[] { shape.block1, shape.block3, shape.block4 };
            shape.blocksToGoFrom = new Block[] { shape.PivotBlock, shape.PivotBlock, shape.block3 };
            shape.connections = new NewBlockLocations[] { 
                NewBlockLocations.Top,
                NewBlockLocations.Left,
                NewBlockLocations.Left
            };
        } // end method ConstructLShape

        /// <summary>
        /// This method forms the shape's blocks into a O-Shape.
        /// </summary>
        private void ConstructOShape()
        {
            /*
            foreach (Block block in shape.ShapeBlocks)
                block.Color = Color.Yellow;*/
            shape.PivotBlock = shape.block2;
            shape.blocksToAssign = new Block[] { shape.block1, shape.block3, shape.block4 };
            shape.blocksToGoFrom = new Block[] { shape.PivotBlock, shape.PivotBlock, shape.PivotBlock };
            shape.connections = new NewBlockLocations[] { 
                NewBlockLocations.Left,
                NewBlockLocations.Bottom_Left,
                NewBlockLocations.Bottom
            };
        } // end method ConstructOShape

        /// <summary>
        /// This method forms the shape's blocks into a S-Shape.
        /// </summary>
        private void ConstructSShape()
        {
            /*
            foreach (Block block in shape.ShapeBlocks)
                block.Color = Color.Green;*/
            shape.PivotBlock = shape.block2;
            shape.blocksToAssign = new Block[] { shape.block1, shape.block3, shape.block4 };
            shape.blocksToGoFrom = new Block[] { shape.PivotBlock, shape.PivotBlock, shape.PivotBlock };
            shape.connections = new NewBlockLocations[] { 
                NewBlockLocations.Right,
                NewBlockLocations.Bottom,
                NewBlockLocations.Bottom_Left
            };
        } // end method ConstructSShape

        /// <summary>
        /// This method forms the shape's blocks into a T-Shape.
        /// </summary>
        private void ConstructTShape()
        {
            /*
            foreach (Block block in shape.ShapeBlocks)
                block.Color = Color.Purple;*/
            shape.PivotBlock = shape.block3;
            shape.blocksToAssign = new Block[] { shape.block2, shape.block1, shape.block4 };
            shape.blocksToGoFrom = new Block[] { shape.PivotBlock, shape.PivotBlock, shape.PivotBlock };
            shape.connections = new NewBlockLocations[] { 
                NewBlockLocations.Left,
                NewBlockLocations.Top,
                NewBlockLocations.Right
            };   
        } // end method ConstructTShape

        /// <summary>
        /// This method forms the shape's blocks into a Z-Shape.
        /// </summary>
        private void ConstructZShape()
        {
            /*
            foreach (Block block in shape.ShapeBlocks)
                block.Color = Color.Red;*/
            shape.PivotBlock = shape.block2;
            ///shape.rotationIndex = rotationIndex;
            shape.blocksToAssign = new Block[] { shape.block1, shape.block3, shape.block4 };
            shape.blocksToGoFrom = new Block[] { shape.PivotBlock, shape.PivotBlock, shape.PivotBlock };
            shape.connections = new NewBlockLocations[] { 
                NewBlockLocations.Left,
                NewBlockLocations.Bottom,
                NewBlockLocations.Bottom_Right
            };
        } // end method ConstructZShape
    }
}
