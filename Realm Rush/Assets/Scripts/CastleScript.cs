using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CastleScript : MonoBehaviour
{
    [SerializeField]
    private GameObject gameOverText;
    [SerializeField]
    private GameObject mainUIElements;

    [SerializeField]
    private Image castleHealthBar;
    [SerializeField]
    private Gradient healthGradient;

    [SerializeField] [Range(50f, 300f)]
    private float maxCastleHealth = 100f;

    private float currentCastleHealth;

    private void Awake()
    {
        currentCastleHealth = maxCastleHealth;
        UpdateDisplay();
    }

    public void DamageCastle(float amount)
    {
        currentCastleHealth -= Mathf.Abs(amount);

        if (currentCastleHealth <= 0)
        {
            StartCoroutine(GameOver());
        }

        UpdateDisplay();
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

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(0);
    }
}
