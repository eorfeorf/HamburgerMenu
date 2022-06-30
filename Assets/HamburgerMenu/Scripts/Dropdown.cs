using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UniRx;
using UnityEngine;

namespace HamburgerMenu.Scripts
{
    public class Dropdown : ItemBase
    {
        [SerializeField]
        private TMP_Dropdown dropdown;
        
        /// <summary>
        /// enumで初期化.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="defaultValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IObservable<int> Initialize<T>(string label, int defaultValue) where T : Enum
        {
            standardParts.label.text = label;
            
            var names = Enum.GetNames(typeof(T));
            dropdown.AddOptions(names.ToList());
            dropdown.value = defaultValue;
            return dropdown.onValueChanged.AsObservable();
        }
        
        /// <summary>
        /// ICollectionで初期化.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public IObservable<int> Initialize(string label, ICollection value, int index)
        {
            standardParts.label.text = label;
            
            var input = (from object v in value select v.ToString()).ToList();
            dropdown.AddOptions(input);
            dropdown.value = index;
            return dropdown.onValueChanged.AsObservable();
        }
    }
}
