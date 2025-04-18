using UnityEngine;

public static class ScriptUtils
{
    public static Bounds GetWorldBoundsFromRect(RectTransform rect)
    {
        Bounds bounds = new Bounds();
        Vector3[] worldCorners = new Vector3[4];

        rect.GetWorldCorners(worldCorners);
        bounds.SetMinMax(worldCorners[0], worldCorners[2]);

        return bounds;
    }
}
