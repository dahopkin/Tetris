using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Tetris
{
    public interface IDrawStyle
    {
        void Draw(Graphics g);
        void StartFlashing();
        bool IsFlashing();
        bool DoneFlashing();
    }
}
