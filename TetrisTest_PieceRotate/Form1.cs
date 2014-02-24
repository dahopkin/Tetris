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
        Block block;
        Shape shape;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            shape = new Shape_T(e.Location);
            using (Graphics g = this.CreateGraphics()) {
                shape.Draw(g);
            }
        }

        private void btnRotate_Click(object sender, EventArgs e)
        {
            Refresh();
            shape.RotateShape(shape);
            using (Graphics g = this.CreateGraphics())
            {
                shape.Draw(g);
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            Refresh();
            shape.Move(Direction.Down, 10);
            using (Graphics g = this.CreateGraphics())
            {
                shape.Draw(g);
            }
        }

        private void btnMoveLeft_Click(object sender, EventArgs e)
        {
            Refresh();
            shape.Move(Direction.Left, 10);
            using (Graphics g = this.CreateGraphics())
            {
                shape.Draw(g);
            }
        }

        private void btnMoveRight_Click(object sender, EventArgs e)
        {
            Refresh();
            shape.Move(Direction.Right, 10);
            using (Graphics g = this.CreateGraphics())
            {
                shape.Draw(g);
            }
        }
    }
}
