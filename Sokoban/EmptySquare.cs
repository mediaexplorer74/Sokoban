using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SokobanGame
{
    sealed class EmptySquare : Square
    {

        public EmptySquare(int newRow, int newColumn)
        {
            _row = newRow;
            _column = newColumn;
        }

        new public void PrintSquare()
        {
            Debug.WriteLine("This square is an empty square, in row {0}, and column {1}", _row, _column);
            Debug.WriteLine("This square should not be used, it is unneccessary");
        }
    }
}
