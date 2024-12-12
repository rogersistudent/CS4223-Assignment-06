using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemy;
    public GameObject spawnPoint;
    Bounds bounds;

    private void Start()
    {
        bounds = spawnPoint.GetComponent<Collider>().bounds;
    }

    public void SpawnEnemy()
    {
        float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
        float offsetY = Random.Range(-bounds.extents.y, bounds.extents.y);
        float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);
        Vector3 point = new Vector3(offsetX, offsetY, offsetZ);

        NavMeshHit hit;
        NavMesh.SamplePosition(point, out hit, Mathf.Infinity, NavMesh.AllAreas);
        GameObject newEnemy = GameObject.Instantiate(enemy);
        newEnemy.transform.position = hit.position;
    }
}
