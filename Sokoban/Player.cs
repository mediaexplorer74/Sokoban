using System;
using System.Collections.Generic;
using System.Text;

namespace SokobanGame
{
    class Player
    {
        private int _row;
        private int _column;
        private int _moveCount = 0;

        public Player(int newRow, int newColumn)
        {
            _row = newRow;
            _column = newColumn;
        }

        public Player(int newRow, int newColumn, int moveCount)
        {
            _row = newRow;
            _column = newColumn;
            _moveCount = moveCount;
        }

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

        public void IncreaseMoveCount()
        {
            _moveCount += 1;
        }

        public int GetMoveCount()
        {
            return _moveCount;
        }

        public void ResetMoves()
        {
            _moveCount = 0;
        }

    }
}
