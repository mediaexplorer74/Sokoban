using System;
using System.Collections.Generic;
using System.Text;

namespace SokobanGame
{
    class Wall : Square
    {
        public Wall(int newRow, int newColumn)
        {
            _row = newRow;
            _column = newColumn;
        }
    }
}
