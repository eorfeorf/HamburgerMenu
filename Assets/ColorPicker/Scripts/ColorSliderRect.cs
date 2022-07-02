using System;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ColorPicker.Scripts
{
    public class ColorSliderRect : MonoBehaviour
    {
        [SerializeField]
        private Image pointer;

        public RectTransform RectTransform { get; private set; }
        public IObservable<PointerEventData> OnPointerClick;
        public IObservable<PointerEventData> OnPointerDrag;

        private ObservableEventTrigger eventTrigger;
        
        private void Awake()
        {
            eventTrigger = gameObject.AddComponent<ObservableEventTrigger>();

            RectTransform = GetComponent<RectTransform>();
            OnPointerClick = eventTrigger.OnPointerClickAsObservable();
            OnPointerDrag = eventTrigger.OnDragAsObservable();
        }
    }
}
