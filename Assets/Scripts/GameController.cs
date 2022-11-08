using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] UiController _uiController;
        [SerializeField] GameObject _gridUnit;
        private SnakeGameGrid _grid;
        private Snake _snake;
        private bool _isGameOver = false;

        private const int LINE = 10;
        private const int COL = 10;
        private const int WALLS = 10;

        void Start()
        {
            _grid = new SnakeGameGrid(LINE, COL, _gridUnit);

            _snake = new Snake();
            _grid.ChangeGridUnitState(_snake.SnakeHead, SnakeGameGridUnitState.Snake);

            GenerateWalls(WALLS);

            GenerateFruit();
        }

        private void Update()
        {
            if (!Input.anyKeyDown) return;

            if (_isGameOver)
            {
                RestartGame();
            }

            OnKeyDown();
        }

        private void OnKeyDown()
        {
            Vector2Int nextGrid = new Vector2Int();

            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                nextGrid = _snake.SnakeHead + Vector2Int.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                nextGrid = _snake.SnakeHead + Vector2Int.down;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                nextGrid = _snake.SnakeHead + Vector2Int.left;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                nextGrid = _snake.SnakeHead + Vector2Int.right;
            }
            else
            {
                return;
            }

            if (nextGrid.x < 0 || nextGrid.y < 0 || nextGrid.x >= LINE || nextGrid.y >= COL)
            {
                GameOver();
                return;
            }

            SnakeGameGridUnitState nextGridState = _grid.GetGridUnitState(nextGrid);

            switch (nextGridState)
            {
                case SnakeGameGridUnitState.Snake:
                    GameOver();
                    break;
                case SnakeGameGridUnitState.Wall:
                    GameOver();
                    break;
                case SnakeGameGridUnitState.Fruit:
                    _snake.AddNewBodyPart(nextGrid);
                    _grid.ChangeGridUnitState(nextGrid, SnakeGameGridUnitState.Snake);
                    GenerateFruit();
                    break;
                case SnakeGameGridUnitState.Empty:
                    _grid.ChangeGridUnitState(_snake.SnakeTail, SnakeGameGridUnitState.Empty);
                    _snake.MoveTo(nextGrid);
                    _grid.ChangeGridUnitState(_snake.SnakeHead, SnakeGameGridUnitState.Snake);
                    break;
            }
        }

        private void GenerateFruit()
        {
            PlaceElementAtRandom(SnakeGameGridUnitState.Fruit);
        }

        private void GenerateWalls(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                PlaceElementAtRandom(SnakeGameGridUnitState.Wall);
            }
        }

        private void PlaceElementAtRandom(SnakeGameGridUnitState element)
        {
            Vector2Int randPosition = new Vector2Int();
            bool invalidPosition = true;

            while (invalidPosition)
            {
                randPosition = GetRandomVector2Int();
                invalidPosition = _grid.GetGridUnitState(randPosition) != SnakeGameGridUnitState.Empty;
            }

            _grid.ChangeGridUnitState(randPosition, element);
        }

        private Vector2Int GetRandomVector2Int()
        {
            int line = Random.Range(0, LINE);
            int col = Random.Range(0, COL);

            return new Vector2Int(line, col);
        }

        private void GameOver()
        {
            _isGameOver = true;
            _uiController.ActivateGameOver();
        }

        private void RestartGame()
        {
            _isGameOver = false;
            _uiController.DeactivateGameOver();
            SceneManager.LoadScene(0);
        }
    }
}