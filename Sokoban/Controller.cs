using SokobanGame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sokoban_Game_WPF
{
    public class Controller
    {
        private Game _game;
        private bool _loaded;

        public void Start()
        {
            _game = new Game();
            _game.CreateLevel(8, 9);
        }

        public void CreateLevel()
        {
            CreateGame(_game);
        }

        public void MovePlayer(Direction dir)
        {
            _game.Move(dir);
        }

        public Game GetGame() => _game;

        public int GetMoveCount() => _game.GetMoveCount();

        public void setLoaded(bool val) => _loaded = val;

        public bool getLoaded() => _loaded;
        
        public void Restart()
        {
            List<Square> squares = _game.ReturnSquares();
            squares.Clear();
            CreateGame(_game);
        }

        public bool CheckWin()
        {
            return _game.IsFinished();
        }

        public void SaveGame()
        {
            _game.SaveMe();
        }

        public void LoadGame()
        {
            _game.LoadMe();
        }

        public void SaveLastMove() => _game.SaveLastMove();

        public void LoadLastMove() => _game.LoadLastMove();

        public void CreateGame(Game game)
        {
            // Row 1
            game.AddWall(1, 1);
            game.AddWall(2, 1);
            game.AddWall(3, 1);
            game.AddWall(4, 1);
            game.AddWall(5, 1);
            game.AddWall(6, 1);
            game.AddWall(7, 1);
            game.AddWall(8, 1);

            // Row 2
            game.AddWall(1, 2);
            game.AddWall(2, 2);
            game.AddWall(3, 2);
            game.AddEmpty(4, 2);
            game.AddEmpty(5, 2);
            game.AddEmpty(6, 2);
            game.AddWall(7, 2);
            game.AddWall(8, 2);

            // Row 3
            game.AddWall(1, 3);
            game.AddGoal(2, 3);
            game.AddPlayer(3, 3);
            game.AddBlock(4, 3);
            game.AddEmpty(5, 3);
            game.AddEmpty(6, 3);
            game.AddWall(7, 3);
            game.AddWall(8, 3);

            // Row 4
            game.AddWall(1, 4);
            game.AddWall(2, 4);
            game.AddWall(3, 4);
            game.AddEmpty(4, 4);
            game.AddBlock(5, 4);
            game.AddGoal(6, 4);
            game.AddWall(7, 4);
            game.AddWall(8, 4);

            // Row 5
            game.AddWall(1, 5);
            game.AddGoal(2, 5);
            game.AddWall(3, 5);
            game.AddWall(4, 5);
            game.AddBlock(5, 5);
            game.AddEmpty(6, 5);
            game.AddWall(7, 5);
            game.AddWall(8, 5);

            // Row 6
            game.AddWall(1, 6);
            game.AddEmpty(2, 6);
            game.AddWall(3, 6);
            game.AddEmpty(4, 6);
            game.AddGoal(5, 6);
            game.AddEmpty(6, 6);
            game.AddWall(7, 6);
            game.AddWall(8, 6);

            // Row 7
            game.AddWall(1, 7);
            game.AddBlock(2, 7);
            game.AddEmpty(3, 7);
            game.AddEmpty(4, 7);
            game.AddBlock(5, 7);
            game.AddBlock(6, 7);
            game.AddGoal(7, 7);
            game.AddWall(8, 7);

            // Row 8
            game.AddWall(1, 8);
            game.AddEmpty(2, 8);
            game.AddEmpty(3, 8);
            game.AddEmpty(4, 8);
            game.AddGoal(5, 8);
            game.AddEmpty(6, 8);
            game.AddEmpty(7, 8);
            game.AddWall(8, 8);


            // Row 9
            game.AddWall(1, 9);
            game.AddWall(2, 9);
            game.AddWall(3, 9);
            game.AddWall(4, 9);
            game.AddWall(5, 9);
            game.AddWall(6, 9);
            game.AddWall(7, 9);
            game.AddWall(8, 9);
        }
    }
}
