using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField]
    private Transform _weapon;
    [SerializeField]
    private ParticleSystem _projectileSystem;

    [Header("Tower Config")]

    [SerializeField] [Range(5f, 50f)] [Tooltip("Range at which the tower fires at incoming enemies")]
    private float _range = 25f;
    [SerializeField] [Range(0.1f, 4f)] [Tooltip("The rate at which the tower fires (RATE IS NOT IN SECONDS, DEPENDENT ON PARTICLE SYSTEM RATE))")]
    private float _firingRate = 0.85f;

    private Transform _target;

    // Variables


    private void Start()
    {
        var projectileEmission = _projectileSystem.emission;

        projectileEmission.rateOverTime = _firingRate;
    }

    private void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(enemy.transform.position, transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }

        _target = closestTarget;
    }

    private void AimWeapon()
    {
        if (_target)
        {
            float targetDistance = Vector3.Distance(transform.position, _target.position);

            if (targetDistance < _range)
            {
                _weapon.LookAt(_target);
                Attack(true);
            }
            else
            {
                Attack(false);
            }
        }
    }

    private void Attack(bool isActive)
    {
        var emissionModule = _projectileSystem.emission;

        emissionModule.enabled = isActive;
    }
}
