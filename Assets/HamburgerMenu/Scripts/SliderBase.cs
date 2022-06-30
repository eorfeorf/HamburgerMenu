using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace HamburgerMenu.Scripts
{
    public abstract class SliderBase<T> : ItemBase
    {
        [SerializeField] protected TMP_InputField inputField;
        [SerializeField] protected Button decrement;
        [SerializeField] protected Button increment;

        protected readonly ReactiveProperty<T> value = new ReactiveProperty<T>();
        protected T min;
        protected T max;
        protected T unit;

        public abstract IReactiveProperty<T> Initialize(string label, T defaultValue, T min, T max, T unit);
        protected abstract ReactiveProperty<T> Decrement(ReactiveProperty<T> value, T min, T max, T unit);
        protected abstract ReactiveProperty<T> Increment(ReactiveProperty<T> value, T min, T max, T unit);
        protected abstract ReactiveProperty<T> EditEnd(ReactiveProperty<T> value, string x, T min, T max);
    }
}
