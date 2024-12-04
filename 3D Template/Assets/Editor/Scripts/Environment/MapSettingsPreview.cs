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
            GUI.DrawTexture(r, _preview, ScaleMode.ScaleToFit, true, 1);
        }

        Rect testPosition = new(r.x, r.height - EditorGUIUtility.singleLineHeight, r.width, EditorGUIUtility.singleLineHeight);
        if (!_preview || GUI.Button(testPosition, "Test"))
        {
            _preview = GenerationHelper.CreateMap(mapSettings, CreateMapTexture, CreateRoomTexture, ChildRoom, out _lastSeed);
        }
    }

    public Texture2D CreateMapTexture(MapSettings mapSettings, EntropicList<Room>[,] map, System.Random random)
    {
        return new(mapSettings.Dimensions.x * 3, mapSettings.Dimensions.y * 3);
    }

    public Texture2D CreateRoomTexture(MapSettings settings, Room room, System.Random random)
    {
        Texture2D roomTexture = new(3, 3);
        Color32[] colors = new Color32[9];

        for (int y = 0; y < roomTexture.height; ++y)
        {
            for (int x = 0; x < roomTexture.width; ++x)
            {
                colors[y * roomTexture.width + x] = room.Layout[x, y] ? Color.white : Color.black;
            }
        }
        
        roomTexture.SetPixels32(colors);
        roomTexture.Apply();

        return roomTexture;
    }

    public void ChildRoom(MapSettings settings, ref Texture2D map, ref Texture2D room, Vector2Int position)
    {
        map.SetPixels32(position.x * room.width, position.y * room.height, room.width, room.height, room.GetPixels32());
        map.Apply();
    }
}
