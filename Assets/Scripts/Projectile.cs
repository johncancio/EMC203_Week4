using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float detectionRange = 0.5f;

    private Vector3 direction;

    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
        CheckForEnemyHit();
    }

    void CheckForEnemyHit()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = enemies
            .OrderBy(e => (e.transform.position - transform.position).sqrMagnitude)
            .FirstOrDefault();

        if (closestEnemy != null && (closestEnemy.transform.position - transform.position).magnitude <= detectionRange)
        {
            Destroy(closestEnemy);
            Destroy(gameObject);
        }
    }
}
