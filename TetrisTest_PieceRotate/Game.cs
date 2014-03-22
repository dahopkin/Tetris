using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    class Game
    {
        /*This grid class will be where the "Tetris" part of tetris happens. Shapes will 
         * be generated at random and will be fused the pile when they reach it
         * The grid's size will be 10x22
         */
        //Grid's size needs to be 10x20
        public int gridWidth = 10;
        public int gridHeight = 20;
        public int Score = 0;
        public int Level = 1;
        public int LinesCleared = 0;
        public BlockType BlockType;
        private int framesToSkip = 7;
        private int framesSkipped = 0;
        private int levelLineScore = 0;
        private int raiseLevelLineScore = 25;
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
        public event EventHandler GameOver;


        /// <summary>
        /// Gets or sets the location of the gaming grid.
        /// </summary>
        private Point Location { get; set; }

        /// <summary>
        /// Instantiates an instance of the Game class from a Location point, a random, and a BlockType enum.
        /// </summary>
        /// <param name="location">The location of the gaming grid.</param>
        /// <param name="random">A random to use for the game's functions that use random.</param>
        /// <param name="BlockType">The type of blocks that will be generated in the game.</param>
        public Game(Point location, Random random, BlockType BlockType) {
            this.Location = location;
            this.random = random;
            this.gridPile = new Pile();
            gridSize = new Size(
                (Block.BlockLength * gridWidth)
                , (Block.BlockLength * gridHeight));
            Boundaries = new Rectangle(Location, gridSize);
            shapeGenerationPoint = new Point((Boundaries.Left)+BlocksOver(4), Boundaries.Top-BlocksOver(1));
            shapeFactory = new ShapeFactory();
            dropShapes = new List<Shape>();
            linesToClear = new List<List<Block>>();
            GenerateBackgroundBlockGrid();
            lockDelay = new TimedEffect(500, TimeUnit.Milliseconds);
            this.BlockType = BlockType;
        }

        /// <summary>
        /// This method progresses all the game actions along. The current shape is moved 
        /// and collisions are processed.
        /// </summary>
        public void Go()
        {
            foreach (Block block in gridPile.PileBlocks)
                if (block.IsFlashing()) return;
            if (PileReachedTheTop()) OnGameOver(new EventArgs());
            CheckToInsertNextShape();
            CheckToGenerateNewCurrentShape();
            AutoMoveDown();
            CheckToAddShapeToPile();
            FlashLines();
            ClearLines();
            CheckToRaiseLevel();
        }

        private int BlocksOver(int numberOfBlocksOver) { return numberOfBlocksOver * Block.BlockLength; }

        public void OnGameOver(EventArgs e)
        {
            EventHandler gameOver = GameOver;
            if (gameOver != null)
                gameOver(this, e);
        } // end method OnGameOver

        #region Drawing Methods

        /// <summary>
        /// This method draws all of the game's elements onto the screen.
        /// </summary>
        /// <param name="g">The graphics object to draw onto.</param>
        public void Draw(Graphics g)
        {
            //Draw black background
            g.FillRectangle(Brushes.Gray, Boundaries);
            g.DrawRectangle(Pens.Black, Boundaries);
            //Draw pile and current shape.
            foreach (Block block in gridLines)
                block.Draw(g);
            gridPile.Draw(g);
            DrawNextShape(g);
            DrawHeldShape(g);
            DrawCurrentShape(g);
            DrawStats(g);
        }

        /// <summary>
        /// This method draws the Score, Lines Cleared, and Level onto the screen.
        /// </summary>
        /// <param name="g">The graphics object to draw onto.</param>
        private void DrawStats(Graphics g)
        {
            Point startPoint = new Point(
                Boundaries.Right + BlocksOver(2),
                Boundaries.Top + BlocksOver(12));

            string scoreString = "Score:" + Environment.NewLine + Score.ToString()
                + Environment.NewLine + Environment.NewLine
                + "Lines Cleared: " + Environment.NewLine + LinesCleared.ToString()
                + Environment.NewLine + Environment.NewLine
                + "Level: " + Environment.NewLine + Level.ToString();
            Font scoreFont = new Font("Arial", 12, FontStyle.Bold);
            g.DrawString(scoreString, scoreFont, Brushes.Black, startPoint);

        }

        /// <summary>
        /// This method draws the next shape on the form.
        /// </summary>
        /// <param name="g">The graphics object to draw onto.</param>
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

        /// <summary>
        /// This method draws the shape being held on the form.
        /// </summary>
        /// <param name="g">The graphics object to draw onto.</param>
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

        /// <summary>
        /// This method draws the current moving shape on the form.
        /// </summary>
        /// <param name="g">The graphics object to draw onto.</param>
        private void DrawCurrentShape(Graphics g)
        {
            if (currentShape != null)
            {
                foreach (Block block in currentShape.ShapeBlocks)
                {
                    if (block.Location.Y >= Boundaries.Top)
                        block.Draw(g);
                }
            }
        }

        /// <summary>
        /// This method uses a Shape and a Rectangle background to 
        /// return the point to place the shape against the background.
        /// </summary>
        /// <param name="shapeToDisplay">The Shape to get the point for.</param>
        /// <param name="background">The rectangle background </param>
        /// <returns>A point where to put the shape on the tiny display grid.</returns>
        private Point GetShapeDisplayPoint(Shape shapeToDisplay, Rectangle background)
        {
            Point shapeDisplayPoint = new Point(0, 0);
            if (shapeToDisplay.shapeName == ShapeName.I)
                shapeDisplayPoint = new Point(background.Left + BlocksOver(2), background.Top + BlocksOver(1));
            //else if (shapeToDisplay is Shape_J)
            else if (shapeToDisplay.shapeName == ShapeName.J)
                shapeDisplayPoint = new Point(background.Left + BlocksOver(1), background.Top + BlocksOver(2));
            //else if (shapeToDisplay is Shape_L)
            else if (shapeToDisplay.shapeName == ShapeName.L)
                shapeDisplayPoint = new Point(background.Left + BlocksOver(3), background.Top + BlocksOver(2));
            //else if (shapeToDisplay is Shape_O)
            else if (shapeToDisplay.shapeName == ShapeName.O)
                shapeDisplayPoint = new Point(background.Left + BlocksOver(3), background.Top + BlocksOver(1));
            //else if (shapeToDisplay is Shape_S)
            else if (shapeToDisplay.shapeName == ShapeName.S)
                shapeDisplayPoint = new Point(background.Left + BlocksOver(2), background.Top + BlocksOver(1));
            //else if (shapeToDisplay is Shape_T)
            else if (shapeToDisplay.shapeName == ShapeName.T)
                shapeDisplayPoint = new Point(background.Left + BlocksOver(2), background.Top + BlocksOver(2));
            //else if (shapeToDisplay is Shape_Z)
            else if (shapeToDisplay.shapeName == ShapeName.Z)
                shapeDisplayPoint = new Point(background.Left + BlocksOver(2), background.Top + BlocksOver(1));
            return shapeDisplayPoint;
        }

        /// <summary>
        /// This method generates a grid of transparent blocks for a background.
        /// </summary>
        private void GenerateBackgroundBlockGrid()
        {
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
                    gridLines.Add(new Block(
                        new Point((startPoint.X + j * Block.BlockLength)
                        , (startPoint.Y + i * Block.BlockLength))));
                }
            }
        }

        #endregion

        #region Movement Processing Methods
        /// <summary>
        /// This method moves the shape falling down on the screen left, right, or down, 
        /// after checking to see if the move is possible.
        /// </summary>
        /// <param name="directionToMove">The direction to move</param>
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

        /// <summary>
        /// This method moves the shape down automatically. It keeps the game in motion.
        /// </summary>
        private void AutoMoveDown()
        {
            if (++framesSkipped < framesToSkip) return;
            else
            {
                MoveCurrentShape(Direction.Down);
                framesSkipped = 0;
            }
        }

        /// <summary>
        /// This method drops the shape onto the pile.
        /// </summary>
        public void HardDrop()
        {
            if (currentShape != null)
            {
                skipLockDelay = true;
                while (ShapeCanMoveDown())
                    MoveCurrentShape(Direction.Down);
            }
        }

        /// <summary>
        /// This method returns the shortest distance between a block in the falling shape and 
        /// a block on the pile to the left, or the left side of the grid.
        /// </summary>
        /// <param name="shapeToTest">The Shape being tested</param>
        /// <returns>The shortest distance between a collision point.</returns>
        private int ShortestDistanceFromLeft(Shape shapeToTest)
        {
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

        /// <summary>
        /// This method returns the shortest distance between a block in the falling shape and 
        /// a block on the pile to the right, or the right side of the grid.
        /// </summary>
        /// <param name="shapeToTest">The Shape being tested</param>
        /// <returns>The shortest distance between a collision point.</returns>
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

        /// <summary>
        /// This method returns the shortest distance between a block in the falling shape and 
        /// the bottom of the grid.
        /// </summary>
        /// <param name="shapeToTest">The Shape being tested</param>
        /// <returns>The shortest distance between a collision point.</returns>
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

        /// <summary>
        /// This method rotates the current shape if it can rotate without going past
        /// the boundary border or overlapping with a block on the pile.
        /// </summary>
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
        #endregion

        #region Collision Detection Methods
        /*Note: all collision detection methods relating to movement are designed to test unseen shapes 
         * (i.e. where the shape WOULD be if it moved)*/

        /// <summary>
        /// This method checks to see if a temporary shape has collided with the pile.
        /// </summary>
        /// <param name="shapeToTest">The unseen shape to check against the pile for collisions.</param>
        /// <returns>A boolean indicating whether or not the unseen shape is touching the pile.</returns>
        private bool IsTouchingPile(Shape shapeToTest)
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
                            foreach (Block block in pileColumn)
                                if (shapeBlock.Location == block.NewBlockTop) return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// This method checks to see if a temporary shape is past the left or right border.
        /// </summary>
        /// <param name="shapeToTest">The unseen shape to check against the borders for collisions.</param>
        /// <returns>A boolean indicating whether or not the unseen shape is touching left or right border.</returns>
        private bool WouldPassLeftOrRightBorder(Shape shapeToTest)
        {
            foreach (Block block in shapeToTest.ShapeBlocks)
                if (block.Location.X < Boundaries.Left
                    || block.Area.Right > Boundaries.Right) return true;
            return false;
        }

        /// <summary>
        /// This method checks to see if a temporary shape is touching the bottom of the grid.
        /// </summary>
        /// <param name="shapeToTest">The unseen shape to check against the bottom.</param>
        /// <returns>A boolean indicating whether or not the unseen shape is touching the bottom.</returns>
        private bool IsTouchingBottom(Shape shapeToTest)
        {
            foreach (Block block in shapeToTest.ShapeBlocks)
                if (block.Area.Bottom == Boundaries.Bottom) return true;
            return false;
        }

        /// <summary>
        /// This method checks to see if a temporary shape is overlapping a block on the pile.
        /// </summary>
        /// <param name="shapeToTest">The unseen shape to check against the pile blocks for breaches.</param>
        /// <returns>A boolean indicating whether or not the unseen shape is overlapping a pile block.</returns>
        private bool WouldOverlapGridBlock(Shape shapeToTest)
        {
            foreach (Block shapeBlock in shapeToTest.ShapeBlocks)
            {
                //List<Point> collisionPoints = shapeBlock.AllCollisionPoints;
                foreach (Block gridBlock in gridPile.PileBlocks)
                    if (shapeBlock.Location == gridBlock.Location) return true;
                //foreach(Point point in collisionPoints)
                //if(gridBlock.Area.Contains(point)) return true;
            }
            return false;
        }

        /// <summary>
        /// This method checks to see if the pile of non-moving, uncleared blocks has reached the 
        /// top of the grid.
        /// </summary>
        /// <returns>A boolean indicating whether or not the pile has reached the top.</returns>
        private bool PileReachedTheTop()
        {
            foreach (Block block in gridPile.PileBlocks)
                if (block.Location.Y <= Boundaries.Top) return true;
            return false;
        }

        /// <summary>
        /// This method runs several checks to see if the current shape can move down any further.
        /// </summary>
        /// <returns>A boolean indicating whether or not the current shape can move down.</returns>
        private bool ShapeCanMoveDown()
        {
            return (!IsTouchingPile(currentShape) && !IsTouchingBottom(currentShape));
        }

        #endregion

        #region Block Line Processing Methods

        /// <summary>
        /// This method checks to see if there is currently a line or more on the screen to clear.
        /// </summary>
        /// <returns>A bool representing whether or not there are lines.</returns>
        private bool HasLines()
        {
            bool hasLines = false;
            if (linesToClear.Count > 0) hasLines = true;
            else
            {
                var lineGroupings =
                        from block in gridPile.PileBlocks
                        orderby block.Location.X
                        group block by block.Location.Y
                            into linesToClearGroup
                            select linesToClearGroup;

                foreach (var group in lineGroupings)
                {
                    if (group.Count() == gridWidth)
                    {
                        linesToClear.Add(group.ToList());
                        if (!hasLines) hasLines = true;
                    } // end if 
                } // end foreach 
            } // end else 
            return hasLines;
        } // end method HasLines

        /// <summary>
        /// This method marks lines of blocks for later removal by triggering their flash animation.
        /// </summary>
        private void FlashLines()
        {
            if (HasLines())
            {
                foreach (List<Block> blockList in linesToClear)
                    foreach (Block block in blockList)
                        if (!block.IsFlashing()) block.StartFlashing();
            }
        }

        /// <summary>
        /// This method removes lines of blocks from the pile.
        /// </summary>
        private void ClearLines()
        {

            if (linesToClear.Count > 0)
            {
                //Return if blocks that are flashing aren't done yet.
                foreach (List<Block> blockList in linesToClear)
                    foreach (Block block in blockList)
                        if (!block.DoneFlashing()) return;

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

                    //for (int j = linesToClear[i].Count - 1; j >= 0; j--)
                    //{
                      //  gridPile.PileBlocks.Remove(linesToClear[i][j]);
                    //}
                    /*There's no particular reason to use the RemoveBlocksFromPile
                     method over the for loop right above. I just had that method
                     baked into the class and decided to use it in case 
                     the protocol for removing lines change.*/
                    gridPile.RemoveBlocksFromPile(linesToClear[i]);
                    linesToClear.Remove(linesToClear[i]);
                }

            }
        }

        /// <summary>
        /// This adds the number of lines that were cleared to the Lines Cleared variable.
        /// </summary>
        /// <param name="numberOfLines">Number of lines to add.</param>
        private void AddToLinesCleared(int numberOfLines)
        {
            LinesCleared += numberOfLines;
        }

        /// <summary>
        /// This adds the number of lines that were cleared to the Score variable.
        /// </summary>
        /// <param name="numberOfLines">Number of lines to add.</param>
        private void AddLinesToScore(int numberOfLines)
        {

            switch (numberOfLines)
            {
                case 1:
                    Score += 40;
                    levelLineScore += 1;
                    break;
                case 2:
                    Score += 100;
                    levelLineScore += 2;
                    break;
                case 3:
                    Score += 300;
                    levelLineScore += 3;
                    break;
                case 4:
                    Score += 1200;
                    levelLineScore += 4;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// This method raises the level and speeds up the game if the player has cleared a certain number of lines.
        /// </summary>
        private void CheckToRaiseLevel()
        {
            if (levelLineScore >= raiseLevelLineScore)
            {
                levelLineScore -= raiseLevelLineScore;
                framesToSkip--;
                if (Level < 7) Level++;
            }
        }

        #endregion

        #region "Go To Next Shape" Methods
        /// <summary>
        /// This method will add a shape to the pile
        /// </summary>
        private void CheckToAddShapeToPile()
        {
            if (currentShape != null)
            {
                if (!ShapeCanMoveDown())
                {
                    //if (HasLines()) skipLockDelay = true;

                    if (!skipLockDelay && !lockDelay.Active)
                        lockDelay.Start();

                    if (skipLockDelay || lockDelay.Expired())
                    {
                        gridPile.AddShapeToPile(currentShape);
                        Score += 17;
                        currentShape = null;
                        if (skipLockDelay) skipLockDelay = false;
                        if (lockDelay.Expired()) lockDelay.Reset();
                    }
                }
            }
        }

        /// <summary>
        /// This method will send a new shape moving down the grid.
        /// </summary>
        private void CheckToGenerateNewCurrentShape()
        {
            if (currentShape == null)
            {
                currentShape = dropShapes[0];
                dropShapes.Remove(currentShape);
            }
        }

        /// <summary>
        /// This method will check to see if a shape should be loaded to be sent out next.
        /// </summary>
        private void CheckToInsertNextShape()
        {
            if (dropShapes.Count == 0)
            {
                int randomShapeNumber = random.Next(7);
                //Shape nextShape = shapeFactory.GenerateShape(randomShapeNumber, shapeGenerationPoint, BlockType);
                Shape nextShape = new Shape(shapeGenerationPoint, BlockType, (ShapeName)randomShapeNumber);
                dropShapes.Add(nextShape);
            }
        }

        /// <summary>
        /// This method will hold a shape for later. If there's already a shape being held, it will swap out that shape
        /// for the one currently moving on the screen.
        /// </summary>
        public void HoldShape()
        {
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
                else
                {
                    heldShape = currentShape;
                    currentShape = null;
                }
            }
        }
        #endregion 
    }
}
