using System;
using System.Collections.Generic;
using System.Text;

namespace SokobanGame
{
    interface ILevel
    {

        void CreateLevel(int width, int height);
        int GetLevelWidth();
        int GetLevelHeight();
        void AddBlock(int gridX, int gridY);
        void AddPlayer(int gridX, int gridY);
        void AddWall(int gridX, int gridY);
        void AddGoal(int gridX, int gridY);
        void AddEmpty(int gridX, int gridY);
        void AddBlockOnGoal(int gridX, int gridY);
        void AddPlayerOnGoal(int gridX, int gridY);
        Part GetPartAtIndex(int gridX, int gridY);
        void SaveMe();
        bool CheckValid();
    }
}
