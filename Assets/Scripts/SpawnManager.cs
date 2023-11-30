using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float startDelay = 2;
    private float spawnInterval = 1.5f;
    private float cansStartDelay = 0.5f;
    private float cansSpawnInterval = 3f;
    public GameObject[] enemyPrefabs;
    public GameObject[] itemPrefabs;
    void Start()
    {
        InvokeRepeating("SpawnEnemy", startDelay, spawnInterval);
        InvokeRepeating("SpawnCans", cansStartDelay, cansSpawnInterval);
    }

    private float spawnRangeX = 20;
    private float spawnRangeZ = 20;
    void Update()
    {

    }

    void SpawnEnemy()
    {
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
        Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
    }

    void SpawnCans()
    {
        //spawn the cans in random locations
        int cansIndex = Random.Range(0, itemPrefabs.Length);
        Vector3 cansSpawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
        Instantiate(itemPrefabs[cansIndex], cansSpawnPos, enemyPrefabs[cansIndex].transform.rotation);
    }
}
