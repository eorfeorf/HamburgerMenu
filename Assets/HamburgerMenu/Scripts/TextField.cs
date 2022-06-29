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

        private ReactiveProperty<string> value = new ReactiveProperty<string>();

        private void Start()
        {
            inputField.onEndEdit.AsObservable().Subscribe(value =>
            {
                this.value.Value = value;
            }).AddTo(this);
        }

        public IObservable<string> Initialize(string label, string text)
        {
            standardParts.label.text = label;
            inputField.text = text;
            return value;
        }
    }
}
