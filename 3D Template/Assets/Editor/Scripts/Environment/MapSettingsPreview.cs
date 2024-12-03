using UnityEditor;
using UnityEngine;

[CustomPreview(typeof(MapSettings))]
public class MapSettingsPreview : ObjectPreview
{
    private int _lastSeed;
    private Texture2D _preview;

    public override bool HasPreviewGUI() => true;
    public override string GetInfoString() => $"Seed: {_lastSeed}";

    public override void OnPreviewGUI(Rect r, GUIStyle background)
    {
        MapSettings mapSettings = target as MapSettings;

        if (_preview)
        {
            GUI.DrawTexture(r, _preview);
        }

        Rect testPosition = new(r.x, r.height - EditorGUIUtility.singleLineHeight, r.width, EditorGUIUtility.singleLineHeight);
        if (GUI.Button(testPosition, "Test"))
        {
            _preview = GenerationHelper.CreateMap(mapSettings, CreateMapTexture, CreateRoomTexture, ChildRoom);
        }
    }

    public Texture2D CreateMapTexture(MapSettings mapSettings, EntropicList<Room>[,] map, System.Random random)
    {
        Texture2D mapTexture = new(mapSettings.Dimensions.x * 3, mapSettings.Dimensions.y * 3);

        return mapTexture;
    }

    public Texture2D CreateRoomTexture(MapSettings settings, Room room, System.Random random)
    {
        Texture2D roomTexture = new(3, 3);

        for (int y = 0; y < roomTexture.height; ++y)
        {
            for (int x = 0; x < roomTexture.width; ++x)
            {
                roomTexture.SetPixel(x, y, room.Layout[x, y] ? Color.white : Color.black);
            }
        }

        return roomTexture;
    }

    public void ChildRoom(MapSettings settings, Texture2D map, Texture2D room, Vector2Int position)
    {
        for (int y1 = 0; y1 < map.height; ++y1)
        {
            for (int x1 = 0; x1 < map.width; ++x1)
            {
                for (int y2 = 0; y2 < room.height; ++y2)
                {
                    for (int x2 = 0; x2 < room.width; ++x2)
                    {
                        map.SetPixel(x1 * map.width, y1 * map.height, room.GetPixel(x2, x1));
                    }
                }
            }
        }
    }
}
