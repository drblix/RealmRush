using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private Transform playerCamera;

    private GameObject mainScreen;
    private GameObject creditScreen;

    [SerializeField]
    private AudioClip buttonClick;

    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        mainScreen = transform.Find("MainScreen").gameObject;
        creditScreen = transform.Find("CreditScreen").gameObject;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenCloseCredits()
    {
        AudioSource.PlayClipAtPoint(buttonClick, playerCamera.position, 1f);
        mainScreen.SetActive(!mainScreen.activeInHierarchy);
        creditScreen.SetActive(!creditScreen.activeInHierarchy);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
