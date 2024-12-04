using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class MapGenerator : NetworkBehaviour
{
    [SerializeField] private MapSettings _default;

    private readonly Stack<GameObject> _maps = new();

    [ContextMenu("Generate")]
    public void Generate()
    {
        if (_maps.Count > 0)
            Destroy();
        
        Generate(_default);
    }

    public void Generate(MapSettings settings)
    {
        _maps.Push(GenerationHelper.CreateMap(settings, SpawnMap, SpawnRoom, ChildRoom, out int _));
    }

    public void Destroy() => Destroy(_maps.Pop());

    public GameObject SpawnMap(MapSettings mapSettings, EntropicList<Room>[,] map, System.Random random)
    {
        return new("Map");
    }

    public GameObject SpawnRoom(MapSettings settings, Room room, System.Random random)
    {
        GameObject roomGO = Instantiate(settings.RoomPrefab);
        DecorateRoom(roomGO, room, random);

        return roomGO;
    }

    public void ChildRoom(MapSettings settings, ref GameObject map, ref GameObject room, Vector2Int position)
    {
        room.transform.SetParent(map.transform, false);
        room.transform.localPosition = new(position.x * settings.Spacing.x, 0, -position.y * settings.Spacing.y);
    }

    public void DecorateRoom(GameObject roomGO, Room room, System.Random random)
    {
        roomGO.transform.GetChild(1).gameObject.SetActive(room.Layout[1, 0]);
        roomGO.transform.GetChild(2).gameObject.SetActive(room.Layout[2, 1]);
        roomGO.transform.GetChild(3).gameObject.SetActive(room.Layout[1, 2]);
        roomGO.transform.GetChild(4).gameObject.SetActive(room.Layout[0, 1]);
    }
}
