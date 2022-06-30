using System;
using TMPro;
using UniRx;
using UnityEngine;

namespace HamburgerMenu.Scripts
{
    public class TextField : ItemBase
    {
        [SerializeField]
        private TMP_InputField inputField;

        private readonly ReactiveProperty<string> value = new StringReactiveProperty("");

        public IObservable<string> Initialize(string label, string text)
        {
            value.Subscribe(value =>
            {
                inputField.text = value;
            }).AddTo(this);
            
            inputField.onEndEdit.AsObservable().Subscribe(value =>
            {
                this.value.Value = value;
            }).AddTo(this);
            
            standardParts.label.text = label;
            value.Value = text;
            return value;
        }
    }
}
