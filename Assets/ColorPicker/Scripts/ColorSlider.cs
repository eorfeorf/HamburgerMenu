using UniRx;
using UnityEngine;

namespace ColorPicker.Scripts
{
    public class ColorSlider : MonoBehaviour
    {
        [SerializeField]
        private ColorSliderRect rect;

        [SerializeField]
        private RectTransform pointer;
        private void Start()
        {
            Observable.Merge(rect.OnPointerClick, rect.OnPointerDrag).Subscribe(data =>
            {
                var localPoint = ColorPickerUtility.GetLocalPoint(data.position, rect.RectTransform);
                localPoint.y = Mathf.Clamp(localPoint.y, rect.RectTransform.rect.yMin, rect.RectTransform.rect.yMax);
                localPoint.x = pointer.localPosition.x;

                pointer.localPosition = localPoint;

                // 真ん中が0なので半分を底上げ
                Vector2 uv = ColorPickerUtility.GetLocalPoint01(localPoint, rect.RectTransform);

                Debug.Log($"ColorSlider: ClickPosition={localPoint}");
                Debug.Log($"ColorSlider: UV={uv}");
            }).AddTo(this);
        }
    }
}
