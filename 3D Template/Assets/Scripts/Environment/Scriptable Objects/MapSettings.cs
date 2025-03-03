using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Map Settings", menuName = "Custom/Map Settings")]
public class MapSettings : ScriptableObject
{
    [SerializeField][HideInInspector] private List<Room> _rooms;
    public List<Room> Rooms => _rooms;

    [SerializeField] private GenerationShape _shape;
    public GenerationShape Shape => _shape;

    [SerializeField] private EntropicList<Room>.FilterMode _filterMode;
    public EntropicList<Room>.FilterMode FilterMode => _filterMode;

    [SerializeField] GameObject _roomPrefab;
    public GameObject RoomPrefab => _roomPrefab;

    [SerializeField] private Vector2Int _dimensions;
    public Vector2Int Dimensions => _dimensions;

    [SerializeField] private Vector2 _spacing;
    public Vector2 Spacing => _spacing;

    [SerializeField] Nullable<int> _seed;
    public int Seed => _seed.GetValueOrDefault(Random.Range(0, int.MaxValue));
}
