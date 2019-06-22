using UnityEngine;

public static class Vector2Extensions
{
    public static Vector2 GetEnvelopeToValue(this Vector2 sizeDelta, float value)
    {
        if (sizeDelta.x < sizeDelta.y)
        {
            return FitToWidth(sizeDelta, value);
        }
        else
        {
            return FitToHeight(sizeDelta, value);
        }
    }

    public static Vector2 GetFitToValue(this Vector2 sizeDelta, float value)
    {
        if (sizeDelta.x > sizeDelta.y)
        {
            return FitToWidth(sizeDelta, value);
        }
        else
        {
            return FitToHeight(sizeDelta, value);
        }
    }

    private static Vector2 FitToHeight(Vector2 sizeDelta, float value)
    {
        var width = (sizeDelta.x / sizeDelta.y) * value;
        return new Vector2(width, value);
    }

    private static Vector2 FitToWidth(Vector2 sizeDelta, float value)
    {
        var height = (sizeDelta.y / sizeDelta.x) * value;
        return new Vector2(value, height);
    }
}
