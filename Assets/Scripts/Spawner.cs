using UnityEngine;

public class CreatureSpawner : MonoBehaviour
{
    public GameObject creaturePrefab;
    public Transform spawnPointQuadratic;
    public Transform spawnPointCubic;
    public Transform endPoint;
    
    public float spawnInterval = 3f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnQuadraticCreature();
            SpawnCubicCreature();
            timer = 0f;
        }
    }

    void SpawnQuadraticCreature()
    {
        GameObject enemy = Instantiate(creaturePrefab, spawnPointQuadratic.position, Quaternion.identity);
        Vector3 controlPoint = spawnPointQuadratic.position + new Vector3(2f, 3f, 0f);
        enemy.GetComponent<Enemy>().SetPath(spawnPointQuadratic.position, controlPoint, endPoint.position);
    }

    void SpawnCubicCreature()
    {
        GameObject enemy = Instantiate(creaturePrefab, spawnPointCubic.position, Quaternion.identity);
        Vector3 controlPoint1 = spawnPointCubic.position + new Vector3(2f, 4f, 0f);
        Vector3 controlPoint2 = spawnPointCubic.position + new Vector3(4f, 2f, 0f);
        enemy.GetComponent<Enemy>().SetPath(spawnPointCubic.position, controlPoint1, endPoint.position, true, controlPoint2);
    }
}