using Maze;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMazeButtonHandler : MonoBehaviour
{
    public void OnButtonClick(MazeGenerator generator)
    {
        generator.GenerateMaze();
    }
}
