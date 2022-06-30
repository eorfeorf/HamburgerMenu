using System;
using UniRx;
using UnityEngine;

namespace HamburgerMenu.Scripts
{
    public class ButtonItem : ItemBase
    {
        [SerializeField]
        private UnityEngine.UI.Button button;

        private ReactiveProperty<Unit> value = new ReactiveProperty<Unit>();

        public IObservable<Unit> Initialize(string label)
        {
            standardParts.label.text = label;
            return button.OnClickAsObservable();
        }
    }
}
