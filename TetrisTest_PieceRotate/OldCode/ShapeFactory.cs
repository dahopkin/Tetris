using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class ShapeFactory
    {
        /*public Shape GenerateShape(ShapeName shapeName, Point location, BlockType blockType) { 
            Shape shapeToReturn = null;
            switch (shapeName) { 
                case ShapeName.I:
                    shapeToReturn = new Shape_I(location, blockType);
                    break;
                case ShapeName.J:
                    shapeToReturn = new Shape_J(location, blockType);
                    break;
                case ShapeName.L:
                    shapeToReturn = new Shape_L(location, blockType);
                    break;
                case ShapeName.O:
                    shapeToReturn = new Shape_O(location, blockType);
                    break;
                case ShapeName.S:
                    shapeToReturn = new Shape_S(location, blockType);
                    break;
                case ShapeName.T:
                    shapeToReturn = new Shape_T(location, blockType);
                    break;
                case ShapeName.Z:
                    shapeToReturn = new Shape_Z(location, blockType);
                    break;
            }
            return shapeToReturn;
        }

        public Shape GenerateShape(int shapeName, Point location, BlockType blockType)
        {
            return GenerateShape((ShapeName)shapeName, location, blockType);
        }*/

        public Shape GenerateShape(ShapeName shapeName, Point location, BlockType blockType)
        {
            Shape shapeToReturn = new Shape(location, blockType, shapeName);
            /*switch (shapeName)
            {
                case ShapeName.I:
                    shapeToReturn = new Shape(location, blockType, shapeName);
                    break;
                case ShapeName.J:
                    shapeToReturn = new Shape_J(location, blockType);
                    break;
                case ShapeName.L:
                    shapeToReturn = new Shape_L(location, blockType);
                    break;
                case ShapeName.O:
                    shapeToReturn = new Shape_O(location, blockType);
                    break;
                case ShapeName.S:
                    shapeToReturn = new Shape_S(location, blockType);
                    break;
                case ShapeName.T:
                    shapeToReturn = new Shape_T(location, blockType);
                    break;
                case ShapeName.Z:
                    shapeToReturn = new Shape_Z(location, blockType);
                    break;
            }*/
            return shapeToReturn;
        }

    }
}
