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
        private ReactiveProperty<float> onEditH = new ReactiveProperty<float>();
        public IReactiveProperty<float> OnEditS => onEditS;
        private ReactiveProperty<float> onEditS = new ReactiveProperty<float>();
        public IReactiveProperty<float> OnEditV => onEditV;
        private ReactiveProperty<float> onEditV = new ReactiveProperty<float>();

        // 入力してApplyが呼ばれた場合はテキストを更新しない.
        private bool input = false;

        private void Start()
        {
            Initialize(inputFieldH, onEditH, ColorPickerDefine.HUE_MIN, ColorPickerDefine.HUE_MAX);
            Initialize(inputFieldS, onEditS, ColorPickerDefine.SATURATION_MIN, ColorPickerDefine.SATURATION_MAX);
            Initialize(inputFieldV, onEditV, ColorPickerDefine.VALUE_MIN, ColorPickerDefine.VALUE_MAX);
        }

        private void Initialize(TMP_InputField inputField, ReactiveProperty<float> onEdit, int min, int max)
        {
            // TODO:数値が空の時にonEndEditが呼ばれた場合０を入れるようにする
            Observable.Merge(inputField.onEndEdit.AsObservable(), inputField.onValueChanged.AsObservable()).Subscribe(value =>
            {
                // 数値か？.
                if(int.TryParse(value, out var intValue))
                {
                    input = true;
                    intValue = Mathf.Clamp(intValue, min, max);
                    // Clampした結果を文字に反映.
                    inputField.SetTextWithoutNotify(intValue.ToString());
                    // 0~1.
                    var value01 = (float)intValue / max;
                    onEdit.Value = value01;
                }
                else
                {
                    Debug.LogWarning("ParameterRGB : Invalid parameter.");
                }
            }).AddTo(this);
        }
        
        public void Apply(Vector3 rgb)
        {
            if (input)
            {
                input = false;
                return;
            }
            
            Color.RGBToHSV(new Color(rgb.x, rgb.y, rgb.z), out var h, out var s, out var v);
            var intH = (int) (h * ColorPickerDefine.HUE_MAX);
            var intS = (int) (s * ColorPickerDefine.SATURATION_MAX);
            int intV = (int) (v * ColorPickerDefine.VALUE_MAX);

            inputFieldH.SetTextWithoutNotify(intH.ToString());
            inputFieldS.SetTextWithoutNotify(intS.ToString());
            inputFieldV.SetTextWithoutNotify(intV.ToString());
        }

        public void ApplyHue(float hue)
        {
            var intH = (int) (hue * ColorPickerDefine.HUE_MAX);
            inputFieldH.SetTextWithoutNotify(intH.ToString());
        }
    }
}
