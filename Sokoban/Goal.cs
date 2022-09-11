using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SokobanGame
{
    sealed public class Goal : Square
    {
        public Goal(int newRow, int newColumn)
        {
            _row = newRow;
            _column = newColumn;
        }

        public override void PrintSquare()
        {
            Debug.WriteLine("This square is a Goal");
            base.PrintSquare();
        }

    }
}
