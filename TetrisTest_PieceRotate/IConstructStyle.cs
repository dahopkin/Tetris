/*
 * The IConstructStyle Interface makes a class define a method for
 * creating a block's visual appearance.
 * 
 * This interface allows blocks to be created in different ways 
 * (either through different sized image arrays or GDI).
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
    public interface IConstructStyle
    {
        void ConstructBlock();
    }
}
