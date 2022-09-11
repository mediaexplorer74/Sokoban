using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SokobanGame
{
    public class Game : IGame, ILevel
    {
        List<Level> listOfLevels = new List<Level>();

        Level newestLevel;

        // Adds a block to the level.
        public void AddBlock(int gridX, int gridY)
        {
                newestLevel.AddBlock(gridX, gridY);
        }

        // Adds an empty square to the level.
        public void AddEmpty(int gridX, int gridY)
        {
                newestLevel.AddEmpty(gridX, gridY);
        }

        // Adds a goal to the level.
        public void AddGoal(int gridX, int gridY)
        {
                newestLevel.AddGoal(gridX, gridY);
        }

        // Adds a player to the level.
        public void AddPlayer(int gridX, int gridY)
        {
                newestLevel.AddPlayer(gridX, gridY);
        }

        // Adds a wall to the level.
        public void AddWall(int gridX, int gridY)
        {
                newestLevel.AddWall(gridX, gridY);
        }

        // This method creates a level and adds it to list of levels.
        public void CreateLevel(int maxWidth, int maxHeight)
        {
            Level newLevel = new Level(maxWidth, maxHeight);
            listOfLevels.Add(newLevel);
            newestLevel = listOfLevels[listOfLevels.Count - 1];
        }

        // Returns the height of the level.
        public int GetLevelHeight()
        {
            return newestLevel.GetHeight();
        }

        // Returns the width of the level.
        public int GetLevelWidth()
        {
            return newestLevel.GetWidth();
        }

        // Returns the row the player is in.
        public int GetPlayerRow()
        {
            Player thePlayer = newestLevel.GetPlayer();
            if(thePlayer != null)
            {
                return thePlayer.GetRow();
            }
            return 0;        
        }

        // Returns the column the player is in.
        public int GetPlayerColumn()
        {
            Player thePlayer = newestLevel.GetPlayer();
            if (thePlayer != null)
            {
                return thePlayer.GetColumn();
            }
            return 0;
        }

        // Returns the square in the specific position.
        public Square GetTargetSquare(int targetColumn, int targetRow)
        {
            Square squareType = newestLevel.GetTargetSquare(targetRow - 1, targetColumn, Direction.Down);
            return squareType;
        }

        // Looks for a goal at target position, if there is, it returns the square.
        public Square GetGoalAt(int targetColumn, int targetRow)
        {
            return newestLevel.GetGoalAt(targetColumn, targetRow);
        }

        // Returns move count. 
        public int GetMoveCount()
        {
            return newestLevel.GetPlayerMoves();
        }

        // Returns level count. 
        public int GetLevelCount()
        {
            return listOfLevels.Count;
        }

        // Moves Player in target direction.
        public void Move(Direction moveDirection)
        {
            if (newestLevel.IsWall(moveDirection) == false)
            {
                if (newestLevel.IsBlock(moveDirection) == true)
                {
                    int blockRow = newestLevel.GetTargetRow(moveDirection);
                    int blockCol = newestLevel.GetTargetColumn(moveDirection);
                    Square block = newestLevel.GetBlock(blockRow, blockCol);
                    
                    if(newestLevel.GetTargetSquare(blockRow, blockCol, moveDirection).GetType().Name == "Block" ^ newestLevel.GetTargetSquare(blockRow, blockCol, moveDirection).GetType().Name == "Wall")
                    {
                       
                    }
                    else
                    {
                        newestLevel.MovePlayer(moveDirection);
                        
                        switch(moveDirection)
                        {
                            case Direction.Up:
                                block.SetRow(blockRow - 1);
                                break;
                            case Direction.Down:
                                block.SetRow(blockRow + 1);
                                break;
                            case Direction.Left:
                                block.SetColumn(blockCol - 1);
                                break;
                            case Direction.Right:
                                block.SetColumn(blockCol + 1);
                                break;
                        }
                    }
                    
                }
                else
                {
                    newestLevel.MovePlayer(moveDirection);
                }
                
            }
            
        }

        public List<Square> ReturnSquares()
        {
            return newestLevel.ReturnSquares();
        }

        // Checks if the game is finished.
        public bool IsFinished()
        {
            return newestLevel.CheckFinished();
        }

        public void Restart()
        {
            List<Square> squares = newestLevel.GetSquares();
            squares.Clear();
        }

        public void SaveMe()
        {
            newestLevel.SetSavedList();
        }

        public void LoadMe()
        {
            newestLevel.LoadSave();
        }

        public void SaveLastMove() => newestLevel.SaveLastMove();

        public void LoadLastMove() => newestLevel.LoadLastMove();

        public void Undo()
        {
            throw new NotImplementedException();
        }

        public bool CheckValid()
        {
            throw new NotImplementedException();
        }

        public void AddPlayerOnGoal(int gridX, int gridY)
        {
            throw new NotImplementedException();
        }

        public void AddBlockOnGoal(int gridX, int gridY)
        {
            throw new NotImplementedException();
        }

        public Part GetPartAtIndex(int gridX, int gridY)
        {
            throw new NotImplementedException();
        }

        public void Load(string newLevel)
        {
            throw new NotImplementedException();
        }
    }
}
