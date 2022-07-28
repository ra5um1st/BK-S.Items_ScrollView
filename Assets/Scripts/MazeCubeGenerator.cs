using Maze;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maze
{
    public class MazeCubeGenerator : MonoBehaviour
    {
        [SerializeField] private int _cubeLengthX = 5;
        [SerializeField] private int _cubeLengthY = 5;
        [SerializeField] private int _cubeLengthZ = 5;
        [SerializeField] private int _size = 1;
        [SerializeField] private GameObject _wall;

        [SerializeField] private MazeGenerator _ceilingGenerator;
        [SerializeField] private MazeGenerator _floorGenerator;
        [SerializeField] private MazeGenerator _westGenerator;
        [SerializeField] private MazeGenerator _eastGenerator;
        [SerializeField] private MazeGenerator _northGenerator;
        [SerializeField] private MazeGenerator _southGenerator;

        [SerializeField] private bool _updated;

        void Start()
        {
            transform.localPosition = new Vector3(_cubeLengthX / 2 + _size / 2, _cubeLengthY / 2, _cubeLengthZ / 2 + _size / 2);
            Initialize();
        }

        void Update()
        {
            if (_updated)
            {
                _floorGenerator.transform.localRotation = new Quaternion(180, 0, 0, 1);

                _westGenerator.transform.localRotation = new Quaternion(0, 0, 90, 1);

                _eastGenerator.transform.localRotation = new Quaternion(0, 0, -90, 1);

                _northGenerator.transform.localRotation = new Quaternion(-90, 0, 0, 1);

                _southGenerator.transform.localRotation = new Quaternion(0, 90, 90, 1);

                _updated = false;
            }
        }

        private void Initialize()
        {
            _ceilingGenerator = GetMazeGenerator("Maze Generator (0)", _cubeLengthY, _cubeLengthX, _size, _wall);
            _floorGenerator = GetMazeGenerator("Maze Generator (1)", _cubeLengthY, _cubeLengthX, _size, _wall);
            _westGenerator = GetMazeGenerator("Maze Generator (2)", _cubeLengthY - _size * 2, _cubeLengthX, _size, _wall);
            _eastGenerator = GetMazeGenerator("Maze Generator (3)", _cubeLengthY - _size * 2, _cubeLengthX, _size, _wall);
            _northGenerator = GetMazeGenerator("Maze Generator (4)", _cubeLengthY - _size * 2, _cubeLengthX - _size * 2, _size, _wall);
            _southGenerator = GetMazeGenerator("Maze Generator (5)", _cubeLengthY - _size * 2, _cubeLengthX - _size * 2, _size, _wall);
        }

        private MazeGenerator GetMazeGenerator(string name, int rows, int columns, int size, GameObject wall)
        {
            var generator = GameObject.Find(name).GetComponent<MazeGenerator>();
            generator.Rows = rows;
            generator.Columns = columns;
            generator.Size = size;
            generator.Wall = wall;
            generator.Generated = true;
            return generator;
        }
    }
}