using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _ram;

    [SerializeField] [Min(5)]
    private int totalEnemies = 10;
    [SerializeField]
    private int enemiesLeft;

    [SerializeField] [Range(0, 50)]
    private int _poolSize = 5;
    [SerializeField] [Range(0.25f, 25f)]
    private float _spawnDelay = 1.5f;

    private GameObject[] pool;

    [SerializeField]
    private GameObject startButton;

    private CastleScript castleScript;

    private bool gameOver = false;

    private void Awake()
    {
        castleScript = FindObjectOfType<CastleScript>();
        enemiesLeft = totalEnemies;
        PopulatePool();
    }

    private void Start()
    {
        int buildIndex = SceneManager.GetActiveScene().buildIndex;

        if (buildIndex == 0 || buildIndex == 4) // Checks if scene is main menu or victory
        {
            StartCoroutine(SpawnEnemy());
        }
    }

    private void PopulatePool()
    {
        pool = new GameObject[_poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(_ram, transform);
            pool[i].SetActive(false);
        }
    }

    public void StartGame()
    {
        startButton.SetActive(false);

        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (!gameOver)
        {
            yield return new WaitForSeconds(_spawnDelay);

            if (!gameOver)
            {
                EnableObjectInPool();
            }
        }
    }
    
    private void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    public void EnemyDestroyed()
    {
        enemiesLeft -= 1;

        if (enemiesLeft <= 0)
        {
            StartCoroutine(LevelComplete());
        }
    }

    private IEnumerator LevelComplete()
    {
        gameOver = true;

        yield return new WaitForSeconds(5f);

        StartCoroutine(castleScript.LevelComplete());
    }


    public void GameOver()
    {
        gameOver = true;

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        gameObject.SetActive(false);
    }

    public void EndlessMode()
    {
        enemiesLeft = 9999;
    }
}
