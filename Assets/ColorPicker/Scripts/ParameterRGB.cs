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

        // public IObservable<float> OnEditR => onEditR;
        // private Subject<float> onEditR = new Subject<float>();
        public IReadOnlyReactiveProperty<float> OnEditR => onEditR;
        private ReactiveProperty<float> onEditR = new ReactiveProperty<float>();
        
        public ReactiveProperty<float> OnEditG => onEditG;
        private ReactiveProperty<float> onEditG = new ReactiveProperty<float>();
        
        public ReactiveProperty<float> OnEditB => onEditB;
        private ReactiveProperty<float> onEditB = new ReactiveProperty<float>();

        private void Start()
        {
            Initialize(inputFieldR, onEditR);
            Initialize(inputFieldG, OnEditG);
            Initialize(inputFieldB, OnEditB);
        }

        private void Initialize(TMP_InputField inputField, ReactiveProperty<float> onEdit)
        {
            Observable.Merge(inputField.onEndEdit.AsObservable(), inputField.onValueChanged.AsObservable()).Subscribe(value =>
            {
                if (ColorPickerUtility.StringTo01(value, ColorPickerDefine.RGB_MIN, ColorPickerDefine.RGB_MAX, out var result))
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
            var r = (int)(rgb.x * ColorPickerDefine.RGB_MAX);
            var g = (int)(rgb.y * ColorPickerDefine.RGB_MAX);
            var b = (int)(rgb.z * ColorPickerDefine.RGB_MAX);

            inputFieldR.text = r.ToString();
            inputFieldG.text = g.ToString();
            inputFieldB.text = b.ToString();
        }
    }
}
