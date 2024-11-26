using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Room", menuName = "Custom/Room")]
public class Room : ScriptableObject
{
    [SerializeField] private GameObject _prefab;
    public GameObject Prefab => _prefab;

    [SerializeField] private List<Room> _north;
    public Room RandomNorth => _north[Random.Range(0, _north.Count)];

    [SerializeField] private List<Room> _south;
    public Room RandomSouth => _south[Random.Range(0, _south.Count)];

    [SerializeField] private List<Room> _east;
    public Room RandomEast => _east[Random.Range(0, _east.Count)];

    [SerializeField] private List<Room> _west;
    public Room RandomWest => _west[Random.Range(0, _west.Count)];
}
