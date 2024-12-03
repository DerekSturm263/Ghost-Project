using UnityEngine;

[System.Serializable]
public struct Grid<T>
{
    [SerializeField][HideInInspector] private T[] _elements;
    [SerializeField][HideInInspector] private int _width, _height;

    public readonly T this[System.Index x, System.Index y]
    {
        get => _elements[y.Value * _width + x.Value];
        set => _elements[y.Value * _width + x.Value] = value;
    }

    public Grid(int width, int height)
    {
        _elements = new T[width * height];
        _width = width;
        _height = height;
    }
}
