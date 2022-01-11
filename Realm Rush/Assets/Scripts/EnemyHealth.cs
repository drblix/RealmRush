using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 10f)]
    private int _maxHitPoints = 5;

    private int _currentHitPoints;

    private void Awake()
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
            Destroy(gameObject);
        }
    }
}
