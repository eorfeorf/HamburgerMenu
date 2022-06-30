using System;
using UniRx;
using UnityEngine;

namespace HamburgerMenu.Scripts
{
    public sealed class SliderInt : SliderBase<int>
    {
        /// <summary>
        /// 初期化.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="defaultValue"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        public override IReactiveProperty<int> Initialize(string label, int defaultValue, int min, int max, int unit)
        {
            value.Subscribe(x =>
            {
                inputField.text = x.ToString();
            }).AddTo(this);

            decrement.onClick.AsObservable().Subscribe(_ =>
            {
                Decrement(this.value, min, max, unit);
            }).AddTo(this);
            increment.onClick.AsObservable().Subscribe(_ =>
            {
                Increment(this.value, min, max, unit);
            }).AddTo(this);
            inputField.onEndEdit.AsObservable().Subscribe(x =>
            {
                EditEnd(this.value, x, min, max);
            }).AddTo(this);
            
            standardParts.label.text = label;
            this.min = min;
            this.max = max;
            value.Value = Mathf.Clamp(defaultValue, min, max);
            this.unit = unit;
            return value;
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