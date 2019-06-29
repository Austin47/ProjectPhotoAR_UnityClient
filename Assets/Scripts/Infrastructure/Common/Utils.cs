using UnityEngine;
using UnityEngine.UI;

namespace Infrastructure.Common
{
    public class Utils
    {
        public static void EnvelopeToValueFromTexture2D(RawImage image, Texture2D texture, float envelopeSize)
        {
            var textureSize = new Vector2(texture.width, texture.height);
            var size = textureSize.GetEnvelopeToValue(envelopeSize);
            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
            image.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);
        }

        public static void EnvelopeToValueFromTexture2D(Transform transform, Texture2D texture, float envelopeSize)
        {
            var textureSize = new Vector2(texture.width, texture.height);
            var scale = textureSize.GetEnvelopeToValue(envelopeSize);
            transform.localScale = new Vector3(scale.x, scale.y, 1);
        }

        // Source https://docs.unity3d.com/Manual/DirectionDistanceFromOneObjectToAnother.html
        public static Vector3 GetDirectionBetweenVectors(Vector3 point1, Vector3 point2)
        {
            // Gets a vector that points from the point1 position to the point2 position.
            var heading = point2 - point1;

            var distance = heading.magnitude;
            var direction = heading / distance; // This is now the normalized direction.
            return direction;
        }
    }
}

