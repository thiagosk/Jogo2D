using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] enemyPrefabs;

    void Start()
    {    
        for (int i = 0; i < spawnPoints.Length; i++) {
            int randCount = Random.Range(0, 5);
            if (randCount <= 3) {
                int randEnemy = Random.Range(0, enemyPrefabs.Length);
                int randSpawnPoint = Random.Range(0, spawnPoints.Length);
                Instantiate(enemyPrefabs[randEnemy], spawnPoints[randSpawnPoint].position, transform.rotation);
            }
        }
    }
}
