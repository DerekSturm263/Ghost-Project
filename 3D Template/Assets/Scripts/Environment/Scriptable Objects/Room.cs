using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public struct Room : IRotatable<Room>
{
    [SerializeField] private string _name;
    public readonly string Name => _name;

    [SerializeField] private Grid<bool> _layout;
    public readonly Grid<bool> Layout => _layout;
    public void SetLayoutTemp(Grid<bool> layout) => _layout = layout;

    [SerializeField] private Directional<List<Grid<bool>>> _roomsToDirections;
    public readonly Directional<List<Grid<bool>>> RoomsToDirections => _roomsToDirections;

    public Room(string name, Grid<bool> layout, Directional<List<Grid<bool>>> roomsToDirections)
    {
        _name = name;
        _layout = layout;
        _roomsToDirections = roomsToDirections;
    }

    public readonly Room Rotate90() => new($"{_name} (90)", _layout.Rotate90(), _roomsToDirections.Rotate90().Select(item => item.Select(item2 => item2.Rotate90()).ToList()).ToDirectional());
    public readonly Room Rotate180() => new($"{_name} (180)", _layout.Rotate180(), _roomsToDirections.Rotate180().Select(item => item.Select(item2 => item2.Rotate180()).ToList()).ToDirectional());
    public readonly Room Rotate270() => new($"{_name} (270)", _layout.Rotate270(), _roomsToDirections.Rotate270().Select(item => item.Select(item2 => item2.Rotate270()).ToList()).ToDirectional());
}
