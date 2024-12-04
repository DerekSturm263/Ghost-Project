using UnityEngine;

[System.Serializable]
public struct Directional<T>
{
    public enum Direction
    {
        North,
        NorthEast,
        East,
        SouthEast,
        South,
        SouthWest,
        West,
        NorthWest
    }

    [SerializeField] private T _north;
    [SerializeField] private T _northEast;
    [SerializeField] private T _east;
    [SerializeField] private T _southEast;
    [SerializeField] private T _south;
    [SerializeField] private T _southWest;
    [SerializeField] private T _west;
    [SerializeField] private T _northWest;

    public readonly T GetFromDirection(Direction direction) => direction switch
    {
        Direction.North => _north,
        Direction.NorthEast => _northEast,
        Direction.East => _east,
        Direction.SouthEast => _southEast,
        Direction.South => _south,
        Direction.SouthWest => _southWest,
        Direction.West => _west,
        Direction.NorthWest => _northWest,
        _ => default
    };

    public Directional(T north, T northEast, T east, T southEast, T south, T southWest, T west, T northWest)
    {
        _north = north;
        _northEast = northEast;
        _east = east;
        _southEast = southEast;
        _south = south;
        _southWest = southWest;
        _west = west;
        _northWest = northWest;
    }
}
