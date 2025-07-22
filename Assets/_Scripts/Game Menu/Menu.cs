using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName;

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }
    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }
}
