/*
 * The IDrawStyle Interface makes a class define methods for:
 * -Drawing a block onto the screen.
 * -Making a block start flashing, and checking to see if the 
 * flash is active/has expired.
 * 
 * This interface allows blocks to draw themselves onto the 
 * screen in different ways (either through preset images
 * or flowing GDI).
 * 
 * (c) Copyright 2014 Daniel Hopkins. All Rights Reserved.
 * E-mail: dahopkin2@gmail.com
 */
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
