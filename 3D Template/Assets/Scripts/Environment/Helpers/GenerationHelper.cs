using System.Collections.Generic;
using UnityEngine;

public static class GenerationHelper
{
    public delegate TMap MapSpawner<TMap>(MapSettings mapSettings, EntropicList<Room>[,] map, System.Random random);
    public delegate TRoom RoomSpawner<TRoom>(MapSettings mapSettings, Room room, System.Random random);
    public delegate void RoomMapper<TRoom, TMap>(MapSettings mapSettings, ref TMap map, ref TRoom room, Vector2Int position);

    public static TMap CreateMap<TRoom, TMap>(MapSettings settings, MapSpawner<TMap> mapSpawner, RoomSpawner<TRoom> roomSpawner, RoomMapper<TRoom, TMap> roomMapper, out int seed)
    {
        seed = settings.Seed;
        System.Random random = new(seed);

        return SpawnMap(settings, CalculateMap(settings, random), mapSpawner, roomSpawner, roomMapper, random);
    }

    private static EntropicList<Room>[,] CalculateMap(MapSettings settings, System.Random random)
    {
        // Initialize the possibilities of each room.
        EntropicList<Room>[,] possibilities = InitializePossibilities(settings);

        // Iterate through all the rooms...
        for (int y = 0; y < settings.Dimensions.y; ++y)
        {
            for (int x = 0; x < settings.Dimensions.x; ++x)
            {
                // Randomly select the room based on its possibilities.
                Room room = possibilities[x, y].Get(random);

                // Reduce the possibilities for each of the neighbors.
                if (y - 1 >= 0)
                    possibilities[x, y - 1].Filter(item => room.RoomsToDirections.GetFromDirection(Directional<List<Grid<bool>>>.Direction.North).Contains(item.Layout), settings.Rooms[^1], settings.FilterMode);
                if (x + 1 < settings.Dimensions.x)
                    possibilities[x + 1, y].Filter(item => room.RoomsToDirections.GetFromDirection(Directional<List<Grid<bool>>>.Direction.East).Contains(item.Layout), settings.Rooms[^1], settings.FilterMode);
                if (y + 1 < settings.Dimensions.y)
                    possibilities[x, y + 1].Filter(item => room.RoomsToDirections.GetFromDirection(Directional<List<Grid<bool>>>.Direction.South).Contains(item.Layout), settings.Rooms[^1], settings.FilterMode);
                if (x - 1 >= 0)
                    possibilities[x - 1, y].Filter(item => room.RoomsToDirections.GetFromDirection(Directional<List<Grid<bool>>>.Direction.West).Contains(item.Layout), settings.Rooms[^1], settings.FilterMode);
            }
        }

        // Return the new list of possibilities, which should only contain a single possibility per slot.
        return possibilities;
    }

    private static EntropicList<Room>[,] InitializePossibilities(MapSettings settings)
    {
        // Create a new multidimensional array of lists to store the rooms.
        EntropicList<Room>[,] possibilities = new EntropicList<Room>[settings.Dimensions.x, settings.Dimensions.y];

        // Iterate through all the rooms...
        for (int y = 0; y < settings.Dimensions.y; ++y)
        {
            for (int x = 0; x < settings.Dimensions.x; ++x)
            {
                // Assign a new room list to each slot in the array.
                possibilities[x, y] = new(settings.Rooms);
            }
        }

        // Return the list of all possibilities, which should contain every possible possibility per slot.
        return possibilities;
    }

    private static TMap SpawnMap<TRoom, TMap>(MapSettings settings, EntropicList<Room>[,] map, MapSpawner<TMap> mapSpawner, RoomSpawner<TRoom> roomSpawner, RoomMapper<TRoom, TMap> roomMapper, System.Random random)
    {
        TMap mapInstance = mapSpawner.Invoke(settings, map, random);

        for (int y = 0; y < settings.Dimensions.y; ++y)
        {
            for (int x = 0; x < settings.Dimensions.x; ++x)
            {
                TRoom roomInstance = roomSpawner.Invoke(settings, map[x, y].Get(random), random);
                roomMapper.Invoke(settings, ref mapInstance, ref roomInstance, new(x, y));
            }
        }

        return mapInstance;
    }
}
