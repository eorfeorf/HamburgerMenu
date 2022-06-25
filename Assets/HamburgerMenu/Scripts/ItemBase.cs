using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HamburgerMenu.Scripts
{
    [Serializable]
    public class StandardParts
    {
        public RawImage bg;
        public TMP_Text label;
    }
    
    public class ItemBase : MonoBehaviour
    {
        [Header("Standard parts")]
        [SerializeField]
        protected StandardParts standardParts;
    }
}
