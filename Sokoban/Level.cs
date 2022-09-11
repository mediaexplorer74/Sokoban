using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SokobanGame
{
    class Level
    {
        private int _width { get; set; }
        private int _height { get; set; }

        Player thePlayer;
        Player savedPlayer;

        List<Square> listOfSquares = new List<Square>();
        List<Square> SavedlistOfSquares = new List<Square>();
        List<List<Square>> SavedlistOfMoves = new List<List<Square>>();
        List<Player> ListOfPlayerMoves = new List<Player>();

        public Level(int newWidth, int newHeight)
        {
            _width = newWidth;
            _height = newHeight;
        }

        public int GetWidth() => _width;

        public int GetHeight() => _height;

        public Player GetPlayer() => thePlayer;

        public List<Square> GetSquares() => listOfSquares;

        public void SetSavedList()
        {
            SavedlistOfSquares.Clear();
            foreach(Square square in listOfSquares)
            {
                switch(square.GetType().Name)
                {
                    case "Wall":
                        SavedlistOfSquares.Add(new Wall(square.GetRow(), square.GetColumn()));
                        break;
                    case "Block":
                        SavedlistOfSquares.Add(new Block(square.GetRow(), square.GetColumn()));
                        break;
                    case "Goal":
                        SavedlistOfSquares.Add(new Goal(square.GetRow(), square.GetColumn()));
                        break;
                    case "Empty":
                        SavedlistOfSquares.Add(new EmptySquare(square.GetRow(), square.GetColumn()));
                        break;
                }

            }
            savedPlayer = new Player(thePlayer.GetRow(), thePlayer.GetColumn(), thePlayer.GetMoveCount());
        }

        public void LoadSave()
        {
            listOfSquares.Clear();
            foreach (Square square in SavedlistOfSquares)
            {
                switch (square.GetType().Name)
                {
                    case "Wall":
                        listOfSquares.Add(new Wall(square.GetRow(), square.GetColumn()));
                        break;
                    case "Block":
                        listOfSquares.Add(new Block(square.GetRow(), square.GetColumn()));
                        break;
                    case "Goal":
                        listOfSquares.Add(new Goal(square.GetRow(), square.GetColumn()));
                        break;
                    case "Empty":
                        listOfSquares.Add(new EmptySquare(square.GetRow(), square.GetColumn()));
                        break;
                }

            }
            thePlayer = new Player(savedPlayer.GetRow(), savedPlayer.GetColumn(), savedPlayer.GetMoveCount());
        }

        public void SaveLastMove()
        {
            List<Square> TempList = new List<Square>();
            foreach (Square square in listOfSquares)
            {
                switch (square.GetType().Name)
                {
                    case "Wall":
                        TempList.Add(new Wall(square.GetRow(), square.GetColumn()));
                        break;
                    case "Block":
                        TempList.Add(new Block(square.GetRow(), square.GetColumn()));
                        break;
                    case "Goal":
                        TempList.Add(new Goal(square.GetRow(), square.GetColumn()));
                        break;
                    case "Empty":
                        TempList.Add(new EmptySquare(square.GetRow(), square.GetColumn()));
                        break;
                }

            }
            Player tempPlayer = new Player(thePlayer.GetRow(), thePlayer.GetColumn(), thePlayer.GetMoveCount());

            SavedlistOfMoves.Add(TempList);
            ListOfPlayerMoves.Add(tempPlayer);
        }

        public void LoadLastMove()
        {
            if(SavedlistOfMoves.Count > 0)
            {
                List<Square> lastMove = SavedlistOfMoves[SavedlistOfMoves.Count - 1];
                listOfSquares.Clear();
                foreach (Square square in lastMove)
                {
                    switch (square.GetType().Name)
                    {
                        case "Wall":
                            listOfSquares.Add(new Wall(square.GetRow(), square.GetColumn()));
                            break;
                        case "Block":
                            listOfSquares.Add(new Block(square.GetRow(), square.GetColumn()));
                            break;
                        case "Goal":
                            listOfSquares.Add(new Goal(square.GetRow(), square.GetColumn()));
                            break;
                        case "Empty":
                            listOfSquares.Add(new EmptySquare(square.GetRow(), square.GetColumn()));
                            break;
                    }

                }
                
                
                Player lastPlayerMove = ListOfPlayerMoves[ListOfPlayerMoves.Count - 1];
                thePlayer = new Player(lastPlayerMove.GetRow(), lastPlayerMove.GetColumn(), lastPlayerMove.GetMoveCount());

                SavedlistOfMoves.RemoveAt(SavedlistOfMoves.Count - 1);
                ListOfPlayerMoves.RemoveAt(ListOfPlayerMoves.Count - 1);
            }
        }


        public void AddPlayer(int column, int row)
        {
            if(InBounds(column, row))
            {
                Player newPlayer = new Player(row, column);
                thePlayer = newPlayer;
            }
        }

        public void AddGoal(int column, int row)
        {
            if(InBounds(column, row))
            {
                Goal newGoal = new Goal(row, column);
                listOfSquares.Add(newGoal);
            }
        }

        public void AddBlock(int column, int row)
        {
            if (InBounds(column, row))
            {
                Block newBlock = new Block(row, column);
                listOfSquares.Add(newBlock);
            }
        }

        public void AddEmpty(int column, int row)
        {
            if (InBounds(column, row))
            {
                EmptySquare newEmpty = new EmptySquare(row, column);
                listOfSquares.Add(newEmpty);
            }
        }

        public void AddWall(int column, int row)
        {
            if (InBounds(column, row))
            {
                Wall newWall = new Wall(row, column);
                listOfSquares.Add(newWall);
            }
        }

        public bool InBounds(int targetColumn, int targetRow)
        {
            bool isValid = false;
            int levelWidth = GetWidth();
            int levelHeight = GetHeight();

            if(targetColumn > 0 & targetColumn <= levelWidth & targetRow > 0 & targetRow <= levelHeight)
            {
                isValid = true;
            }

            return isValid;
        }

        public int GetTargetRow(Direction moveDirection)
        {
            int testRow = thePlayer.GetRow();

            if (moveDirection == Direction.Up)
            {
                testRow -= 1;
            }
            else if (moveDirection == Direction.Down)
            {
                testRow += 1;
            }

            return testRow;

        }

        public int GetTargetColumn(Direction moveDirection)
        {
            int testCol = thePlayer.GetColumn();

            if (moveDirection == Direction.Left)
            {
                testCol -= 1;
            }
            else if (moveDirection == Direction.Right)
            {
                testCol += 1;
            }

            return testCol;
        }

        public Square GetTargetSquare(int targetRow, int targetColumn, Direction moveDirection)
        {
            int newRow = targetRow;
            int newCol = targetColumn;

            if (moveDirection == Direction.Up)
            {
                newRow -= 1;
            }
            else if (moveDirection == Direction.Down)
            {
                newRow += 1;
            }
            else if (moveDirection == Direction.Left)
            {
                newCol -= 1;
            }
            else if (moveDirection == Direction.Right)
            {
                newCol += 1;
            }

            foreach (Square square in listOfSquares)
            {
                if (square.GetRow() == newRow & square.GetColumn() == newCol)
                {
                    return square;
                }
            }
            return new EmptySquare(-1, -1);
        }

        public Square GetBlock(int targetRow, int targetColumn)
        {
            foreach (Square square in listOfSquares)
            {
                if (square.GetType().Name == "Block" & square.GetRow() == targetRow & square.GetColumn() == targetColumn)
                {
                    return square;
                }
            }

            return new EmptySquare(0, 0);
        }

        public Square GetGoalAt(int targetColumn, int targetRow)
        {
            foreach (Square square in listOfSquares)
            {
                if (square.GetType().Name == "Goal" & square.GetRow() == targetRow & square.GetColumn() == targetColumn)
                {
                    return square;
                }
            }

            return new EmptySquare(0, 0);
        }

        public int GetPlayerMoves()
        {
            return thePlayer.GetMoveCount();
        }

        public void MovePlayer(Direction moveDirection)
        {
            int moveRow = GetTargetRow(moveDirection);
            int moveCol = GetTargetColumn(moveDirection);

            thePlayer.SetRow(moveRow);
            thePlayer.SetColumn(moveCol);
            thePlayer.IncreaseMoveCount();
        }

        public bool IsWall(Direction moveDirection)
        {
            int testRow = GetTargetRow(moveDirection);
            int testCol = GetTargetColumn(moveDirection);
            bool wall = false;


            foreach(Square square in listOfSquares)
            {
                if(square is Wall & square.GetRow() == testRow & square.GetColumn() == testCol)
                {
                    wall = true; ;
                }
            }
            return wall;
        }

        public bool IsBlock(Direction moveDirection)
        {
            int testRow = GetTargetRow(moveDirection);
            int testCol = GetTargetColumn(moveDirection);

            foreach (Square square in listOfSquares)
            {
                if (square.GetType().Name == "Block" & square.GetRow() == testRow & square.GetColumn() == testCol)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckFinished()
        {
            List<Square> listOfBlocks = new List<Square>();
            List<Square> listOfGoals = new List<Square>();

            foreach(Square square in listOfSquares)
            {
                if(square.GetType().Name == "Block")
                {
                    listOfBlocks.Add(square);
                }
                else if(square.GetType().Name == "Goal")
                {
                    listOfGoals.Add(square);
                }
            }


            if (listOfBlocks.Count != listOfGoals.Count)
            {
                return false;
            }
            
            for(int block = listOfBlocks.Count - 1; block >= 0; block--)
            {
                for (int goal = listOfGoals.Count - 1; goal >= 0; goal--)
                {
                    if (listOfBlocks[block].GetRow() == listOfGoals[goal].GetRow() & listOfBlocks[block].GetColumn() == listOfGoals[goal].GetColumn())
                    {
                        listOfBlocks.RemoveAt(block);
                        listOfGoals.RemoveAt(goal);
                        break;
                    }
                }
            }

            if(listOfBlocks.Count == 0 & listOfGoals.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<Square> ReturnSquares()
        {
            return listOfSquares;
        }
    }
}
