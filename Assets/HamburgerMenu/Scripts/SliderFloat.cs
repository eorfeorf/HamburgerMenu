using System;
using UniRx;
using UnityEngine;

namespace HamburgerMenu.Scripts
{
    public class SliderFloat : SliderBase<float>
    {
        private void Start()
        {
            value.Subscribe(x =>
            {
                inputField.text = string.Format("{0:F2}",value);
            }).AddTo(this);
            
            decrement.onClick.AsObservable().Subscribe(_ =>
            {
                Decrement(value, min, max, unit);
            }).AddTo(this);
           
            increment.onClick.AsObservable().Subscribe(_ =>
            {
                Increment(value, min, max, unit);
            }).AddTo(this);

            inputField.onEndEdit.AsObservable().Subscribe(x =>
            {
                EditEnd(value, x);
            }).AddTo(this);
        }
        
        public override IReactiveProperty<float> Initialize(string label, float value, float min, float max, float unit)
        {
            standardParts.label.text = label;
            this.min = min;
            this.max = max;
            this.value.Value = Mathf.Clamp(value, min, max);
            this.unit = unit;

            return this.value;
        }

        protected override ReactiveProperty<float> Decrement(ReactiveProperty<float> value, float min, float max, float unit)
        {
            var tmp = value.Value - unit;
            value.Value = Mathf.Clamp(tmp, min, max);
            return value;
        }

        protected override ReactiveProperty<float> Increment(ReactiveProperty<float> value, float min, float max, float unit)
        {
            var tmp = value.Value + unit;
            value.Value = Mathf.Clamp(tmp, min, max);
            return value;
        }

        protected override ReactiveProperty<float> EditEnd(ReactiveProperty<float> value, string x)
        {
            var tmp = Convert.ToSingle(x);
            value.Value = Mathf.Clamp(tmp, min, max);
            return value;
        }
    }
}
