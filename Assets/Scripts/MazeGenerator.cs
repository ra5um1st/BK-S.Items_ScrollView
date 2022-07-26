using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Maze;

namespace Maze
{
    public class MazeGenerator : MonoBehaviour
    {
        private Cell[,] _cells;
        private static int _instanseCounter;

        [SerializeField] private bool _generate;
        public bool Executed
        {
            get => _generate;
            set => _generate = value;
        }


        [SerializeField] private int _rows;
        public int Rows
        {
            get => _rows;
            set => _rows = value;
        }


        [SerializeField] private int _columns;
        public int Columns
        {
            get => _columns;
            set => _columns = value;
        }


        [SerializeField] private float _size;
        public float Size
        {
            get => _size;
            set => _size = value;
        }

        [SerializeField] private GameObject _wall;
        public GameObject Wall
        {
            get => _wall;
            set => _wall = value;
        }

        public IMazeGenerationStrategy GenerationStatery { get; set; }

        private GameObject _maze;
        public GameObject Maze => _maze;

        void Start()
        {
            transform.Translate(_columns / 2, 0, _rows / 2);
            Setup();
        }

        void Update()
        {
            if (_generate)
            {
                Setup();
                _generate = false;
            }
        }

        private void Setup()
        {
            if (_maze != null)
            {
                Destroy(_maze);
            }
            _maze = new GameObject($"Maze {_instanseCounter++}");
            _maze.transform.parent = this.transform;

            Initialize();

            GenerationStatery = new MazeGenerationStrategy();

            if (GenerationStatery != null)
            {
                GenerationStatery.GenerateMaze(_cells);
            }
            else
            {
                Debug.Log("Generation strategy is null, cannot generate a maze");
            }
        }

        private void Initialize()
        {
            _cells = new Cell[_rows, _columns];

            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _columns; j++)
                {
                    _cells[i, j] = new Cell();

                    _cells[i, j].Floor = Instantiate(_wall, new Vector3(i * _size, -(_size / 2f), j * _size), Quaternion.identity);
                    _cells[i, j].Floor.name = $"Floor[{i},{j}]";
                    _cells[i, j].Floor.transform.Rotate(Vector3.right, 90f);
                    _cells[i, j].Floor.transform.parent = _maze.transform;

                    if (j == 0)
                    {
                        _cells[i, j].WestWall = Instantiate(_wall, new Vector3(i * _size, 0, (j * _size) - (_size / 2f)), Quaternion.identity);
                        _cells[i, j].WestWall.name = $"West Wall[{i},{j}]";
                        _cells[i, j].WestWall.transform.parent = _maze.transform;
                    }

                    _cells[i, j].EastWall = Instantiate(_wall, new Vector3(i * _size, 0, (j * _size) + (_size / 2f)), Quaternion.identity);
                    _cells[i, j].EastWall.name = $"East Wall[{i},{j}]";
                    _cells[i, j].EastWall.transform.parent = _maze.transform;

                    if (i == 0)
                    {
                        _cells[i, j].NorthWall = Instantiate(_wall, new Vector3((i * _size) - (_size / 2f), 0, j * _size), Quaternion.identity);
                        _cells[i, j].NorthWall.name = $"North Wall[{i},{j}]";
                        _cells[i, j].NorthWall.transform.Rotate(Vector3.up * 90f);
                        _cells[i, j].NorthWall.transform.parent = _maze.transform;

                    }

                    _cells[i, j].SouthWall = Instantiate(_wall, new Vector3((i * _size) + (_size / 2f), 0, j * _size), Quaternion.identity);
                    _cells[i, j].SouthWall.name = $"South Wall[{i},{j}]";
                    _cells[i, j].SouthWall.transform.Rotate(Vector3.up * 90f);
                    _cells[i, j].SouthWall.transform.parent = _maze.transform;

                }
            }
        }

        public void GenerateMaze()
        {
            _generate = true;
        }
    }
}