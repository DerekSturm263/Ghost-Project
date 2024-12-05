using System.Collections.Generic;
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
        EntropicList<Room>[,] possibilities = InitializePossibilities(settings, random);

        int currentGeneration = 0;

        // Iterate through all the rooms...
        for (int y = 1; y < settings.Dimensions.y - 1; ++y)
        {
            for (int x = 1; x < settings.Dimensions.x - 1; ++x)
            {
                // If the number of generations has passed, end it early.
                if (currentGeneration >= generations)
                    return possibilities;

                // Assign the given space.
                ReduceSpace(settings, possibilities, new(x, y), random);

                ++currentGeneration;
            }
        }

        // Return the new list of possibilities, which should only contain a single possibility per slot.
        return possibilities;
    }

    private static EntropicList<Room>[,] InitializePossibilities(MapSettings settings, System.Random random)
    {
        // Create a new multidimensional array of lists to store the rooms.
        EntropicList<Room>[,] possibilities = new EntropicList<Room>[settings.Dimensions.x, settings.Dimensions.y];

        // Create a new list of rooms that auto rotates the existing ones.
        List<Room> rooms = CreateRotatableList(settings);

        // Iterate through all the rooms...
        for (int y = 0; y < settings.Dimensions.y; ++y)
        {
            for (int x = 0; x < settings.Dimensions.x; ++x)
            {
                // Assign a new room list to each slot in the array.
                possibilities[x, y] = new(rooms);
            }
        }

        // Assign all the corners
        possibilities[0, 0] = new(new() { rooms[1] });                                                  // NW Corner
        ReduceSpace(settings, possibilities, new(0, 0), random);

        possibilities[settings.Dimensions.x - 1, 0] = new(new() { rooms[3] });                          // NE Corner
        ReduceSpace(settings, possibilities, new(settings.Dimensions.x - 1, 0), random);

        possibilities[settings.Dimensions.x - 1, settings.Dimensions.y - 1] = new(new() { rooms[4] });  // SE Corner
        ReduceSpace(settings, possibilities, new(settings.Dimensions.x - 1, settings.Dimensions.y - 1), random);

        possibilities[0, settings.Dimensions.y - 1] = new(new() { rooms[5] });                          // SW Corner
        ReduceSpace(settings, possibilities, new(0, settings.Dimensions.y - 1), random);

        // Iterate through all N Walls...
        for (int i = 1; i < settings.Dimensions.x - 1; ++i)
        {
            possibilities[i, 0] = new(new() { rooms[2], rooms[3] });
            ReduceSpace(settings, possibilities, new(i, 0), random);
        }

        // Iterate through all E Walls...
        for (int i = 1; i < settings.Dimensions.y - 1; ++i)
        {
            possibilities[settings.Dimensions.x - 1, i] = new(new() { rooms[6], rooms[4] });
            ReduceSpace(settings, possibilities, new(settings.Dimensions.x - 1, i), random);
        }

        // Iterate through all S Walls...
        for (int i = 1; i < settings.Dimensions.x - 1; ++i)
        {
            possibilities[i, settings.Dimensions.y - 1] = new(new() { rooms[7], rooms[5] });
            ReduceSpace(settings, possibilities, new(i, settings.Dimensions.y - 1), random);
        }

        // Iterate through all W Walls...
        for (int i = 1; i < settings.Dimensions.y - 1; ++i)
        {
            possibilities[0, i] = new(new() { rooms[8], rooms[1] });
            ReduceSpace(settings, possibilities, new(0, i), random);
        }

        // Return the list of all possibilities, which should contain every possible possibility per slot.
        return possibilities;
    }

    private static List<Room> CreateRotatableList(MapSettings settings)
    {
        // Create a new list of rooms.
        List<Room> rooms = new(settings.Rooms)
        {
            settings.Rooms[1].Rotate90(), // [3] NE Corner
            settings.Rooms[1].Rotate180(), // [4] SE Corner
            settings.Rooms[1].Rotate270(), // [5] SW Corner
            settings.Rooms[2].Rotate90(), // [6] E Wall
            settings.Rooms[2].Rotate180(), // [7] S Wall
            settings.Rooms[2].Rotate270() // [8] W Wall
        };

        // Return the new list.
        return rooms;
    }

    public static void ReduceSpace(MapSettings settings, EntropicList<Room>[,] possibilities, Vector2Int position, System.Random random)
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
