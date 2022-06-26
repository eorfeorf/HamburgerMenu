using System;
using UniRx;
using UnityEngine;

namespace HamburgerMenu.Scripts
{
    public class SliderInt : SliderBase<int>
    {
        private void Start()
        {
            value.Subscribe(x =>
            {
                inputField.text = x.ToString();
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
                EditEnd(value, x, min, max);
            }).AddTo(this);
        }

        public override IReactiveProperty<int> Initialize(string label, int value, int min, int max, int unit)
        {
            standardParts.label.text = label;
            this.min = min;
            this.max = max;
            this.value.Value = Mathf.Clamp(value, min, max);
            this.unit = unit;

            return this.value;
        }

        protected override ReactiveProperty<int> Decrement(ReactiveProperty<int> value, int min, int max, int unit)
        {
            var tmp = value.Value - unit;
            value.Value = Mathf.Clamp(tmp, min, max);
            return value;
        }

        protected override ReactiveProperty<int> Increment(ReactiveProperty<int> value, int min, int max, int unit)
        {
            var tmp = value.Value + unit;
            value.Value = Mathf.Clamp(tmp, min, max);
            return value;
        }

        protected override ReactiveProperty<int> EditEnd(ReactiveProperty<int> value, string x, int min, int max)
        {
            var tmp = Convert.ToInt32(x);
            value.Value = Mathf.Clamp(tmp, min, max);
            return value;
        }
    }
}