using Maze;
using UnityEngine;
using UnityEngine.UI;

namespace Maze
{
    public class MazeGameManager : MonoBehaviour
    {
        [SerializeField] private MazeGenerator _generator;
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private GameObject _finishPrefab;
        [SerializeField] private GameObject _message;

        private bool _updated;
        private GameObject _finish;
        private GameObject _player;
        private Transform _playerPosition;

        private void Update()
        {
            if (!_updated)
            {
                CreatePlayer(_generator.Cells);
                CreateFinish(_generator.Cells);
                _updated = true;
            }
        }

        public void ResetMaze()
        {
            _generator.GenerateMaze();
            CreatePlayer(_generator.Cells);
            CreateFinish(_generator.Cells);
        }

        private void CreateFinish(Cell[,] cells)
        {
            if (_finish != null)
            {
                Destroy(_finish);
            }

            var finishFloor = cells[_generator.Rows - 1, _generator.Columns - 1].Floor;
            var finishPlate = Instantiate(_finishPrefab, this.transform);

            finishPlate.transform.position = finishFloor.transform.position;
            finishPlate.transform.rotation = finishFloor.transform.rotation;

            _finish = finishPlate;
        }

        private void CreatePlayer(Cell[,] cells)
        {
            if(_player != null)
            {
                Destroy(_player);
            }

            var playerTransform = cells[0, 0].Floor.transform;
            var playerRadius = _playerPrefab.GetComponent<SphereCollider>().radius;
            _player = Instantiate(_playerPrefab, this.transform);
            _player.transform.position = playerTransform.position;
            _player.transform.Translate(new Vector3(0, playerRadius * 2, 0));
            var playerHandle = _player.GetComponent<Player>();
            playerHandle.OnFinishTriggerEnter = OnFinishTriggerEnter;
        }

        private void OnFinishTriggerEnter()
        {
            ShowFinishMessage();
        }

        public void ShowFinishMessage()
        {
            var userText = _message.transform.Find("User Input Text");
            var finishText = _message.transform.Find("Finish Text");
            var resetButton = _message.transform.Find("Generate Maze Button");

            userText.gameObject.SetActive(false);
            finishText.gameObject.SetActive(true);
            resetButton.gameObject.SetActive(true);
            _generator.Maze.SetActive(false);
            _player.SetActive(false);
            _finish.SetActive(false);
        }
        public void HideFinishMessage()
        {
            var userText = _message.transform.Find("User Input Text");
            var finishText = _message.transform.Find("Finish Text");
            var resetButton = _message.transform.Find("Generate Maze Button");

            userText.gameObject.SetActive(true);
            finishText.gameObject.SetActive(false);
            resetButton.gameObject.SetActive(false);
            _generator.Maze.SetActive(true);
        }
    }
}
