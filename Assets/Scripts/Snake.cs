using System.Collections.Generic;
using UnityEngine;

public class Snake
{
    public List<Vector2Int> SnakeBody;
    public Vector2Int SnakeHead => SnakeBody[SnakeBody.Count - 1];
    public Vector2Int SnakeTail => SnakeBody[0];

    public Snake()
    {
        SnakeBody = new List<Vector2Int>();
        SnakeBody.Add(Vector2Int.zero);
    }

    public void MoveTo(Vector2Int position)
    {
        SnakeBody.Add(position);
        SnakeBody.RemoveAt(0);
    }

    public void AddNewBodyPart(Vector2Int newBodyPart)
    {
        SnakeBody.Add(newBodyPart);
    }
}
