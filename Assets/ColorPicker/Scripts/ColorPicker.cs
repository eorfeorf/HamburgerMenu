using UniRx;
using UnityEngine;

namespace ColorPicker.Scripts
{
    public class ColorPicker : MonoBehaviour
    {
        [SerializeField]
        private ColorViewer colorViewer;
        [SerializeField]
        private ColorPanel colorPanel;
        [SerializeField]
        private ColorSlider colorSlider;
        [SerializeField]
        private ParameterRGB parameterRGB;
        [SerializeField]
        private ParameterHSV parameterHSV;

        // 外部用の最終的に決まった色.
        public IReactiveProperty<Color> FixColor => fixColor;
        private ReactiveProperty<Color> fixColor = new ReactiveProperty<Color>(Color.white);
        
        // ほぼテンポラリなので生データでよい.
        // 一方的に変わるもの. 新しい色
        // 相互で変わるもの. rgb,hsv,colorPanel,colorSlider
        private Vector3 rgb;
      
        private float alpha;

        private void Awake()
        {
            FixColor.Subscribe(x =>
            {
                Debug.Log(x);
            }).AddTo(this);
        }

        private void Start()
        {
            // ParameterRGB
            parameterRGB.OnEditR.Subscribe(value =>
            {
                rgb.x = value;
                Apply(rgb);
            }).AddTo(this);
            parameterRGB.OnEditG.SkipLatestValueOnSubscribe().Subscribe(value =>
            {
                rgb.y = value;
                Apply(rgb);
            }).AddTo(this);
            parameterRGB.OnEditB.SkipLatestValueOnSubscribe().Subscribe(value =>
            {
                rgb.z = value;
                Apply(rgb);
            }).AddTo(this);
            
            // ParameterHSV
            parameterHSV.OnEditH.SkipLatestValueOnSubscribe().Subscribe(value =>
            {
                rgb.x = value;
                Apply(rgb);
            }).AddTo(this);
            parameterHSV.OnEditS.SkipLatestValueOnSubscribe().Subscribe(value =>
            {
                rgb.y = value;
                Apply(rgb);
            }).AddTo(this);
            parameterHSV.OnEditV.SkipLatestValueOnSubscribe().Subscribe(value =>
            {
                rgb.z = value;
                Apply(rgb);
            }).AddTo(this);
            
            // ColorPanel
            
            // ColorSlider
            colorSlider.Hue01.SkipLatestValueOnSubscribe().Subscribe(hue =>
            {
                var color = Color.HSVToRGB(hue, 1f, 1f);
                //colorPanel.Apply(new Vector3(color.r, color.g, color.b));
            }).AddTo(this);
        }

        private void Apply(Vector3 rgb)
        {
            parameterRGB.Apply(rgb);
            parameterHSV.Apply(rgb);
            colorPanel.Apply(rgb);
            colorSlider.Apply(rgb);
            colorViewer.ApplyNewColor(rgb);
        }
    }
}
