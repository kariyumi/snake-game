using UnityEngine;

public class SnakeGameGridUnit
{
    public GameObject GridUnitGameObject;
    public SpriteRenderer GridUnitSpriteRenderer;
    public SnakeGameGridUnitState GridUnitState;

    public SnakeGameGridUnit(GameObject gridUnitGameObject, Vector3 position, SnakeGameGridUnitState gridUnitState)
    {
        GridUnitGameObject = GameObject.Instantiate(gridUnitGameObject, position, Quaternion.identity);
        GridUnitSpriteRenderer = GridUnitGameObject.GetComponent<SpriteRenderer>();
        SetGridUnitState(gridUnitState);
    }

    public void SetGridUnitState(SnakeGameGridUnitState gridUnitState)
    {
        GridUnitState = gridUnitState;

        switch (GridUnitState)
        {
            case SnakeGameGridUnitState.Snake:
                GridUnitSpriteRenderer.color = Color.blue;
                break;

            case SnakeGameGridUnitState.Wall:
                GridUnitSpriteRenderer.color = Color.black;
                break;

            case SnakeGameGridUnitState.Fruit:
                GridUnitSpriteRenderer.color = Color.red;
                break;

            case SnakeGameGridUnitState.Empty:
                GridUnitSpriteRenderer.color = Color.white;
                break;
        }
    }
}

public enum SnakeGameGridUnitState
{
    Snake,
    Wall,
    Fruit,
    Empty
}
