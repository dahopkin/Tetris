using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class ShapeFactory
    {
        public Shape GenerateShape(BlockType blockType, Point location) { 
            Shape shapeToReturn = null;
            switch (blockType) { 
                case BlockType.I:
                    shapeToReturn = new Shape_I(location);
                    break;
                case BlockType.J:
                    shapeToReturn = new Shape_J(location);
                    break;
                case BlockType.L:
                    shapeToReturn = new Shape_L(location);
                    break;
                case BlockType.O:
                    shapeToReturn = new Shape_O(location);
                    break;
                case BlockType.S:
                    shapeToReturn = new Shape_S(location);
                    break;
                case BlockType.T:
                    shapeToReturn = new Shape_T(location);
                    break;
                case BlockType.Z:
                    shapeToReturn = new Shape_Z(location);
                    break;
            }
            return shapeToReturn;
        }

        public Shape GenerateShape(int blockType, Point location) {
            return GenerateShape((BlockType)blockType, location);
        }

    }
}
