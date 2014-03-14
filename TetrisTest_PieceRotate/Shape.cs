using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    abstract class Shape
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
        public Color color;
        public Block block1;
        public Block block2;
        public Block block3;
        public Block block4;
        protected int rotationIndex = 0;
        // Turn the following 3 arrays into a class that takes
        // two blocks and a NewBlockLocation as parameters.
        protected Block[] blocksToAssign;
        protected Block[] blocksToGoFrom;
        protected NewBlockLocations[] connections;
        public Shape(Point location, Color color) { 
            //this.boundaries = boundaries;
            this.Location = location;
            this.color = color;
            this.ShapeBlocks = new List<Block>();
            block1 = new Block(location, color);
            block2 = new Block(location, color);
            block3 = new Block(location, color);
            block4 = new Block(location, color);
            ShapeBlocks.Add(block1);
            ShapeBlocks.Add(block2);
            ShapeBlocks.Add(block3);
            ShapeBlocks.Add(block4);
            //ConstructShape();
        }

       /*public Shape(Shape shape)
        {

           this.ShapeBlocks = new List<Block>(); 
           foreach (Block block in shape.ShapeBlocks)
                this.ShapeBlocks.Add(new Block(block.Location, block.color));
            this.color = ShapeBlocks[0].color;
            this.Location = shape.Location;
            this.PivotBlock = shape.PivotBlock;
            //ConstructShape();
        }*/



        public void Draw(Graphics g) {
            foreach (Block block in ShapeBlocks)
                block.Draw(g);
        }

        //protected abstract void ConstructShape(int shapeIndex);

        public abstract Shape CopyShape();

        public abstract Shape CopyShape(Point location);

        public abstract Shape CopyShape(Point location, int rotationIndex);

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
                    break;
            } // end switch

        }

        public void Move(Direction direction)
        {
            Move(direction, Block.BlockLength);
        }

        public void MoveShapeDown(int distance){
            for (int i = 0; i < ShapeBlocks.Count; i++)
                ShapeBlocks[i].Location = new Point(
                    ShapeBlocks[i].Location.X, ShapeBlocks[i].Location.Y + distance);
        }

        public void MoveShapeLeft(int distance)
        {
            for (int i = 0; i < ShapeBlocks.Count; i++)
                ShapeBlocks[i].Location = new Point(
                    ShapeBlocks[i].Location.X - distance, ShapeBlocks[i].Location.Y);
        }
        
        public void MoveShapeRight(int distance)
        {
            for (int i = 0; i < ShapeBlocks.Count; i++)
                ShapeBlocks[i].Location = new Point(
                    ShapeBlocks[i].Location.X + distance, ShapeBlocks[i].Location.Y);
        }
        // rotate returns false if it the shape can't be rotated.
  /*      public virtual void RotateShape() {
            List<Point> newBlockLocationsList = new List<Point>();
            Point pivotPoint = PivotBlock.Location;
            foreach (Block block in ShapeBlocks)
                newBlockLocationsList.Add(block.Location);
                       
            for (int i = 0; i < newBlockLocationsList.Count; i++)
                if (newBlockLocationsList[i] != pivotPoint)
                    newBlockLocationsList[i] = RotatePoint90DegreesCounterClockWise(newBlockLocationsList[i], pivotPoint);

            for (int i = 0; i < ShapeBlocks.Count; i++)
                ShapeBlocks[i].Location = newBlockLocationsList[i];

        }

        /*
        public void RotateShape(Shape shape)
        {
            List<Point> newBlockLocationsList = new List<Point>();
            Point pivotPoint = pivotBlock.Location;
            foreach (Block block in shape.ShapeBlocks)
                newBlockLocationsList.Add(block.Location);

            for (int i = 0; i < newBlockLocationsList.Count; i++)
                if (newBlockLocationsList[i] != pivotPoint)
                    newBlockLocationsList[i] = RotatePoint90DegreesCounterClockWise(newBlockLocationsList[i], pivotPoint);

            for (int i = 0; i < shape.ShapeBlocks.Count; i++)
                shape.ShapeBlocks[i].Location = newBlockLocationsList[i];

        }*/


        private Point RotatePoint90DegreesCounterClockWise(Point rotatePoint, Point pivot) {
            /*This rotation method is based off of a youtube
             video detailing matrix rotation of a point.
             Link:https://www.youtube.com/watch?v=Atlr5vvdchY 
             * Steps:
             * 1.) Subtract the X'es and Y's of the point to rotate
             * from those of the pivot point this gets you a point
             * representing the relative vector distance between the two points.
             
             * 2.) Multiply the X'es and Y's of relative vector distance by the 
             * "X'es and Y's" of these integer pairs: (0, -1) and (1, 0). Add all
             * results together for Xes and Ys separately. You'll get the vector
             * transformed.
             * 
             * 3.) Add the  X'es and Y's of the vector transformed to those of the
             * pivot point.
             * 
             * The rotate algorithm works, but doesn't uses loops for its matrix
             * multiplication. I have to find out how to do that when one array
             */

            int[,] pivotPointMatrix = new int[2, 1] { { pivot.X }, { pivot.Y } };
            int[,] rotatePointMatrix = new int[2, 1] { { rotatePoint.X }, { rotatePoint.Y } };
            int[,] transformationMatrix = new int[2,2]{ {0,-1},{1,0}};
            int[,] vectorRelativeToPivot = new int[2, 1];
            int[,] vectorTransformed = new int[2, 1];
            Point pointTransformed;
            for (int i = 0; i < vectorRelativeToPivot.GetLength(0); i++)
                for (int j = 0; j < vectorRelativeToPivot.GetLength(1); j++)
                    vectorRelativeToPivot[i, j] = rotatePointMatrix[i, j] - pivotPointMatrix[i, j];          
            
            /*This commented block worked perfectly for the rotation, but I wanted something with loops.
             * vectorTransformed[0, 0] = (transformationMatrix[0, 0] * vectorRelativeToPivot[0, 0])
                + (transformationMatrix[0, 1] * vectorRelativeToPivot[1, 0]);
            vectorTransformed[1, 0] = (transformationMatrix[1, 0] * vectorRelativeToPivot[0, 0])
                + (transformationMatrix[1, 1] * vectorRelativeToPivot[1, 0]);*/

            for (int transformRow = 0; transformRow < vectorTransformed.GetLength(0); transformRow++)
                for (int transformColumn = 0; transformColumn < vectorTransformed.GetLength(1); transformColumn++)
                    for (int k = 0; k < transformationMatrix.GetLength(1); k++)
                        vectorTransformed[transformRow, transformColumn]
                            += transformationMatrix[transformRow, k]
                            * vectorRelativeToPivot[k, transformColumn];


            pointTransformed = new Point(pivot.X + vectorTransformed[0, 0], pivot.Y + vectorTransformed[1, 0]);
                        
            return pointTransformed;

        }

        /*private bool WillPassBorder(Point pointToCheck) {
            if ((pointToCheck.X < boundaries.Left) 
                || (pointToCheck.X > boundaries.Right)
                || (pointToCheck.Y > boundaries.Bottom)) return true;
            return false;
        }*/

       /* private bool WillOverlapAnotherBlock(Point pointToCheck)
        {
             Get the points from all blocks in the shape.
             * If you run the risk of
            Block testBlock = new Block(pointToCheck);
            List<Point> pointToCheckEdges = new List<Point>();
            pointToCheckEdges.Add(testBlock.Location);
            pointToCheckEdges.Add(testBlock.TopRight);
            pointToCheckEdges.Add(testBlock.BottomLeft);
            pointToCheckEdges.Add(testBlock.BottomRight);
            if ((pointToCheck.X < boundaries.Left)
                || (pointToCheck.X > boundaries.Right)
                || (pointToCheck.Y > boundaries.Bottom)) return true;
            return false;
        }*/


    }
}
