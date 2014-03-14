using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Grid
    {
        /*This grid class will be where the "Tetris" part of tetris happens. Shapes will 
         * be generated at random and will be fused the pile when they reach it
         * The grid's size will be 10x22
         
         */

        public int gridWidth = 10;
        public int gridHeight = 20;
        public int Score = 0;
        public int LinesCleared = 0;
        private int framesToSkip = 7;
        private int framesSkipped = 0;
        private TimedEffect lockDelay;
        private bool skipLockDelay = false;
        public Pile gridPile;
        public Point shapeGenerationPoint;
        private ShapeFactory shapeFactory = new ShapeFactory();
        public Shape currentShape = null;
        public Shape heldShape = null;
        public Rectangle Boundaries;
        private Size gridSize;
        private Random random;
        private List<Block> gridLines;
        private List<Shape> dropShapes;
        private List<List<Block>> linesToClear;
        //private List<BlockRotateConnection> blockConnections;
        //private Block anchorBlock;
        public event EventHandler GameOver;

        private Point Location { get; set; }
        public Grid(Point location, Random random) {
            this.Location = location;
            this.random = random;
            this.gridPile = new Pile();
            //Grid's size needs to be 10x22
            gridSize = new Size(
                (Block.BlockLength * gridWidth)
                , (Block.BlockLength * gridHeight));
            Boundaries = new Rectangle(Location, gridSize);
            shapeGenerationPoint = new Point((Boundaries.Left)+BlocksOver(4), Boundaries.Top-BlocksOver(1));
            shapeFactory = new ShapeFactory();
            dropShapes = new List<Shape>();
            linesToClear = new List<List<Block>>();
            GenerateBlockGrid();
            lockDelay = new TimedEffect(500, TimeUnit.Milliseconds);
            //blockConnections = new List<BlockRotateConnection>();

        }
        private int BlocksOver(int numberOfBlocksOver) { return numberOfBlocksOver * Block.BlockLength; }

        public void Draw(Graphics g) {
            //Draw black background
            g.FillRectangle(Brushes.Gray, Boundaries);
            //Draw pile and current shape.
            foreach (Block block in gridLines)
                block.Draw(g);
            gridPile.Draw(g);
            DrawNextShape(g);
            DrawHeldShape(g);
            DrawCurrentShape(g);
            DrawScoreAndLinesCleared(g);
        }

        private void DrawScoreAndLinesCleared(Graphics g)
        {
            Point startPoint = new Point(
                Boundaries.Right + BlocksOver(2),
                Boundaries.Top + BlocksOver(12));

            string scoreString = "Score:"+Environment.NewLine + Score.ToString()
                + Environment.NewLine + Environment.NewLine
                + "Lines Cleared: " + Environment.NewLine + LinesCleared.ToString();
            Font scoreFont = new Font("Arial", 12, FontStyle.Bold);
            g.DrawString(scoreString, scoreFont, Brushes.Black, startPoint);
         
        }

        private void DrawNextShape(Graphics g)
        {
            Point startPoint = new Point(
                Boundaries.Right + BlocksOver(2), Boundaries.Top);
            string nextShapeString = "Next Shape:";
            Font nextShapeFont = new Font("Arial", 12, FontStyle.Bold);
            g.DrawString(nextShapeString, nextShapeFont, Brushes.Black, startPoint);
            startPoint.Y += 20;
            Size displaySize = new Size(BlocksOver(6), BlocksOver(4));
            Rectangle background = new Rectangle(startPoint, displaySize);
            g.FillRectangle(Brushes.Black, background);

            if (dropShapes.Count > 0)
            {

                Point shapeDisplayPoint = GetShapeDisplayPoint(dropShapes[0], background);
                Shape displayShape = dropShapes[0].CopyShape(shapeDisplayPoint);
                displayShape.Draw(g); 
            }
        }

        private void DrawHeldShape(Graphics g)
        {
            Point startPoint = new Point(
                Boundaries.Right + BlocksOver(2), Boundaries.Top + BlocksOver(6));
            string nextShapeString = "Held Shape:";
            Font nextShapeFont = new Font("Arial", 12, FontStyle.Bold);
            g.DrawString(nextShapeString, nextShapeFont, Brushes.Black, startPoint);
            startPoint.Y += 20;
            Size displaySize = new Size(BlocksOver(6), BlocksOver(4));
            Rectangle background = new Rectangle(startPoint, displaySize);
            g.FillRectangle(Brushes.Black, background);

            if (heldShape != null)
            {

                Point shapeDisplayPoint = GetShapeDisplayPoint(heldShape, background);
                Shape displayShape = heldShape.CopyShape(shapeDisplayPoint);
                displayShape.Draw(g);
            }
        }

        private Point GetShapeDisplayPoint(Shape shapeToDisplay, Rectangle background) {
            Point shapeDisplayPoint = new Point(0,0);
            if(shapeToDisplay is Shape_I)
                shapeDisplayPoint = new Point(background.Left + BlocksOver(2), background.Top + BlocksOver(1));
            else if (shapeToDisplay is Shape_J)
                shapeDisplayPoint = new Point(background.Left + BlocksOver(1), background.Top + BlocksOver(2));
            else if (shapeToDisplay is Shape_L)
                shapeDisplayPoint = new Point(background.Left + BlocksOver(3), background.Top + BlocksOver(2));
            else if (shapeToDisplay is Shape_O)
                shapeDisplayPoint = new Point(background.Left + BlocksOver(3), background.Top + BlocksOver(1));
            else if (shapeToDisplay is Shape_S)
                shapeDisplayPoint = new Point(background.Left + BlocksOver(2), background.Top + BlocksOver(1));
            else if (shapeToDisplay is Shape_T)
                shapeDisplayPoint = new Point(background.Left + BlocksOver(2), background.Top + BlocksOver(2));
            else if (shapeToDisplay is Shape_Z)
                shapeDisplayPoint = new Point(background.Left + BlocksOver(2), background.Top + BlocksOver(1));
            return shapeDisplayPoint;
        }

        public void OnGameOver(EventArgs e)
        {
            EventHandler gameOver = GameOver;
            if (gameOver != null)
                gameOver(this, e);
        } // end method OnGameOver

        private void DrawCurrentShape(Graphics g)
        {
            if (currentShape != null)
            {
                foreach (Block block in currentShape.ShapeBlocks) {
                    if (block.Location.Y >= Boundaries.Top)
                        block.Draw(g);
                }
            }
        }

        /*This method progresses all the grid actions along. The current shape is moved
         and collisions are processed.*/
        public void Go()
        {
            foreach (Block block in gridPile.PileBlocks)
                if (block.IsClearing && !block.HasCleared) return;
            if (PileReachedTheTop()) OnGameOver(new EventArgs());
            CheckToInsertNextShape();
            CheckToGenerateNewCurrentShape();
            AutoMoveDown();
            CheckToAddShapeToPile();
            MarkLines();
            ClearLines();
        }

        private void AutoMoveDown()
        {
            if (++framesSkipped < framesToSkip) return;
            else
            {
                MoveCurrentShape(Direction.Down);
                framesSkipped = 0;
            }
        }

        /*This method moves the current shape if it can move without going past
         the boundary border or overlapping with a block on the pile.*/
        //public void MoveCurrentShape(Direction directionToMove)
        //{
        //    if (currentShape != null)
        //    {
        //         copy the shape to test it.
        //        Shape currentShapeCopy = currentShape.CopyShape();
        //         rotate the copied shape.
        //        currentShapeCopy.Move(directionToMove);
        //        /*If the copied, moved shape is not past the boundary/overlapping a 
        //         pile block, move the actual shape.*/
        //        if (!WouldOverlapGridBlock(currentShapeCopy)
        //            && !WouldPassLeftOrRightBorder(currentShapeCopy))
        //            currentShape = currentShapeCopy;
        //    }
        //}

        /*This method moves the current shape if it can move without going past
         the boundary border or overlapping with a block on the pile.*/
        public void MoveCurrentShape(Direction directionToMove)
        {
            if (currentShape != null)
            {
                // copy the shape to test it.
                //Shape currentShapeCopy = currentShape.CopyShape();
                Shape currentShapeCopy = currentShape.CopyShape();
                // move the copied shape.
                switch (directionToMove)
                {
                    case Direction.Right:
                        currentShapeCopy.Move(directionToMove, ShortestDistanceFromRight(currentShapeCopy));
                        break;
                    case Direction.Left:
                        currentShapeCopy.Move(directionToMove, ShortestDistanceFromLeft(currentShapeCopy));
                        break;
                    case Direction.Down:
                        currentShapeCopy.Move(directionToMove, ShortestDistanceFromBottom(currentShapeCopy));
                        break;
                }
                /*If the copied, moved shape is not past the boundary/overlapping a 
                 pile block, move the actual shape.*/
                if (!WouldOverlapGridBlock(currentShapeCopy)
                    && !WouldPassLeftOrRightBorder(currentShapeCopy))
                    currentShape = currentShapeCopy;
            }
        }

        public void HardDrop() {
            if (currentShape != null)
            {
                skipLockDelay = true;
                while (ShapeCanMoveDown())
                    MoveCurrentShape(Direction.Down);
            }
        }

        public void HoldShape() {
            // if there is a shape to hold...
            if (currentShape != null)
            {
                /*if there's already a held shape, swap it out with the current one.*/
                if (heldShape != null)
                {
                    Shape tempShape = currentShape.CopyShape();
                    currentShape = heldShape.CopyShape(shapeGenerationPoint);
                    heldShape = tempShape;
                }
                /*If there isn't a held shape, store the current one.*/
                else {
                    heldShape = currentShape;
                    currentShape = null;
                }
            }
        }
        /*This method rotates the current shape if it can rotate without going past
         the boundary border or overlapping with a block on the pile.*/
        public void RotateCurrentShape()
        {
            if (currentShape != null)
            {
                // copy the shape to test it.
                Shape currentShapeCopy = currentShape.CopyShape();
                // rotate the copied shape.
                currentShapeCopy.RotateShape();
                //RotateShape(currentShapeCopy);
                /*If the copied, moved shape is not past the boundary/overlapping a 
                 pile block, move the actual shape.*/
                if (!WouldOverlapGridBlock(currentShapeCopy) && !WouldPassLeftOrRightBorder(currentShapeCopy))
                    currentShape = currentShapeCopy; 
                    //currentShape.RotateShape();
            }

        }

        //This method checks to see if the current shape has collided with the pile.
        private bool IsOnTopOfPile(Shape shapeToTest)
        {
            if (shapeToTest != null)
            {
                foreach (Block shapeBlock in shapeToTest.ShapeBlocks)
                {
                    if (gridPile.PileBlocks.Count > 0)
                    {
                        /*Get all pileBlocks with the same X coordinate as the current shapeBlock.
                         */
                        var pileColumn =
                            from block in gridPile.PileBlocks
                            orderby block.Location.Y
                            where block.Location.X == shapeBlock.Location.X
                            select block;

                        /*If the top edge of the top block of the selected column is equal
                         to the bottom edge of the shape block*/
                        if (pileColumn.Count() > 0)
                        {
                            foreach(Block block in pileColumn)
                                if (shapeBlock.Location == block.NewBlockTop) return true;
                        }
                    }
                } 
            }

            return false;
        }

        /*This method checks to see if a temporary shape would pass the left or right boundaries of the grid.*/
        private bool WouldPassLeftOrRightBorder(Shape shapeToTest) {
            foreach (Block block in shapeToTest.ShapeBlocks)
                if (block.Location.X < Boundaries.Left
                    || block.Area.Right > Boundaries.Right) return true;
            return false;
        }

        /*This method checks to see if a shape is touching the bottom
         * of the grid.*/
        private bool IsTouchingBottom(Shape shapeToTest)
        {
            foreach (Block block in shapeToTest.ShapeBlocks)
                if (block.Area.Bottom == Boundaries.Bottom) return true;
            return false;
        }

        /*This method checks to see if the current shape is overlapping a block on the pile.*/
        private bool WouldOverlapGridBlock(Shape shapeToTest)
        {
            foreach (Block shapeBlock in shapeToTest.ShapeBlocks)
            {
                //List<Point> collisionPoints = shapeBlock.AllCollisionPoints;
                foreach (Block gridBlock in gridPile.PileBlocks)
                    if(shapeBlock.Location == gridBlock.Location) return true;
                    //foreach(Point point in collisionPoints)
                        //if(gridBlock.Area.Contains(point)) return true;
            }
            return false;
        }

        /*This method checks to see if the pile of blocks on the grid has built up to the top
         of the grid. If so, the game is over.*/
        private bool PileReachedTheTop()
        {
            foreach (Block block in gridPile.PileBlocks)
                if (block.Location.Y <= Boundaries.Top) return true;
            return false;
        }

        /*This method checks to see if it's right to add the Shape to the pile*/
        private void CheckToAddShapeToPile() {
            if (currentShape != null) {
                if (!ShapeCanMoveDown())
                {
                    if (HasLines()) skipLockDelay = true;
                    if(!skipLockDelay)lockDelay.Start();

                    if (skipLockDelay || lockDelay.Expired())
                    {
                        gridPile.AddShapeToPile(currentShape);
                        Score += 17;
                        currentShape = null;
                        if (skipLockDelay) skipLockDelay = false;
                    }
                }
            }
        }

       
        private bool ShapeCanMoveDown()
        {
            return (!IsOnTopOfPile(currentShape) && !IsTouchingBottom(currentShape));
        }

        /*This method generates a shape to send from the top of the grid*/
        private void CheckToGenerateNewCurrentShape() { 
            //CheckToInsertNextShape();
            if(currentShape == null){
                //currentShape = new Shape_I(shapeGenerationPoint);
                //currentShape = shapeFactory.GenerateShape(random.Next(7), shapeGenerationPoint);
                currentShape = dropShapes[0];
                dropShapes.Remove(currentShape);
            }
        }

        private void CheckToInsertNextShape()
        {
            if (dropShapes.Count == 0)
            {
                int randomShapeNumber = random.Next(7);
                Shape nextShape = shapeFactory.GenerateShape(randomShapeNumber, shapeGenerationPoint);
                //Shape nextShape = new Shape_I(shapeGenerationPoint);
                //Shape nextShape = new Shape_J(shapeGenerationPoint);
                //Shape nextShape = new Shape_L(shapeGenerationPoint);
                //Shape nextShape = new Shape_O(shapeGenerationPoint);
                //Shape nextShape = new Shape_S(shapeGenerationPoint);
                //Shape nextShape = new Shape_T(shapeGenerationPoint);
                //Shape nextShape = new Shape_Z(shapeGenerationPoint);
                dropShapes.Add(nextShape);
            }
        }

        private int ShortestDistanceFromLeft(Shape shapeToTest) {
            int minDistanceFromLeftBorder = shapeToTest.ShapeBlocks[0].Area.Left - Boundaries.Left;
            foreach (Block block in shapeToTest.ShapeBlocks)
            {
                int currentMinDist = block.Area.Left - Boundaries.Left;
                if (currentMinDist <= minDistanceFromLeftBorder)
                    minDistanceFromLeftBorder = currentMinDist;
            }
            if (minDistanceFromLeftBorder > Block.BlockLength) return Block.BlockLength;
            else return minDistanceFromLeftBorder;

        }

        private int ShortestDistanceFromRight(Shape shapeToTest)
        {
            int minDistanceFromRightBorder = Boundaries.Right - shapeToTest.ShapeBlocks[0].Area.Right;
            foreach (Block block in shapeToTest.ShapeBlocks)
            {
                int currentMinDist = Boundaries.Right - block.Area.Right;
                if (currentMinDist <= minDistanceFromRightBorder)
                    minDistanceFromRightBorder = currentMinDist;
            }
            if (minDistanceFromRightBorder > Block.BlockLength) return Block.BlockLength;
            else return minDistanceFromRightBorder;

        }

        private int ShortestDistanceFromBottom(Shape shapeToTest)
        {
            int minDistanceFromBottom = Boundaries.Bottom - shapeToTest.ShapeBlocks[0].Area.Bottom;
            foreach (Block block in shapeToTest.ShapeBlocks)
            {
                int currentMinDist = Boundaries.Bottom - block.Area.Bottom;
                if (currentMinDist <= minDistanceFromBottom)
                    minDistanceFromBottom = currentMinDist;
            }
            if (minDistanceFromBottom > Block.BlockLength) return Block.BlockLength;
            else return minDistanceFromBottom;

        }
        //This method generates a grid of transparent blocks.
        private void GenerateBlockGrid() { 
            //Make a list to hold all the blocks.
            gridLines = new List<Block>();
            //Start at the upper left of the boundaries
            Point startPoint = new Point(Boundaries.Left, Boundaries.Top);
            /*Create a nested for loop that generates the 
             board's width in transparent blocks across and
             the board's height in transparent blocks down.*/
            for (int i = 0; i < gridHeight; i++)
            {
                for (int j = 0; j < gridWidth; j++)
                {
                    gridLines.Add(new Block(new Point((startPoint.X + j * Block.BlockLength)
                        , (startPoint.Y + i * Block.BlockLength))));
                }
            }
        }

        //private void MarkLines()
        //{
        //    //Get a grouping of all blocks according to their Y coordinate
        //    var lineGroupings =
        //        from block in gridPile.PileBlocks
        //        orderby block.Location.X
        //        group block by block.Location.Y
        //            into linesToClearGroup
        //            select linesToClearGroup;

        //    foreach (var group in lineGroupings)
        //    {
        //        if (group.Count() == gridWidth)
        //        {
        //            //get Y coord of first block in this line for later.
        //            //int moveDownAllAbove = group.First().Area.Top;
        //            foreach (Block block in group)
        //                //gridPile.PileBlocks.Remove(block);
        //                block.IsClearing = true;
        //            //ClearLines(moveDownAllAbove);

        //        }
        //    }
        //}

        

        private bool HasLines() {
            bool hasLines = false;
            if (linesToClear.Count > 0) hasLines = true;
            else
            {
                //List<Block> allBlocksOnScreen = new List<Block>();
                //allBlocksOnScreen.AddRange(gridPile.PileBlocks);
                //if(currentShape != null)
                    //allBlocksOnScreen.AddRange(currentShape.ShapeBlocks);
                var lineGroupings =
                        from block in gridPile.PileBlocks
                        orderby block.Location.X
                        group block by block.Location.Y
                            into linesToClearGroup
                            select linesToClearGroup;

                foreach (var group in lineGroupings){
                    if (group.Count() == gridWidth)
                    {
                        linesToClear.Add(group.ToList());
                        if (!hasLines) hasLines = true;
                    }
                }
            }
            return hasLines;
        }
        
        private void MarkLines()
        {   
                if (HasLines())
                {
                    foreach (List<Block> blockList in linesToClear)
                        foreach(Block block in blockList)
                            if(!block.IsClearing) block.IsClearing = true;
                }
        }

/*        private void ClearLines()
        {
            // select all marked blocks.
            var markedBlocks =
               from block in gridPile.PileBlocks
               where block.HasCleared
               group block by block.Location.Y
                   into blockGroups
                   select blockGroups;

            if (markedBlocks.Count() > 0)
            {
                int linesToClear = markedBlocks.Count();
                AddLinesToScore(linesToClear);
                AddToLinesCleared(linesToClear);

                foreach (var group in markedBlocks)
                {
                    int moveDownAllAbove = group.First().Location.Y;
                    var blocksAbove =
                        from block in gridPile.PileBlocks
                        where block.Area.Top < moveDownAllAbove
                        select block;

                    foreach (Block block in blocksAbove)
                        block.Location = new Point(block.Location.X, block.Location.Y + Block.BlockLength);

                    foreach (Block block in group)
                        gridPile.PileBlocks.Remove(block);
                }
            }
        }*/

        private void ClearLines()
        {

            if (linesToClear.Count > 0)
            {
                //Return if blocks that are flashing aren't done yet.
                foreach (List<Block> blockList in linesToClear)
                    foreach(Block block in blockList)
                        if (!block.HasCleared) return;

                int numberOfLines = linesToClear.Count();
                AddLinesToScore(numberOfLines);
                AddToLinesCleared(numberOfLines);

                for (int i = linesToClear.Count - 1; i >= 0; i--)
                {
                    int moveDownAllAbove = linesToClear[i][0].Location.Y;
                    var blocksAbove =
                            from block in gridPile.PileBlocks
                            where block.Area.Top < moveDownAllAbove
                            select block;

                        foreach (Block block in blocksAbove)
                            block.Location = new Point(block.Location.X, block.Location.Y + Block.BlockLength);

                    for (int j = linesToClear[i].Count - 1; j >= 0; j--)
                    {
                        gridPile.PileBlocks.Remove(linesToClear[i][j]);
                    }
                    linesToClear.Remove(linesToClear[i]);
                 }
                
            }
        }

        private void AddToLinesCleared(int numberOfLines)
        {
            LinesCleared += numberOfLines;
        }

        private void AddLinesToScore(int numberOfLines)
        {
            
            switch (numberOfLines) { 
                case 1:
                    Score += 40;
                    break;
                case 2:
                    Score += 100;
                    break;
                case 3:
                    Score += 300;
                    break;
                case 4:
                    Score += 1200;
                    break;
                default:
                    break;
            }
        }
    }
}
