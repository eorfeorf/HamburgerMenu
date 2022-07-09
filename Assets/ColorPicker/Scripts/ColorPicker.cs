using System;
using ColorPicker.Scripts.Common;
using UniRx;
using UnityEngine;
using Object = System.Object;

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
                ApplyOnChanged(rgb, parameterRGB);
            }).AddTo(this);
            parameterRGB.OnEditG.Subscribe(value =>
            {
                rgb.y = value;
                ApplyOnChanged(rgb, parameterRGB);
            }).AddTo(this);
            parameterRGB.OnEditB.Subscribe(value =>
            {
                rgb.z = value;
                ApplyOnChanged(rgb, parameterRGB);
            }).AddTo(this);
            
            // ParameterHSV
            parameterHSV.OnEditH.Subscribe(value =>
            {
                var hsv = rgb.ToColor().RGBToHSV();
                hsv.x = value;
                var color = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
                rgb = color.ToVector3();
                ApplyOnChanged(rgb, parameterHSV);
            }).AddTo(this);
            parameterHSV.OnEditS.Subscribe(value =>
            {
                var hsv = rgb.ToColor().RGBToHSV();
                hsv.y = value;
                var color = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
                rgb = color.ToVector3();
                ApplyOnChanged(rgb, parameterHSV);
            }).AddTo(this);
            parameterHSV.OnEditV.Subscribe(value =>
            {
                var hsv = rgb.ToColor().RGBToHSV();
                hsv.z = value;
                var color = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
                rgb = color.ToVector3();
                ApplyOnChanged(rgb, parameterHSV);
            }).AddTo(this);
            
            // ColorSlider
            colorSlider.Hue01.Subscribe(hue =>
            {
                var color = Color.HSVToRGB(hue, parameterHSV.OnEditS.Value, parameterHSV.OnEditV.Value);
                rgb = color.ToVector3();
                ApplyOnChanged(rgb, colorSlider, parameterHSV);
                parameterHSV.ApplyHue(hue);
            }).AddTo(this);
            
            // ColorPanel
            colorPanel.SV01.Subscribe(sv =>
            {
                var hsv = rgb.ToColor().RGBToHSV();
                hsv = new Vector3(hsv.x, sv.x, sv.y);
                var color = Color.HSVToRGB(hsv.x, hsv.y, hsv.z);
                rgb = color.ToVector3();
                ApplyOnChanged(rgb, colorPanel, colorSlider);
            }).AddTo(this);
        }

        private void ApplyOnChanged(Vector3 rgb, Object excludeField1, Object excludeField2 = null)
        {
            var exclude = false;
            
            if(!IsExcludeType(typeof(ParameterRGB), excludeField1, excludeField2) )
            {
                parameterRGB.Apply(rgb);   
            }
            if(!IsExcludeType(typeof(ParameterHSV), excludeField1, excludeField2))
            {
                parameterHSV.Apply(rgb);   
            }
            if (!IsExcludeType(typeof(ColorPanel), excludeField1, excludeField2))
            {
                colorPanel.Apply(rgb);
            }
            if (!IsExcludeType(typeof(ColorSlider), excludeField1, excludeField2))
            {
                colorSlider.Apply(rgb);
            }
            if (!IsExcludeType(typeof(ColorViewer), excludeField1, excludeField2))
            {
                colorViewer.ApplyNewColor(rgb);
            }
        }
        
        private bool IsExcludeType(Type excludeType, Object field1, Object field2 = null)
        {
            if (excludeType == null)
            {
                return false;
            }

            if (field1 != null)
            {
                var fieldType = field1.GetType();
                if (excludeType == fieldType)
                {
                    return true;
                }   
            }

            if (field2 != null)
            {
                var field2Type = field2.GetType();
                if (excludeType == field2Type)
                {
                    return true;
                }   
            }

            return false;
        }
    }
}
