using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public class DebugMenu : MonoBehaviour
    {
        [SerializeField] private HamburgerMenu.Scripts.HamburgerMenu hamburgerMenu;

        [SerializeField]
        private Button openButton;

        [SerializeField]
        private Button closeButton;
        
        enum Test
        {
            none,
            success,
            fail,
        }

        private Dictionary<int, string> dict = new()
        {
            {0, "a"}, {1, "b"}, {2, "c"},
        };
        private List<string> list = new()
        {
            "a", "b", "c"
        };
        private Array ary = new[]
        {
            0, 1, 2
        };
        private int[] ary2 = {
            0, 1, 2
        };
        
        private void Start()
        {
            openButton.onClick.AsObservable().Subscribe(_ =>
            {
                hamburgerMenu.ShowAll();
            }).AddTo(this);
            
            closeButton.onClick.AsObservable().Subscribe(_ =>
            {
                hamburgerMenu.HideAll();
            }).AddTo(this);
            
            hamburgerMenu.Initialize();
            
            hamburgerMenu.AddSliderInt("int",10, 0, 50, 10).Subscribe(x =>
            {
                Debug.Log("slider int: " + x);
            }).AddTo(this);
            hamburgerMenu.AddSliderFloat("float",10, 0, 50, 0.1f).Subscribe(x =>
            {
                Debug.Log("slider float: " + x);
            }).AddTo(this);
            hamburgerMenu.AddToggle("toggle", false).Subscribe(x =>
            {
                Debug.Log("toggle: " + x);
            }).AddTo(this);
            hamburgerMenu.AddDropdown<Test>("dropdown", (int) Test.none).Subscribe(x =>
            {
                Debug.Log("dropdown: " + x);
            }).AddTo(this);
            hamburgerMenu.AddDropdown("dictionary", dict, 1).Subscribe(x =>
            {
                Debug.Log("dictionary: " + x);
            }).AddTo(this);
            hamburgerMenu.AddDropdown("list", list, 0).Subscribe(x =>
            {
                Debug.Log("list: " + x);
            }).AddTo(this);
            hamburgerMenu.AddDropdown("array", ary, 0).Subscribe(x =>
            {
                Debug.Log("array: " + x);
            }).AddTo(this);
            hamburgerMenu.AddDropdown("array2", ary2, 0).Subscribe(x =>
            {
                Debug.Log("array2: " + x);
            }).AddTo(this);
            hamburgerMenu.AddTextField("text", "10").Subscribe(x =>
            {
                Debug.Log("text field: " + x);
            }).AddTo(this);
            hamburgerMenu.AddButton("button").Subscribe(_ =>
            {
                Debug.Log("button: pushed");
            }).AddTo(this);
        }
    }
}