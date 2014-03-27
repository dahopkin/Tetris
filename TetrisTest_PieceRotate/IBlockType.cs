/*
 * The IBlockType Interface combines the IDrawStyle and 
 * IConstructStyle interfaces. The draw/construct interfaces are kept separate 
 * so that certain IDrawStyles can be reused. The IBlockType
 * interface is used by classes that both create and draw blocks.
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
    public interface IBlockType : IDrawStyle, IConstructStyle {}
}
