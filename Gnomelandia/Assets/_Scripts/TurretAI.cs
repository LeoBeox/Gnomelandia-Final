using UnityEngine;

public class TurretAI : MonoBehaviour
{
    [Header("Combat Settings")]
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float range = 15f;
    public float fireRate = 1f;

    private float _fireCountdown = 0f;
    private Transform _target;

    void Update()
    {
        // Find Enemy
        FindTarget();

        // Found enemy = shoot it
        if (_target != null)
        {
            // Rotate to look at enemy
            Vector3 direction = _target.position - transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            // Rotation control
            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, Time.deltaTime * 10f);

            // Shoot
            if (_fireCountdown <= 0f)
            {
                Shoot();
                _fireCountdown = 1f / fireRate;
            }
        }

        _fireCountdown -= Time.deltaTime;
    }

    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            _target = nearestEnemy.transform;
        }
        else
        {
            _target = null;
        }
    }

    void Shoot()
    {
        if (projectilePrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            if (GameManager.Instance != null)
            {
                GameManager.Instance.PlaySFX(GameManager.Instance.turretShotClip);
            }
        }
    }
}