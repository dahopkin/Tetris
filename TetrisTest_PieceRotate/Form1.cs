using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        Game game;
        Random random = new Random();
        bool gameOver;
        List<Keys> keysPressed = new List<Keys>();
        BlockType blockType;
        int switchTypeIndex = 0;
        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
        }

        private void InitializeGrid(){
            game = new Game(new Point(20, 20), random, BlockType.Solid);
            game.GameOver += new EventHandler(grid_GameOver);
            tmrGame.Enabled = true;
            tmrAnimation.Enabled = true;
            gameOver = false;
        }

        void SwitchType() {
            game.BlockType = (BlockType)switchTypeIndex;
            switchTypeIndex = (switchTypeIndex + 1) % 3;
            foreach (Block block in game.gridPile.PileBlocks)
                block.DecideBlockType(game.BlockType);
        }

        void grid_GameOver(object sender, EventArgs e)
        {
            tmrGame.Stop();
            gameOver = true;
            Invalidate();
            //Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            game.Draw(g);

            if (gameOver)
            {
                DrawGameOver(g);
            } // end if 
        }

        private void DrawGameOver(Graphics g)
        {
            string playAgain = "Press S to start a new game or Q to quit";

            Font playAgainFont = new Font("Arial", 9);
            Font gameOverTopFont = new Font("Arial", 24, FontStyle.Bold);
            Point gameOverPoint = new Point((game.Boundaries.Width / 10), (game.Boundaries.Height / 2));
            Point playAgainPoint = new Point(gameOverPoint.X, gameOverPoint.Y + 40);
            SolidBrush fadeBlack = new SolidBrush(Color.FromArgb(127, Color.Black));
            g.FillRectangle(fadeBlack, game.Boundaries);
            g.DrawString("GAME OVER!", gameOverTopFont, Brushes.White, gameOverPoint);
            g.DrawString(playAgain, playAgainFont, Brushes.White, playAgainPoint);
        }

        private void tmrAnimation_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            game.Go();

            foreach (Keys key in keysPressed)
            {
                if (key == Keys.Left)
                {
                    game.MoveCurrentShape(Direction.Left);
                    return;
                } // end if

                else if (key == Keys.Right)
                {
                    game.MoveCurrentShape(Direction.Right);
                    return;
                } // end else if
                else if (key == Keys.Down)
                {
                    game.MoveCurrentShape(Direction.Down);
                    return;
                } // end else if
            } // end foreach

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up)
                game.RotateCurrentShape();

            //if (e.KeyCode == Keys.B)
                //SwitchType();

            if (e.KeyCode == Keys.Q)
                Application.Exit();

            if (e.KeyCode == Keys.Space)
                game.HardDrop();
            
            if (e.KeyCode == Keys.H)
                game.HoldShape();
            
            if (gameOver)
            {
                if (e.KeyCode == Keys.S)
                {
                    InitializeGrid();
                    return;
                } // end if 
            } // end if 

            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);

            keysPressed.Add(e.KeyCode);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (keysPressed.Contains(e.KeyCode))
                keysPressed.Remove(e.KeyCode);
        }

       
    }
}
