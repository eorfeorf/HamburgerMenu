using System;
using UniRx;
using UnityEngine;

namespace HamburgerMenu.Scripts
{
    public class ButtonItem : ItemBase
    {
        [SerializeField]
        private UnityEngine.UI.Button button;

        /// <summary>
        /// 初期化.
        /// </summary>
        /// <param name="label"></param>
        /// <returns></returns>
        public IObservable<Unit> Initialize(string label)
        {
            standardParts.label.text = label;
            return button.OnClickAsObservable();
        }
    }
}
