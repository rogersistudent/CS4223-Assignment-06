using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coin;
    public GameObject spawnPoint;
    Bounds bounds;

    private void Start()
    {
        bounds = spawnPoint.GetComponent<Collider>().bounds;
    }

    public void SpawnCoin()
    {
        float offsetX = Random.Range(-bounds.extents.x, bounds.extents.x);
        float offsetY = 10f;
        float offsetZ = Random.Range(-bounds.extents.z, bounds.extents.z);
        Vector3 point = new Vector3(offsetX, offsetY, offsetZ);

        NavMeshHit hit;
        NavMesh.SamplePosition(point, out hit, Mathf.Infinity, NavMesh.AllAreas);
        GameObject newCoin = GameObject.Instantiate(coin);
        newCoin.transform.position = hit.position;
    }
}
