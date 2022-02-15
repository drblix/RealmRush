using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    private Transform playerCamera;

    private GameObject mainScreen;
    private GameObject creditScreen;
    private GameObject storyScreen;

    [SerializeField]
    private GameObject customizationArea;
    [SerializeField]
    private GameObject storyText;
    [SerializeField]
    private GameObject speedChanger;

    [SerializeField]
    private TextMeshProUGUI playerName;

    [SerializeField]
    private AudioClip buttonClick;

    [SerializeField] [Min(0.01f)]
    private float scrollSpeed = 0.25f;

    private void Awake()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").transform;
        mainScreen = transform.Find("MainScreen").gameObject;
        creditScreen = transform.Find("CreditScreen").gameObject;
        storyScreen = transform.Find("StoryScreen").gameObject;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ToggleCredits()
    {
        AudioSource.PlayClipAtPoint(buttonClick, playerCamera.position, 1f);
        mainScreen.SetActive(!mainScreen.activeInHierarchy);
        creditScreen.SetActive(!creditScreen.activeInHierarchy);
    }

    public void ToggleStory()
    {
        AudioSource.PlayClipAtPoint(buttonClick, playerCamera.position, 1f);
        mainScreen.SetActive(!mainScreen.activeInHierarchy);
        storyScreen.SetActive(!storyScreen.activeInHierarchy);
    }

    public void SubmitName()
    {
        string plrName = playerName.text;

        storyText.GetComponent<TextMeshProUGUI>().text = "Once upon a time, two kingdoms lived in harmony. \n \nThough, one day, the Kingdom of the Purple Man Group declared war on the Kingdom of the Blue Man Group. \n \nFor years, the two kingdoms  fought between each other, until today. \n \nGeneral " + plrName + " has been employed by the Blue Man Group King to put a stop to this war, once, and for all. \n \nGood luck!";

        customizationArea.SetActive(false);
        storyText.SetActive(true);
        speedChanger.SetActive(true);

        StartCoroutine(ScrollStory());
    }

    private IEnumerator ScrollStory()
    {
        Transform textDestination = storyScreen.transform.Find("TextDestination");

        while (storyText.transform.position.y < textDestination.position.y)
        {
            Vector2 newPosition = Vector2.MoveTowards(storyText.transform.position, textDestination.position, scrollSpeed);
            storyText.transform.position = newPosition;

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(0);
    }

    public void ScrollSpeedUpdate(float newSpeed)
    {
        scrollSpeed = newSpeed;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
