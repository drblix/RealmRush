using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _ram;
    [SerializeField]
    private int _poolSize = 5;
    [SerializeField] [Range(1f, 5f)]
    private float _spawnDelay = 1.5f;

    private GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
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

    
    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnDelay);
            EnableObjectInPool();
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
}