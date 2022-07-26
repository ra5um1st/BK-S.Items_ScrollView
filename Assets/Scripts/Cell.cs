using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Maze;

namespace Maze
{
    public class Cell
    {
        public bool Visited { get; set; }
        public GameObject NorthWall { get; set; }
        public GameObject SouthWall { get; set; }
        public GameObject EastWall { get; set; }
        public GameObject WestWall { get; set; }
        public GameObject Floor { get; set; }
        public GameObject Ceilling { get; set; }
    }
}