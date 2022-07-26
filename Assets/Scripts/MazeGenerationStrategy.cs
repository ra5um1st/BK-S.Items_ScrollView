using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class MazeGenerationStrategy : IMazeGenerationStrategy
    {
		private bool _courseComplete = false;
		private int _currentRow = 0;
		private int _currentColumn = 0;

		public void GenerateMaze(Cell[,] cells)
		{
			cells[_currentRow, _currentColumn].Visited = true;

			while (!_courseComplete)
			{
                Kill(cells);
				Hunt(cells);
			}
		}

		private void Kill(Cell[,] cells)
		{
			while (RouteStillAvailable(cells, _currentRow, _currentColumn))
			{
				int direction = Random.Range(1,5);

				if (direction == 1 && IsCellAvailable(cells, _currentRow - 1, _currentColumn))
				{
					// North
					DestroyWall(cells[_currentRow, _currentColumn].NorthWall);
					DestroyWall(cells[_currentRow - 1, _currentColumn].SouthWall);
					_currentRow--;
				}
				else if (direction == 2 && IsCellAvailable(cells, _currentRow + 1, _currentColumn))
				{
					// South
					DestroyWall(cells[_currentRow, _currentColumn].SouthWall);
					DestroyWall(cells[_currentRow + 1, _currentColumn].NorthWall);
					_currentRow++;
				}
				else if (direction == 3 && IsCellAvailable(cells, _currentRow, _currentColumn + 1))
				{
					// east
					DestroyWall(cells[_currentRow, _currentColumn].EastWall);
					DestroyWall(cells[_currentRow, _currentColumn + 1].WestWall);
					_currentColumn++;
				}
				else if (direction == 4 && IsCellAvailable(cells, _currentRow, _currentColumn - 1))
				{
					// west
					DestroyWall(cells[_currentRow, _currentColumn].WestWall);
					DestroyWall(cells[_currentRow, _currentColumn - 1].EastWall);
					_currentColumn--;
				}

				cells[_currentRow, _currentColumn].Visited = true;
			}
		}

		private void Hunt(Cell[,] cells)
		{
			_courseComplete = true;

			for (int i = 0; i < cells.GetLength(0); i++)
			{
				for (int j = 0; j < cells.GetLength(1); j++)
				{
					if (!cells[i, j].Visited && AdjacentCellVisited(cells, i, j))
					{
						_courseComplete = false;
						_currentRow = i;
						_currentColumn = j;
						DestroyAdjacentWall(cells, _currentRow, _currentColumn);
						cells[_currentRow, _currentColumn].Visited = true;
						return;
					}
				}
			}
		}


		private bool RouteStillAvailable(Cell[,] cells, int _currentRow, int _currentColumn)
		{
			int availableRoutes = 0;

			if (_currentRow > 0 && !cells[_currentRow - 1, _currentColumn].Visited)
			{
				availableRoutes++;
			}

			if (_currentRow < cells.GetLength(0) - 1 && !cells[_currentRow + 1, _currentColumn].Visited)
			{
				availableRoutes++;
			}

			if (_currentColumn > 0 && !cells[_currentRow, _currentColumn - 1].Visited)
			{
				availableRoutes++;
			}

			if (_currentColumn < cells.GetLength(1) - 1 && !cells[_currentRow, _currentColumn + 1].Visited)
			{
				availableRoutes++;
			}

			return availableRoutes > 0;
		}

		private bool IsCellAvailable(Cell[,] cells, int row, int column)
		{
			if (row >= 0 && row < cells.GetLength(0) && column >= 0 && column < cells.GetLength(1) && !cells[row, column].Visited)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private void DestroyWall(GameObject wall)
		{
			if (wall != null)
			{
				GameObject.Destroy(wall);
			}
		}

		private bool AdjacentCellVisited(Cell[,] cells, int row, int column)
		{
			int VisitedCells = 0;

			if (row > 0 && cells[row - 1, column].Visited)
			{
				VisitedCells++;
			}
			if (row < (cells.GetLength(0) - 2) && cells[row + 1, column].Visited)
			{
				VisitedCells++;
			}
			if (column > 0 && cells[row, column - 1].Visited)
			{
				VisitedCells++;
			}
			if (column < (cells.GetLength(1) - 2) && cells[row, column + 1].Visited)
			{
				VisitedCells++;
			}

			return VisitedCells > 0;
		}

		private void DestroyAdjacentWall(Cell[,] cells, int row, int column)
		{
			bool wallDestroyed = false;

			while (!wallDestroyed)
			{
				int direction = Random.Range(1, 5);

				if (direction == 1 && row > 0 && cells[row - 1, column].Visited)
				{
					DestroyWall(cells[row, column].NorthWall);
					DestroyWall(cells[row - 1, column].SouthWall);
					wallDestroyed = true;
				}
				else if (direction == 2 && row < (cells.GetLength(0) - 2) && cells[row + 1, column].Visited)
				{
					DestroyWall(cells[row, column].SouthWall);
					DestroyWall(cells[row + 1, column].NorthWall);
					wallDestroyed = true;
				}
				else if (direction == 3 && column > 0 && cells[row, column - 1].Visited)
				{
					DestroyWall(cells[row, column].WestWall);
					DestroyWall(cells[row, column - 1].EastWall);
					wallDestroyed = true;
				}
				else if (direction == 4 && column < (cells.GetLength(1) - 2) && cells[row, column + 1].Visited)
				{
					DestroyWall(cells[row, column].EastWall);
					DestroyWall(cells[row, column + 1].WestWall);
					wallDestroyed = true;
				}
			}

		}

    }
}