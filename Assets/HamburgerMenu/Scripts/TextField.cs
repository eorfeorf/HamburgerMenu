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

        /// <summary>
        /// 初期化.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="defaultText"></param>
        /// <returns></returns>
        public IObservable<string> Initialize(string label, string defaultText)
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
            value.Value = defaultText;
            return value;
        }
    }
}
