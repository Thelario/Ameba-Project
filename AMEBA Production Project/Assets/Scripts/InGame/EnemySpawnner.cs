using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnner : MonoBehaviour
{
    public List<Transform> enemySpawnPoints = new List<Transform>();

    public Transform enemyHolder;

    private float spawnTimeCounter;

    private GameManager gm;

    void Start()
    {
        gm = GameManager.GetInstance();

        GameManager.LevelExited += DeleteEnemies;
        GameManager.LevelStart += StartGame;

        foreach (Transform t in transform)
        {
            enemySpawnPoints.Add(t);
        }
    }
    
    void Update()
    {
        if (gm.inGame)
        {
            spawnTimeCounter -= Time.deltaTime;
            if (spawnTimeCounter <= 0f)
            {
                spawnTimeCounter = gm.enemySpawnTime;
                SpawnNewEnemy();
            }
        }
    }

    public void SpawnNewEnemy()
    {
        int one = Random.Range(0, 5);
        int two = Random.Range(0, gm.enemiesPrefabs.Count);

        Instantiate(gm.enemiesPrefabs[two], enemySpawnPoints[one].position, Quaternion.identity, enemyHolder);
    }

    public void DeleteEnemies()
    {
        foreach (Transform t in enemyHolder)
        {
            Destroy(t.gameObject);
        }
    }

    private void StartGame()
    {
        spawnTimeCounter = gm.enemySpawnTime;
    }
}