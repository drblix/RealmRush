using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private GameObject mainScreen;
    private GameObject creditScreen;

    private void Awake()
    {
        mainScreen = transform.Find("MainScreen").gameObject;
        creditScreen = transform.Find("CreditScreen").gameObject;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenCloseCredits()
    {
        mainScreen.SetActive(!mainScreen.activeInHierarchy);
        creditScreen.SetActive(!creditScreen.activeInHierarchy);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
