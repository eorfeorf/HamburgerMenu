using System;
using UnityEngine;
using UnityEngine.UI;

namespace ColorPicker.Scripts
{
    public class ColorViewer : MonoBehaviour
    {
        [SerializeField]
        private RawImage newColor;
        [SerializeField]
        private RawImage nowColor;
        [SerializeField]
        private Material originalMaterial;

        private void Awake()
        {
            newColor.material = new Material(originalMaterial);
            nowColor.material = new Material(originalMaterial);
        }

        public void ApplyNewColor(Vector3 rgb)
        {
            newColor.material.SetColor("_RGB", new Color(rgb.x,rgb.y,rgb.z, 1f));
        }

        public void ApplyNowColor(Vector3 rgb)
        {
            nowColor.material.SetColor("_RGB", new Color(rgb.x,rgb.y,rgb.z, 1f));
        }
    }
}
