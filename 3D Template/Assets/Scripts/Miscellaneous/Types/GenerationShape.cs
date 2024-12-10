using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GenerationShape
{
    public enum ShapeType
    {
        SnakeLeftToRight,
        SnakeTopToBottom,
        SpiralOutToIn,
        SpiralInToOut,
    }

    [SerializeField] private ShapeType _shapeType;

    public readonly IEnumerable<Vector2Int> GetPositions(Vector2Int dimensions) => _shapeType switch
    {
        ShapeType.SnakeLeftToRight => GetSnakeLeftToRightPositions(dimensions),
        ShapeType.SnakeTopToBottom => GetSnakeTopToBottomPositions(dimensions),
        ShapeType.SpiralInToOut => GetSpiralInToOutPositions(dimensions),
        _ => GetSpiralOutToInPositions(dimensions)
    };

    private static IEnumerable<Vector2Int> GetSnakeLeftToRightPositions(Vector2Int dimensions)
    {
        for (int x = 0; x < dimensions.x; ++x)
        {
            for (int y = 0; y < dimensions.y; ++y)
                yield return new(x, y);

            ++x;

            for (int y = dimensions.y - 1; y >= 0; --y)
                yield return new(x, y);
        }
    }

    private static IEnumerable<Vector2Int> GetSnakeTopToBottomPositions(Vector2Int dimensions)
    {
        for (int y = 0; y < dimensions.y; ++y)
        {
            for (int x = 0; x < dimensions.x; ++x)
                yield return new(x, y);

            ++y;

            for (int x = dimensions.x - 1; x >= 0; --x)
                yield return new(x, y);
        }
    }

    private static IEnumerable<Vector2Int> GetSpiralOutToInPositions(Vector2Int dimensions)
    {
        Vector2Int position = Vector2Int.zero;
        Vector2Int direction = Vector2Int.right;
        RectInt borders = new(Vector2Int.zero, dimensions);

        for (int i = 0; i < dimensions.x * dimensions.y; ++i)
        {
            yield return position;
            position += direction;

            if (position == borders.BottomRight() + Vector2.left && direction == Vector2Int.right) // Bottom Right, Move Up
            {
                direction = Vector2Int.up;
                ++borders.y;
            }
            else if (position == borders.TopRight() - Vector2.one && direction == Vector2Int.up) // Top Right, Move Left
            {
                direction = Vector2Int.left;
                --borders.width;
            }
            else if (position == borders.TopLeft() + Vector2.down && direction == Vector2Int.left) // Top Left, Move Down
            {
                direction = Vector2Int.down;
                --borders.height;
            }
            else if (position == borders.BottomLeft() && direction == Vector2Int.down) // Bottom Left, Move Right
            {
                direction = Vector2Int.right;
                ++borders.x;
            }
        }
    }

    private static IEnumerable<Vector2Int> GetSpiralInToOutPositions(Vector2Int dimensions)
    {
        yield return new();
    }
}
