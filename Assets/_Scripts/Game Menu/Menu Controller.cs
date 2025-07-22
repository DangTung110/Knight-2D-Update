using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public Menu[] menus;

    private void Start()
    {
        Open(MenuName.Start);
    }
    public void Open(string name)
    {
        foreach (Menu menu in menus)
        {
            if (menu.menuName == name)
                menu.OpenMenu();
            else
                menu.CloseMenu();
        }
    }
}
