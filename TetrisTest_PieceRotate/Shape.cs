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
        public Block pivotBlock;
        public Point Location { get; set; }
        public Color color;
        public Shape(Point location, Color color) { 
            //this.boundaries = boundaries;
            this.Location = location;
            this.color = color;
            ShapeBlocks = new List<Block>(); 
            //ConstructShape();
        }

        //private void ConstructShape()
        //{

        //    Block block1 = new Block(Location, Color.Blue);
        //    Block block2 = new Block(block1.NewBlockRight, Color.Green);
        //    Block block3 = new Block(block2.NewBlockTop, Color.Blue);
        //    Block block4 = new Block(block3.NewBlockTop, Color.Blue);
        //    pivotBlock = block2;
        //    ShapeBlocks.Add(block1);
        //    ShapeBlocks.Add(block2);
        //    ShapeBlocks.Add(block3);
        //    ShapeBlocks.Add(block4);
        //}

        public void Draw(Graphics g) {
            foreach (Block block in ShapeBlocks)
                block.Draw(g);
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
        public virtual bool RotateShape(Shape shape) {
            List<Point> newBlockLocationsList = new List<Point>();
            Point pivotPoint = pivotBlock.Location;
            foreach (Block block in shape.ShapeBlocks)
                newBlockLocationsList.Add(block.Location);
                       

            for (int i = 0; i < newBlockLocationsList.Count; i++)
                if (newBlockLocationsList[i] != pivotPoint)
                    newBlockLocationsList[i] = RotatePoint90DegreesCounterClockWise(newBlockLocationsList[i], pivotPoint);

            /*foreach (Point point in newBlockLocationsList)
            {
                if (WillPassBorder(point)) return false;
            }*/
            for (int i = 0; i < shape.ShapeBlocks.Count; i++)
                shape.ShapeBlocks[i].Location = newBlockLocationsList[i];
            return true;

        }


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
