using System;
using UniRx;
using UnityEngine;

namespace HamburgerMenu.Scripts
{
    public sealed class SliderFloat : SliderBase<float>
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
        public override IReactiveProperty<float> Initialize(string label, float defaultValue, float min, float max, float unit)
        {
            value.Subscribe(x =>
            {
                inputField.text = x.ToString("F2");
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

        protected override ReactiveProperty<float> EditEnd(ReactiveProperty<float> value, string x, float min, float max)
        {
            var tmp = Convert.ToSingle(x);
            value.Value = Mathf.Clamp(tmp, min, max);
            return value;
        }
    }
}
