using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject settingsMenu;
    [SerializeField]
    private GameObject settingsMenuButton;

    private AudioSource buttonClick;

    private Transform worldTiles;

    private bool towersMuted = false;
    public bool TowersMuted { get { return towersMuted; } }

    private float oldScale = 1f;
    private bool gamePaused = false;

    private void Awake()
    {
        worldTiles = GameObject.FindGameObjectWithTag("WorldTiles").transform;
        buttonClick = GetComponent<AudioSource>();

        settingsMenu.SetActive(false);

        if (!worldTiles) { Debug.LogError("World tiles not found!"); }
    }

    private void Update()
    {
        if (!gamePaused)
        {
            if (IsPointerOverUIObject())
            {
                ToggleTiles(true);
            }
            else
            {
                ToggleTiles(false);
            }
        }
    }

    public void ToggleTowersAudio(bool state)
    {
        buttonClick.Play();

        if (state)
        {
            towersMuted = true;
        }
        else
        {
            towersMuted = false;
        }
    }

    private void ToggleTiles(bool overUI)
    {
        if (overUI)
        {
            foreach (Transform child in worldTiles)
            {
                child.GetComponent<Waypoint>().ToggleTile(false);
            }
        }
        else
        {
            foreach (Transform child in worldTiles)
            {
                child.GetComponent<Waypoint>().ToggleTile(true);
            }
        }
    }

    public void ToggleSettings()
    {
        if (Time.timeScale > 0f)
        {
            oldScale = Time.timeScale;
        }

        buttonClick.Play();

        settingsMenu.SetActive(!settingsMenu.activeInHierarchy);
        settingsMenuButton.SetActive(!settingsMenuButton.activeInHierarchy);

        if (settingsMenu.activeInHierarchy)
        {
            gamePaused = true;
            ToggleTiles(true);
            Time.timeScale = 0f;
        }
        else
        {
            gamePaused = false;
            ToggleTiles(false);
            Time.timeScale = oldScale;
        }
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene(0);
    }

    private static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
