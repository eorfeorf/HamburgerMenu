using UnityEngine;

namespace ColorPicker.Scripts
{
    public static class ColorPickerUtility
    {
        public static Vector2 GetLocalPoint(Vector2 pos, RectTransform rectTransform, Camera camera = null)
        {
            camera = camera == null ? Camera.current : camera;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, pos, camera, out var localPoint);
            return localPoint;
        }

        public static Vector2 GetLocalPoint01(Vector2 localPoint, RectTransform rectTransform)
        {
            // 真ん中が0なので半分を底上げ
            return new Vector2(
                (localPoint.x + Mathf.Abs(rectTransform.rect.xMax)) / rectTransform.rect.width,
                (localPoint.y + Mathf.Abs(rectTransform.rect.yMax)) / rectTransform.rect.height
            );
        }
    }
}