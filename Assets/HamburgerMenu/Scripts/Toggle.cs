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

        public ReactiveProperty<bool> Initialize(string label, bool defaultFlag)
        {
            value.Subscribe(flag =>
            {
                onImage.gameObject.SetActive(flag);
            }).AddTo(this);

            button.OnClickAsObservable().Subscribe(_ =>
            {
                value.Value = !value.Value;
            }).AddTo(this);
            
            standardParts.label.text = label;
            value.Value = defaultFlag;
            return value;
        }
    }
}
