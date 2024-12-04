using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GenerationHelper
{
    public delegate TMap MapSpawner<TMap>(MapSettings mapSettings, EntropicList<Room>[,] map, System.Random random);
    public delegate TRoom RoomSpawner<TRoom>(MapSettings mapSettings, Room room, System.Random random);
    public delegate void RoomMapper<TRoom, TMap>(MapSettings mapSettings, ref TMap map, ref TRoom room, Vector2Int position);

    public static TMap CreateMap<TRoom, TMap>(MapSettings settings, MapSpawner<TMap> mapSpawner, RoomSpawner<TRoom> roomSpawner, RoomMapper<TRoom, TMap> roomMapper, int generations, out int seed)
    {
        seed = settings.Seed;
        System.Random random = new(seed);

        return SpawnMap(settings, CalculateMap(settings, random, generations), mapSpawner, roomSpawner, roomMapper, random);
    }

    private static EntropicList<Room>[,] CalculateMap(MapSettings settings, System.Random random, int generations)
    {
        // Initialize the possibilities of each room.
        EntropicList<Room>[,] possibilities = InitializePossibilities(settings);

        // Iterate through all the rooms...
        for (int y = 0; y < settings.Dimensions.y; ++y)
        {
            for (int x = 0; x < settings.Dimensions.x; ++x)
            {
                // If the number of generations has passed, end it early.
                if (x + y * settings.Dimensions.x >= generations)
                    return possibilities;

                // Assign the given space.
                AssignSpace(settings, possibilities, new(x, y), random);
            }
        }

        // Return the new list of possibilities, which should only contain a single possibility per slot.
        return possibilities;
    }

    public static void AssignSpace(MapSettings settings, EntropicList<Room>[,] possibilities, Vector2Int position, System.Random random)
    {
        // Randomly select the room based on its possibilities.
        Room room = possibilities[position.x, position.y].Get(random);

        // Reduce the possibilities for each of the neighbors.
        if (position.y - 1 >= 0)
            possibilities[position.x, position.y - 1].Filter(item => room.RoomsToDirections.GetFromDirection(Directional<List<Grid<bool>>>.Direction.North).Contains(item.Layout), settings.Rooms[^1], settings.FilterMode);
        if (position.y - 1 >= 0 && position.x + 1 < settings.Dimensions.x)
            possibilities[position.x + 1, position.y - 1].Filter(item => room.RoomsToDirections.GetFromDirection(Directional<List<Grid<bool>>>.Direction.NorthEast).Contains(item.Layout), settings.Rooms[^1], settings.FilterMode);
        if (position.x + 1 < settings.Dimensions.x)
            possibilities[position.x + 1, position.y].Filter(item => room.RoomsToDirections.GetFromDirection(Directional<List<Grid<bool>>>.Direction.East).Contains(item.Layout), settings.Rooms[^1], settings.FilterMode);
        if (position.x + 1 < settings.Dimensions.x && position.y + 1 < settings.Dimensions.y)
            possibilities[position.x + 1, position.y + 1].Filter(item => room.RoomsToDirections.GetFromDirection(Directional<List<Grid<bool>>>.Direction.SouthEast).Contains(item.Layout), settings.Rooms[^1], settings.FilterMode);
        if (position.y + 1 < settings.Dimensions.y)
            possibilities[position.x, position.y + 1].Filter(item => room.RoomsToDirections.GetFromDirection(Directional<List<Grid<bool>>>.Direction.South).Contains(item.Layout), settings.Rooms[^1], settings.FilterMode);
        if (position.y + 1 < settings.Dimensions.y && position.x - 1 >= 0)
            possibilities[position.x - 1, position.y + 1].Filter(item => room.RoomsToDirections.GetFromDirection(Directional<List<Grid<bool>>>.Direction.SouthWest).Contains(item.Layout), settings.Rooms[^1], settings.FilterMode);
        if (position.x - 1 >= 0)
            possibilities[position.x - 1, position.y].Filter(item => room.RoomsToDirections.GetFromDirection(Directional<List<Grid<bool>>>.Direction.West).Contains(item.Layout), settings.Rooms[^1], settings.FilterMode);
        if (position.x - 1 >= 0 && position.y - 1 >= 0)
            possibilities[position.x - 1, position.y - 1].Filter(item => room.RoomsToDirections.GetFromDirection(Directional<List<Grid<bool>>>.Direction.NorthWest).Contains(item.Layout), settings.Rooms[^1], settings.FilterMode);
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
                if (x == 0 && y == 0)
                    possibilities[x, y] = new(new() { settings.Rooms[3] });
                else if (x == settings.Dimensions.x - 1 && y == 0)
                    possibilities[x, y] = new(new() { settings.Rooms[0] });
                else if (x == settings.Dimensions.x - 1 && y == settings.Dimensions.y - 1)
                    possibilities[x, y] = new(new() { settings.Rooms[1] });
                else if (x == 0 && y == settings.Dimensions.y - 1)
                    possibilities[x, y] = new(new() { settings.Rooms[2] });
                else if (x == 0)
                    possibilities[x, y] = new(new() { settings.Rooms[7] });
                else if (x == settings.Dimensions.x - 1)
                    possibilities[x, y] = new(new() { settings.Rooms[5] });
                else if (y == 0)
                    possibilities[x, y] = new(new() { settings.Rooms[4] });
                else if (y == settings.Dimensions.y - 1)
                    possibilities[x, y] = new(new() { settings.Rooms[6] });
                else
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
