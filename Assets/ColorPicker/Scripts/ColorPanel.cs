using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ColorPicker.Scripts
{
    public class ColorPanel : MonoBehaviour
    {
        [SerializeField]
        private ColorPanelRect rect;

        [SerializeField]
        private RectTransform pointer;

        private void Start()
        {
            Observable.Merge(rect.OnPointerClick, rect.OnPointerDrag).Subscribe(data =>
            {
                var localPoint = ColorPickerUtility.GetLocalPoint(data.position, rect.RectTransform);
                localPoint.x = Mathf.Clamp(localPoint.x, rect.RectTransform.rect.xMin, rect.RectTransform.rect.xMax);
                localPoint.y = Mathf.Clamp(localPoint.y, rect.RectTransform.rect.yMin, rect.RectTransform.rect.yMax);
                
                pointer.localPosition = localPoint;

                // 真ん中が0なので半分を底上げ
                Vector2 uv = ColorPickerUtility.GetLocalPoint01(localPoint, rect.RectTransform);

                Debug.Log($"ColorPanel: ClickPosition={localPoint}");
                Debug.Log($"ColorPanel: UV={uv}");
            }).AddTo(this);
        }
    }
}