using UnityEngine;

public static class UnityExtensionMethods
{
    public static Vector2Int TopLeft(this RectInt rect) => new(rect.min.x, rect.size.y);
    public static Vector2Int TopRight(this RectInt rect) => rect.size;
    public static Vector2Int BottomRight(this RectInt rect) => new(rect.size.x, rect.min.y);
    public static Vector2Int BottomLeft(this RectInt rect) => rect.min;
}
