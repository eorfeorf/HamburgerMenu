using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace ColorPicker.Scripts
{
    public class ParameterRGB : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField inputFieldR;
        [SerializeField]
        private TMP_InputField inputFieldG;
        [SerializeField]
        private TMP_InputField inputFieldB;

        public IReadOnlyReactiveProperty<float> OnEditR => onEditR;
        private ReactiveProperty<float> onEditR = new ReactiveProperty<float>();
        public ReactiveProperty<float> OnEditG => onEditG;
        private ReactiveProperty<float> onEditG = new ReactiveProperty<float>();
        public ReactiveProperty<float> OnEditB => onEditB;
        private ReactiveProperty<float> onEditB = new ReactiveProperty<float>();
        
        // 入力してApplyが呼ばれた場合はテキストを更新しない.
        private bool input = false;

        private void Start()
        {
            Initialize(inputFieldR, onEditR, ColorPickerDefine.RGB_MIN, ColorPickerDefine.RGB_MAX);
            Initialize(inputFieldG, OnEditG, ColorPickerDefine.RGB_MIN, ColorPickerDefine.RGB_MAX);
            Initialize(inputFieldB, OnEditB, ColorPickerDefine.RGB_MIN, ColorPickerDefine.RGB_MAX);
        }

        private void Initialize(TMP_InputField inputField, ReactiveProperty<float> onEdit, int min, int max)
        {
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
            
            var r = (int)(rgb.x * ColorPickerDefine.RGB_MAX);
            var g = (int)(rgb.y * ColorPickerDefine.RGB_MAX);
            var b = (int)(rgb.z * ColorPickerDefine.RGB_MAX);

            inputFieldR.SetTextWithoutNotify(r.ToString());
            inputFieldG.SetTextWithoutNotify(g.ToString());
            inputFieldB.SetTextWithoutNotify(b.ToString());
        }
    }
}
