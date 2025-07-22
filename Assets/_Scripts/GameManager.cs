using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject homeObject;
    [SerializeField] private GameObject playerObject, attackPoint;
    [SerializeField] private GameObject uiPlayer;
    [SerializeField] private GameObject endMenu;
    public GameObject[] maps;
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
        if (playerObject == null)
            return;
        OpenHomeGameScene();
    }
    public void OnEndMenu()
    {
        endMenu.SetActive(true);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }
    public void OpenMenuScene()
    {
        SceneManager.LoadScene(0);
    }
    public void OpenHomeGameScene()
    {
        if (homeObject != null) 
            homeObject.SetActive(true);
        endMenu.SetActive(false);
        playerObject.SetActive(false);
        attackPoint.SetActive(false);
        uiPlayer.SetActive(false);
        foreach (GameObject map in maps)
        {
            map.SetActive(false);
        }
    }
    public void OnDisplayMap(int lv)
    {
        playerObject.GetComponent<PlayerController>().ResetPlayer();
        maps[lv].SetActive(true);
        Transform startPoint = maps[lv].transform.Find("Start Point");
        playerObject.transform.position = startPoint.position;
        endMenu.SetActive(false);
        playerObject.SetActive(true);
        uiPlayer.SetActive(true);
        homeObject.SetActive(false);
    }
}
