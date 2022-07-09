using System;
using ColorPicker.Scripts.Common;
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

        public IReadOnlyReactiveProperty<Vector2> SV01 => sv;
        private ReactiveProperty<Vector2> sv = new ReactiveProperty<Vector2>();

        private static readonly int RGB = Shader.PropertyToID("_RGB");
        
        private void Start()
        {
            Observable.Merge(rect.OnPointerClick, rect.OnPointerDrag).Subscribe(data =>
            {
                var localPoint = ColorPickerUtility.GetLocalPoint(data.position, rect.RectTransform);
                localPoint.x = Mathf.Clamp(localPoint.x, rect.RectTransform.rect.xMin, rect.RectTransform.rect.xMax);
                localPoint.y = Mathf.Clamp(localPoint.y, rect.RectTransform.rect.yMin, rect.RectTransform.rect.yMax);
                
                // ポインタ位置更新.
                pointer.localPosition = localPoint;
                Debug.Log($"ColorPanel: ClickPosition={localPoint}");
                 
                // 色を取得. 
                var s = localPoint.x.Remap(rect.RectTransform.rect.xMin, rect.RectTransform.rect.xMax, 0f, 1f);
                var v = localPoint.y.Remap(rect.RectTransform.rect.yMin, rect.RectTransform.rect.yMax, 0f, 1f);
                sv.Value = new Vector2(s, v);

                // 真ん中が0なので半分を底上げ
                //Vector2 uv = ColorPickerUtility.GetLocalPoint01(localPoint, rect.RectTransform);
                //Debug.Log($"ColorPanel: UV={uv}");
                
                
            }).AddTo(this);
        }

        // TODO:HSVのほうが楽.
        public void Apply(Vector3 rgb)
        {
            ApplyRectMaterial(rgb);
            // TODO:HSVのほうが良いかも.            
            ApplyPointerPosition(rgb);
        }

        private void ApplyRectMaterial(Vector3 rgb)
        {
            var color = rgb.ToColor();
            rect.Material.SetColor(RGB, color);
        }

        private void ApplyPointerPosition(Vector3 rgb)
        {
            
        }
    }
}