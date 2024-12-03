using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map Settings", menuName = "Custom/Map Settings")]
public class MapSettings : ScriptableObject
{
    [SerializeField] private List<Room> _rooms;
    public List<Room> Rooms => _rooms;

    [SerializeField] GameObject _roomPrefab;
    public GameObject RoomPrefab => _roomPrefab;

    [SerializeField] private Vector2Int _dimensions;
    public Vector2Int Dimensions => _dimensions;

    [SerializeField] private Vector2 _spacing;
    public Vector2 Spacing => _spacing;

    [SerializeField] Nullable<int> _seed;
    public int Seed => _seed.GetValueOrDefault(Random.Range(0, int.MaxValue));
}
