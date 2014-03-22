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
        }

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
            }
        }

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
            }
            foreach (Block block in shape.ShapeBlocks)
                block.DecideBlockType(shape.blockType);
        }
        public void ConstructIShape() {
            foreach (Block block in shape.ShapeBlocks)
                block.Color = Color.SkyBlue;

            shape.PivotBlock = shape.block2;
            shape.blocksToAssign = new Block[] { shape.block1, shape.block3, shape.block4 };
            shape.blocksToGoFrom = new Block[] { shape.PivotBlock, shape.PivotBlock, shape.block3 };
            shape.connections = new NewBlockLocations[] { 
                NewBlockLocations.Left,
                NewBlockLocations.Right,
                NewBlockLocations.Right
            };
        }

        public void ConstructJShape()
        {
            foreach (Block block in shape.ShapeBlocks)
                block.Color = Color.Orange;
            shape.PivotBlock = shape.block2;
            shape.blocksToAssign = new Block[] { shape.block1, shape.block3, shape.block4 };
            shape.blocksToGoFrom = new Block[] { shape.PivotBlock, shape.PivotBlock, shape.block3 };
            shape.connections = new NewBlockLocations[] { 
                NewBlockLocations.Top,
                NewBlockLocations.Right,
                NewBlockLocations.Right
            };
            //ConstructShape(rotationIndex);
        }

        public void ConstructLShape()
        {
            foreach (Block block in shape.ShapeBlocks)
                block.Color = Color.Blue;
            shape.PivotBlock = shape.block2;
            shape.blocksToAssign = new Block[] { shape.block1, shape.block3, shape.block4 };
            shape.blocksToGoFrom = new Block[] { shape.PivotBlock, shape.PivotBlock, shape.block3 };
            shape.connections = new NewBlockLocations[] { 
                NewBlockLocations.Top,
                NewBlockLocations.Left,
                NewBlockLocations.Left
            };
            //ConstructShape(rotationIndex);
        }

        public void ConstructOShape()
        {
            foreach (Block block in shape.ShapeBlocks)
                block.Color = Color.Yellow;
            shape.PivotBlock = shape.block2;
            shape.blocksToAssign = new Block[] { shape.block1, shape.block3, shape.block4 };
            shape.blocksToGoFrom = new Block[] { shape.PivotBlock, shape.PivotBlock, shape.PivotBlock };
            shape.connections = new NewBlockLocations[] { 
                NewBlockLocations.Left,
                NewBlockLocations.Bottom_Left,
                NewBlockLocations.Bottom
            };
        }

        public void ConstructSShape()
        {
            foreach (Block block in shape.ShapeBlocks)
                block.Color = Color.Green;
            shape.PivotBlock = shape.block2;
            shape.blocksToAssign = new Block[] { shape.block1, shape.block3, shape.block4 };
            shape.blocksToGoFrom = new Block[] { shape.PivotBlock, shape.PivotBlock, shape.PivotBlock };
            shape.connections = new NewBlockLocations[] { 
                NewBlockLocations.Right,
                NewBlockLocations.Bottom,
                NewBlockLocations.Bottom_Left
            };
        }

        public void ConstructTShape()
        {
            foreach (Block block in shape.ShapeBlocks)
                block.Color = Color.Purple;
            shape.PivotBlock = shape.block3;
            shape.blocksToAssign = new Block[] { shape.block2, shape.block1, shape.block4 };
            shape.blocksToGoFrom = new Block[] { shape.PivotBlock, shape.PivotBlock, shape.PivotBlock };
            shape.connections = new NewBlockLocations[] { 
                NewBlockLocations.Left,
                NewBlockLocations.Top,
                NewBlockLocations.Right
            };
            
        }

        public void ConstructZShape()
        {
            foreach (Block block in shape.ShapeBlocks)
                block.Color = Color.Red;
            shape.PivotBlock = shape.block2;
            ///shape.rotationIndex = rotationIndex;
            shape.blocksToAssign = new Block[] { shape.block1, shape.block3, shape.block4 };
            shape.blocksToGoFrom = new Block[] { shape.PivotBlock, shape.PivotBlock, shape.PivotBlock };
            shape.connections = new NewBlockLocations[] { 
                NewBlockLocations.Left,
                NewBlockLocations.Bottom,
                NewBlockLocations.Bottom_Right
            };
            //ConstructShape(rotationIndex);
        }
    }
}
