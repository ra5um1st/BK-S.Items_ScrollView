using Maze;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMazeButtonHandler : MonoBehaviour
{
    public void OnButtonClick(MazeGameManager manager)
    {
        manager.ResetMaze();
        manager.HideFinishMessage();
    }
}
