using ColorPicker.Scripts.Common;
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
            parameterRGB.OnEditG.Subscribe(value =>
            {
                rgb.y = value;
                Apply(rgb);
            }).AddTo(this);
            parameterRGB.OnEditB.Subscribe(value =>
            {
                rgb.z = value;
                Apply(rgb);
            }).AddTo(this);
            
            // ParameterHSV
            parameterHSV.OnEditH.Subscribe(value =>
            {
                var hsv = rgb.ToColor().RGBToHSV();
                hsv.x = value;
                var color = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
                rgb = color.ToVector3();
                Apply(rgb);
            }).AddTo(this);
            parameterHSV.OnEditS.Subscribe(value =>
            {
                var hsv = rgb.ToColor().RGBToHSV();
                hsv.y = value;
                var color = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
                rgb = color.ToVector3();
                Apply(rgb);
            }).AddTo(this);
            parameterHSV.OnEditV.Subscribe(value =>
            {
                var hsv = rgb.ToColor().RGBToHSV();
                hsv.z = value;
                var color = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
                rgb = color.ToVector3();
                Apply(rgb);
            }).AddTo(this);
            
            // ColorPanel
            
            // ColorSlider
            colorSlider.Hue01.Subscribe(hue =>
            {
                var hsv = rgb.ToColor().RGBToHSV();
                var color = Color.HSVToRGB(hue, hsv.y, hsv.z);
                rgb = color.ToVector3();
                Apply(rgb);
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
