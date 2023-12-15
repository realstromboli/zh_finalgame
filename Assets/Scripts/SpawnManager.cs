using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float startDelay = 2;
    private float spawnInterval = 2.5f;
    private float cansStartDelay = 0.1f;
    private float cansSpawnInterval = 2.25f;
    public GameObject[] enemyPrefabs;
    public GameObject[] itemPrefabs;
    public GameManager gameManagerVariable;

    void Start()
    {
        gameManagerVariable = GameObject.Find("GameManager").GetComponent<GameManager>();
        InvokeRepeating("SpawnEnemy", startDelay, spawnInterval);
        InvokeRepeating("SpawnCans", cansStartDelay, cansSpawnInterval);

    }

    private float spawnRangeX = 35;
    private float spawnRangeZ = 35;
    void Update()
    {

    }

    void SpawnEnemy()
    {
        if (gameManagerVariable.isGameActive == true)
        {
            int enemyIndex = Random.Range(0, 4);
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
            Instantiate(enemyPrefabs[enemyIndex], spawnPos, enemyPrefabs[enemyIndex].transform.rotation);
        }
        
    }

    void SpawnCans()
    {
        if (gameManagerVariable.isGameActive == true)
        {
            int cansIndex = Random.Range(0, 4);
            Vector3 cansSpawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, Random.Range(-spawnRangeZ, spawnRangeZ));
            Instantiate(itemPrefabs[cansIndex], cansSpawnPos, itemPrefabs[cansIndex].transform.rotation);
        }
        
    }
}
