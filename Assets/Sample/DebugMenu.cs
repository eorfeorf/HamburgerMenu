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
            hamburgerMenu.AddSliderInt("int",10, 0, 50, 10);
            hamburgerMenu.AddSliderFloat("float",10, 0, 50, 0.1f);
            hamburgerMenu.AddToggle("toggle", false);
            hamburgerMenu.AddDropdown<Test>("dropdown", (int) Test.none);
            hamburgerMenu.AddDropdown("dictionary", dict, 1);
            hamburgerMenu.AddDropdown("list", list, 0);
            hamburgerMenu.AddDropdown("array", ary, 0);
            hamburgerMenu.AddDropdown("array2", ary2, 0);
            hamburgerMenu.AddTextField("Text", "test text");
        }
    }
}