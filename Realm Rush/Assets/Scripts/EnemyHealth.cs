using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    [Range(1, 10)]
    [Tooltip("Enemy's max health")]
    private int _maxHitPoints = 5;

    [SerializeField]
    [Range(1, 3)]
    [Tooltip("Adds given amount to enemy's maxHitPoints after death")]
    private int _difficultyRamp = 1;

    private int _currentHitPoints;

    private Enemy enemy;
    private ObjectPool pool;

    private void Awake()
    {
        enemy = GetComponent<Enemy>();
        pool = FindObjectOfType<ObjectPool>();
    }

    private void OnEnable()
    {
        _currentHitPoints = _maxHitPoints;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        _currentHitPoints--;

        if (_currentHitPoints <= 0)
        {
            pool.EnemyDestroyed();
            enemy.RewardGold();
            _maxHitPoints += _difficultyRamp;
            gameObject.SetActive(false);
        }
    }
}
