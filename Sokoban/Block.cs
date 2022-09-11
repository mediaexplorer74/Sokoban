using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SokobanGame
{
    sealed class Block : Square
    {
        public Block(int newRow, int newColumn)
        {
            _row = newRow;
            _column = newColumn;
        }

        public override void PrintSquare()
        {
            Debug.WriteLine("This square is a block");
            base.PrintSquare();
        }
    }
}
