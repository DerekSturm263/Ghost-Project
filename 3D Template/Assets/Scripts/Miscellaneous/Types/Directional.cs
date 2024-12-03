using UnityEngine;

[System.Serializable]
public struct Directional<T>
{
    private const float DIRECTION_SUCCESS = 0.5f;

    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    [SerializeField] private T _north;
    [SerializeField] private T _east;
    [SerializeField] private T _south;
    [SerializeField] private T _west;

    public readonly T GetFromDirection(Direction direction) => direction switch
    {
        Direction.North => _north,
        Direction.East => _east,
        Direction.South => _south,
        Direction.West => _west,
        _ => default
    };

    public readonly T GetFromDirection(Vector2 direction)
    {
        if (Vector2.Dot(direction, Vector2.up) > DIRECTION_SUCCESS)
        {
            return GetFromDirection(Direction.North);
        }
        else if (Vector2.Dot(direction, Vector2.down) > DIRECTION_SUCCESS)
        {
            return GetFromDirection(Direction.South);
        }
        else if (Vector2.Dot(direction, Vector2.left) > DIRECTION_SUCCESS)
        {
            return GetFromDirection(Direction.East);
        }
        else if (Vector2.Dot(direction, Vector2.right) > DIRECTION_SUCCESS)
        {
            return GetFromDirection(Direction.West);
        }

        return default;
    }

    public Directional(T north, T east, T south, T west)
    {
        _north = north;
        _east = east;
        _south = south;
        _west = west;
    }
}
