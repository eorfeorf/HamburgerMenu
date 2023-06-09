# HamburgerMenu
ハンバーガーメニューが出せるライブラリです。

![image](https://github.com/eorfeorf/HamburgerMenu/assets/10098082/29ed80a4-4e2e-4770-bb85-c1751dcf546e)



## How to use
ライブラリ内にあるPrefabをシーンに設置。
表示させたいパラメータをAddする。

```
[SerializeField] private HamburgerMenu hamburgerMenu;

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
```
