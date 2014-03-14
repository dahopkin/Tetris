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
        Grid grid;
        Random random = new Random();
        bool gameOver;
        List<Keys> keysPressed = new List<Keys>();
        public Form1()
        {
            InitializeComponent();
            InitializeGrid();
        }

        private void InitializeGrid(){
            grid = new Grid(new Point(20, 20), random);
            grid.GameOver += new EventHandler(grid_GameOver);
            tmrGame.Enabled = true;
            tmrAnimation.Enabled = true;
            gameOver = false;
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
            /*Refresh();
            shape = new Shape_O(e.Location);
            using (Graphics g = this.CreateGraphics()) {
                shape.Draw(g);
            }*/
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            grid.Draw(g);

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
            Point gameOverPoint = new Point((grid.Boundaries.Width / 10), (grid.Boundaries.Height / 2));
            Point playAgainPoint = new Point(gameOverPoint.X, gameOverPoint.Y + 40);
            SolidBrush fadeBlack = new SolidBrush(Color.FromArgb(127, Color.Black));
            g.FillRectangle(fadeBlack, grid.Boundaries);
            g.DrawString("GAME OVER!", gameOverTopFont, Brushes.White, gameOverPoint);
            g.DrawString(playAgain, playAgainFont, Brushes.White, playAgainPoint);
        }

        private void tmrAnimation_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            grid.Go();

            foreach (Keys key in keysPressed)
            {
                if (key == Keys.Left)
                {
                    grid.MoveCurrentShape(Direction.Left);
                    return;
                } // end if

                else if (key == Keys.Right)
                {
                    grid.MoveCurrentShape(Direction.Right);
                    return;
                } // end else if
                else if (key == Keys.Down)
                {
                    grid.MoveCurrentShape(Direction.Down);
                    return;
                } // end else if
                

            } // end foreach

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Up)
                grid.RotateCurrentShape();

            if (e.KeyCode == Keys.Q)
                Application.Exit();

            if (e.KeyCode == Keys.Space)
                grid.HardDrop();
            
            if (e.KeyCode == Keys.H)
                grid.HoldShape();
            
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
