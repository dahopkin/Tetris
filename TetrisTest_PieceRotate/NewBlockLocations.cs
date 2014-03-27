/*
 * The NewBlockLocations enum is used to represent all the possible locations where a 
 * new block could be formed adjacent to the "original" one. This enum lists all of its
 * elements in counter-clockwise order (which is important for functions in the extensions class).
 * 
 * (c) Copyright 2014 Daniel Hopkins. All Rights Reserved.
 * E-mail: dahopkin2@gmail.com
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tetris
{
    public enum NewBlockLocations
    {
        
        Right,
        Top_Right,
        Top,
        Top_Left,
        Left,
        Bottom_Left,
        Bottom,
        Bottom_Right,
    }
}
