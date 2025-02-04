using UnityEngine;
using System.Linq;

public class Turret : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float detectionRange = 5f;
    public float fireCooldown = 2f;
    public float firingAngleThreshold = 0.9f;
    public float rotationSpeed = 200f;

    private float fireCooldownTimer = 0f;
    private Transform targetEnemy;

    void Update()
    {
        fireCooldownTimer -= Time.deltaTime;
        
        FindClosestEnemy();

        if (targetEnemy == null) return;
        
        Vector3 directionToEnemy = (targetEnemy.position - transform.position).normalized;
        RotateTowardsTarget(directionToEnemy);

        Vector3 turretDirection = transform.right;
        float dot = Vector3.Dot(turretDirection, directionToEnemy);
        
        if (dot > firingAngleThreshold && fireCooldownTimer <= 0f)
        {
            Fire(turretDirection);
            fireCooldownTimer = fireCooldown;
        }
    }

    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        targetEnemy = enemies
            .Where(e => (e.transform.position - transform.position).magnitude <= detectionRange)
            .OrderBy(e => (e.transform.position - transform.position).magnitude)
            .Select(e => e.transform)
            .FirstOrDefault();
    }

    void RotateTowardsTarget(Vector3 directionToEnemy)
    {
        float targetAngle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
        float currentAngle = transform.eulerAngles.z;
        float newAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
    }

    void Fire(Vector3 direction)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().SetDirection(direction);
    }
}
