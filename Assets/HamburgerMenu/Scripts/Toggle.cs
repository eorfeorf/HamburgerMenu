using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace HamburgerMenu.Scripts
{
    public class Toggle : ItemBase
    {
        [SerializeField]
        private Button button;

        [SerializeField]
        private Image offImage;

        [SerializeField]
        private Image onImage;
        
        
        private readonly ReactiveProperty<bool> value = new ReactiveProperty<bool>();

        private void Start()
        {
            value.Subscribe(flag =>
            {
                Debug.Log($"toggle:{flag}");
                onImage.gameObject.SetActive(flag);
            }).AddTo(this);

            button.OnClickAsObservable().Subscribe(_ =>
            {
                value.Value = !value.Value;
            }).AddTo(this);
        }

        public ReactiveProperty<bool> Initialize(string label, bool flag)
        {
            standardParts.label.text = label;
            value.Value = flag;
            return value;
        }
    }
}
