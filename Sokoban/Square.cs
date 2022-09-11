using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SokobanGame
{
    abstract public class Square
    {
        protected int _row;
        protected int _column;

        public int GetRow()
        {
            return _row;
        }

        public int GetColumn()
        {
            return _column;
        }

        public void SetRow(int newRow)
        {
            _row = newRow;
        }

        public void SetColumn(int newColumn)
        {
            _column = newColumn;
        }

        public virtual void PrintSquare()
        {
            Debug.WriteLine($"A square with row {_row}, and column {_column}");
        } 

    }
}
