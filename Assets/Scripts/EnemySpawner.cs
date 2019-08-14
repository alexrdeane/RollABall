using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mirror;
public class EnemySpawner : NetworkBehaviour
{
    public GameObject enemyPrefab;
    public float spawnTimer = 1f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", this.spawnTimer, this.spawnTimer);
    }

    void Update()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-4f, 4f), this.transform.position.y, Random.Range(-4f, 4f));
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(enemy);
        Destroy(enemy, 10);
    }
}
