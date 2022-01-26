using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    private UIManager uiManager;

    [SerializeField]
    private Transform weapon;
    [SerializeField]
    private ParticleSystem projectileSystem;
    [SerializeField]
    private AudioSource ballistaShoot;

    [Header("Tower Config")]

    [SerializeField] [Range(5f, 50f)] [Tooltip("Range at which the tower fires at incoming enemies")]
    private float range = 25f;
    [SerializeField] [Range(0.4f, 2.5f)] [Tooltip("The rate at which the tower fires")]
    private float firingRate = 1.5f;

    private Transform target;

    private bool canFire = true;

    // Variables

    private void Awake()
    {
        uiManager = FindObjectOfType<UIManager>();
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

        target = closestTarget;
    }

    private void AimWeapon()
    {
        if (target)
        {
            float targetDistance = Vector3.Distance(transform.position, target.position);

            if (targetDistance < range && canFire)
            {
                weapon.LookAt(target);

                if (canFire)
                {
                    StartCoroutine(UseWeapon());
                }
            }
        }
    }

    private IEnumerator UseWeapon()
    {
        bool muted = uiManager.TowersMuted;

        canFire = false;
        projectileSystem.Emit(1);

        if (!muted)
        {
            ballistaShoot.Play();
        }

        yield return new WaitForSeconds(firingRate);

        canFire = true;
    }

    /*
    private void Attack(bool isActive)
    {
        var emissionModule = _projectileSystem.emission;

        emissionModule.enabled = isActive;
    }
    */
}
