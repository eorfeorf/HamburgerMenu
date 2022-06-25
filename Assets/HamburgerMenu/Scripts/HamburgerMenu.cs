using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace HamburgerMenu.Scripts
{
    public class HamburgerMenu : MonoBehaviour
    {
        [SerializeField]
        private Button openButton;
        [SerializeField]
        private Button closeButton;

        [SerializeField]
        private Transform itemParent;
        
        [Header("Prefabs")]
        [SerializeField]
        private SliderInt sliderIntPrefab;
        [SerializeField]
        private SliderFloat sliderFloatPrefab;
        [SerializeField]
        private Toggle togglePrefab;
        [SerializeField]
        private Dropdown dropdownPrefab;

        public IDisposable OnOpen(Action act) => onOpen.Subscribe(_ => act()).AddTo(this);
        private readonly Subject<Unit> onOpen = new Subject<Unit>();
        public IDisposable OnClose(Action act) => onClose.Subscribe(_ => act()).AddTo(this);
        private readonly Subject<Unit> onClose = new Subject<Unit>();

        // メニューの要素のキャッシュ.
        private readonly List<GameObject> repository = new List<GameObject>();

        private void Start()
        {
            openButton.OnClickAsObservable().Subscribe(_=>
            {
                onOpen.OnNext(Unit.Default);
                ShowAll();
                openButton.gameObject.SetActive(false);
                closeButton.gameObject.SetActive(true);
            }).AddTo(this);
            closeButton.OnClickAsObservable().Subscribe(_=>
            {
                onClose.OnNext(Unit.Default);
                HideAll();
                openButton.gameObject.SetActive(true);
                closeButton.gameObject.SetActive(false);
            }).AddTo(this);
        }
        
        /// <summary>
        /// 全てのアイテムを表示.
        /// </summary>
        private void ShowAll()
        {
            repository.ForEach(x => x.SetActive(true));
        }
        /// <summary>
        /// 全てのアイテムを非表示.
        /// </summary>
        private void HideAll()
        {
            repository.ForEach(x => x.SetActive(false));
        }

        #region CreateMethod
        
        /// <summary>
        /// int スライダー追加
        /// </summary>
        /// <param name="value"></param>
        public IObservable<int> AddSliderInt(string label, int value, int min, int max, int unit)
        {
            return CreateItem(sliderIntPrefab, itemParent).Initialize(label, value, min, max, unit);
        }

        /// <summary>
        /// float スライダー追加
        /// </summary>
        /// <param name="value"></param>
        public IObservable<float> AddSliderFloat(string label, float value, float min, float max, float unit)
        {
            return CreateItem(sliderFloatPrefab, itemParent).Initialize(label, value, min, max, unit);
        }
        
        /// <summary>
        /// Toggle
        /// </summary>
        /// <param name="value"></param>
        public IObservable<bool> AddToggle(string label, bool value)
        {
            return CreateItem(togglePrefab, itemParent).Initialize(label, value);
        } 
        #endregion
        
        /// <summary>
        /// Dropdown enum
        /// </summary>
        /// <param name="label"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IObservable<int> AddDropdown<T>(string label, int value) where T : Enum
        {
            return CreateItem(dropdownPrefab, itemParent).Initialize<T>(label, value);
        }
        
        /// <summary>
        /// Dropdown Collection
        /// </summary>
        /// <param name="label"></param>
        /// <param name="value"></param>
        /// <param name="defaultIndex"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IObservable<int> AddDropdown(string label, ICollection value, int defaultIndex)
        {
            if (0 <= defaultIndex && defaultIndex < value.Count)
            {
                return CreateItem(dropdownPrefab, itemParent).Initialize(label, value, defaultIndex);
            }
            throw new Exception("[HamburgerMenu] Invalid default index.");
        }
        
        #region private
        private T CreateItem<T>(T prefab, Transform parent) where T : MonoBehaviour
        {
            var obj = Instantiate(prefab, parent).GetComponent<T>();
            repository.Add(obj.gameObject);
            return obj;
        }
        #endregion

    }
}