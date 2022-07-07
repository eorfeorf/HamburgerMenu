using System;
using System.Globalization;
using TMPro;
using UniRx;
using UnityEngine;

namespace ColorPicker.Scripts
{
    /// <summary>
    /// HSV数値表示クラス.
    /// H : 0 ~ 360 °
    /// S : 0 ~ 100 %
    /// V : 0 ~ 100 %
    /// </summary>
    public class ParameterHSV : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField inputFieldH;
        [SerializeField]
        private TMP_InputField inputFieldS;
        [SerializeField]
        private TMP_InputField inputFieldV;

        public IReactiveProperty<float> OnEditH => onEditH;
        private ReactiveProperty<float> onEditH => new ReactiveProperty<float>();
        public IReactiveProperty<float> OnEditS => onEditS;
        private ReactiveProperty<float> onEditS => new ReactiveProperty<float>();
        public IReactiveProperty<float> OnEditV => onEditV;
        private ReactiveProperty<float> onEditV => new ReactiveProperty<float>();

        private void Start()
        {
            Initialize(inputFieldH, onEditH, ColorPickerDefine.HUE_MIN, ColorPickerDefine.HUE_MAX);
            Initialize(inputFieldS, onEditS, ColorPickerDefine.SATURATION_MIN, ColorPickerDefine.SATURATION_MAX);
            Initialize(inputFieldV, onEditV, ColorPickerDefine.VALUE_MIN, ColorPickerDefine.VALUE_MAX);
        }

        private void Initialize(TMP_InputField inputField, ReactiveProperty<float> onEdit, int min, int max)
        {
            Observable.Merge(inputField.onEndEdit.AsObservable(), inputField.onValueChanged.AsObservable()).Subscribe(value =>
            {
                if (ColorPickerUtility.StringTo01(value, min, max, out var result))
                {
                    onEdit.Value = result;
                }
                else
                {
                    Debug.LogWarning("ParameterRGB : Invalid parameter.");
                }
            }).AddTo(this);
        }
        
        public void Apply(Vector3 rgb)
        {
            Color.RGBToHSV(new Color(rgb.x, rgb.y, rgb.z), out var h, out var s, out var v);
            var intH = (int) (h * ColorPickerDefine.HUE_MAX);
            var intS = (int) (s * ColorPickerDefine.SATURATION_MAX);
            int intV = (int) (v * ColorPickerDefine.VALUE_MAX);

            inputFieldH.text = intH.ToString();
            inputFieldS.text = intS.ToString();
            inputFieldV.text = intV.ToString();
        }
    }
}
