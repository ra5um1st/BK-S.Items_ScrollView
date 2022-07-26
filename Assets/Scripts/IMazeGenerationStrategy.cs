using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maze;

namespace Maze
{
    public interface IMazeGenerationStrategy
    {
        public void GenerateMaze(Cell[,] cells);
    }
}