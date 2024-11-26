using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RoomGenerator : NetworkBehaviour
{
    [SerializeField] private List<Room> _rooms;

    [SerializeField] private Vector2Int _dimensions;
    [SerializeField] private Vector2 _spacing;

    [ContextMenu("Generate")]
    public void Generate(int seed)
    {
        System.Random random = new(seed);

        Dictionary<Vector2Int, List<Room>> possibilities = GeneratePossibilities();

        for (int y = 0; y < _dimensions.y; ++y)
        {
            for (int x = 0; x < _dimensions.x; ++x)
            {

            }
        }
    }

    public Dictionary<Vector2Int, List<Room>> GeneratePossibilities()
    {
        Dictionary<Vector2Int, List<Room>> possibilities = new();

        for (int y = 0; y < _dimensions.y; ++y)
        {
            for (int x = 0; x < _dimensions.x; ++x)
            {
                List<Room> rooms = new(_rooms);
                possibilities.Add(new Vector2Int(x, y), rooms);
            }
        }

        return possibilities;
    }

    public Room SelectRoom(Dictionary<Vector2Int, List<Room>> possibilities, System.Random random)
    {
        return null;
    }

    public GameObject CreateRoom(Room room, Vector2Int position)
    {
        return Instantiate(room.Prefab, position * _spacing, Quaternion.identity);
    }
}
