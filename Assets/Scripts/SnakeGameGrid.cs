using UnityEngine;

namespace Assets.Scripts
{
    public class SnakeGameGrid
    {
        private int _line;
        private int _col;
        private float _cellSize;
        private SnakeGameGridUnit[,] _gridArray;
        private Vector3 _gridOriginPosition;

        public SnakeGameGrid(int line, int col, GameObject gridUnitGameObject)
        {
            _line = line;
            _col = col;
            _cellSize = gridUnitGameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x * gridUnitGameObject.transform.localScale.x;
            _gridOriginPosition = new Vector3 ((-_cellSize * _line/2) + _cellSize/2, (-_cellSize * _col/2) + _cellSize / 2, 0);
            _gridArray = new SnakeGameGridUnit[line, col];
            CreateGrid(gridUnitGameObject);
        }

        private void CreateGrid(GameObject gridUnitGameObject)
        {
            GameObject gridContainer = new GameObject("GridContainer");

            for (int i = 0; i < _line; i++)
            {
                for (int j = 0; j < _col; j++)
                {
                    Vector3 position = new Vector3(_gridOriginPosition.x + _cellSize * i, _gridOriginPosition.y + _cellSize * j);
                    _gridArray[i, j] = new SnakeGameGridUnit(gridUnitGameObject, position, SnakeGameGridUnitState.Empty);
                    _gridArray[i, j].GridUnitGameObject.transform.parent = gridContainer.transform;
                }
            }
        }

        public SnakeGameGridUnitState GetGridUnitState(Vector2Int gridUnitPosition)
        {
            return _gridArray[gridUnitPosition.x, gridUnitPosition.y].GridUnitState;
        }

        public void ChangeGridUnitState(Vector2Int gridUnitPosition, SnakeGameGridUnitState gridUnitState)
        {
            _gridArray[gridUnitPosition.x, gridUnitPosition.y].SetGridUnitState(gridUnitState);

            switch (gridUnitState)
            {
                case SnakeGameGridUnitState.Snake:
                    _gridArray[gridUnitPosition.x, gridUnitPosition.y].GridUnitSpriteRenderer.color = Color.blue;
                    break;
                case SnakeGameGridUnitState.Fruit:
                    _gridArray[gridUnitPosition.x, gridUnitPosition.y].GridUnitSpriteRenderer.color = Color.red;
                    break;
                case SnakeGameGridUnitState.Wall:
                    _gridArray[gridUnitPosition.x, gridUnitPosition.y].GridUnitSpriteRenderer.color = Color.black;
                    break;
                case SnakeGameGridUnitState.Empty:
                    _gridArray[gridUnitPosition.x, gridUnitPosition.y].GridUnitSpriteRenderer.color = Color.white;
                    break;
            }
        }
    }
}
