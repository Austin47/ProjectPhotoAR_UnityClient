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
    }
}

