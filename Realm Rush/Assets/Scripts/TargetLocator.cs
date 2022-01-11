using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField]
    private Transform _weapon;
    [SerializeField]
    private ParticleSystem _projectileSystem;
    [SerializeField]
    private float _range = 25f;

    private Transform _target;

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
