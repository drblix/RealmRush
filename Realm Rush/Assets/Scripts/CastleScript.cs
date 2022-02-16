using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CastleScript : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverText;
    [SerializeField]
    private GameObject levelCompleteText;
    [SerializeField]
    private GameObject mainUIElements;

    private Transform objectPool;

    [SerializeField]
    private Image castleHealthBar;
    [SerializeField]
    private Gradient healthGradient;

    [SerializeField] [Range(50f, 300f)]
    private float maxCastleHealth = 100f;

    private float currentCastleHealth;

    private bool godMode = false;
    public bool GodMode { get { return godMode; } }

    private void Awake()
    {
        objectPool = FindObjectOfType<ObjectPool>().transform;
        currentCastleHealth = maxCastleHealth;
        UpdateDisplay();
    }

    public void DamageCastle(float amount)
    {
        if (!godMode)
        {
            currentCastleHealth -= Mathf.Abs(amount);

            if (currentCastleHealth <= 0)
            {
                StartCoroutine(GameOver());
            }

            UpdateDisplay();
        }
    }


    private void UpdateDisplay()
    {
        float newHealth = currentCastleHealth / maxCastleHealth;
        castleHealthBar.fillAmount = newHealth;

        Color newColor = healthGradient.Evaluate(newHealth);
        castleHealthBar.color = newColor;
    }

    
    private IEnumerator GameOver()
    {
        ObjectPool objectPool = FindObjectOfType<ObjectPool>();
        Transform tiles = GameObject.FindGameObjectWithTag("Environment").transform.Find("WorldTiles");

        objectPool.GameOver();

        foreach (Transform child in tiles)
        {
            child.GetComponent<Waypoint>().GameOver();
        }

        mainUIElements.SetActive(false);
        gameOverText.SetActive(true);

        yield return new WaitForSeconds(5f);

        SceneManager.LoadScene(0);
    }

    public IEnumerator LevelComplete()
    {
        Transform tiles = GameObject.FindGameObjectWithTag("Environment").transform.Find("WorldTiles");

        foreach (Transform child in tiles)
        {
            child.GetComponent<Waypoint>().GameOver();
        }

        mainUIElements.SetActive(false);
        levelCompleteText.SetActive(true);

        yield return new WaitForSeconds(5f);

        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        SceneManager.LoadScene(nextScene);
    }

    public void ToggleGodMode()
    {
        godMode = !godMode;
    }
}
