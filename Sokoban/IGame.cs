using System;
using System.Collections.Generic;
using System.Text;

namespace SokobanGame
{
    public interface IGame
    {

        void Move(Direction moveDirection);
        int GetMoveCount();
        void Undo();
        void Restart();
        bool IsFinished();
        void Load(string newLevel);
    }
}
